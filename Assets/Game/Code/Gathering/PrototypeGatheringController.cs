using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Input;
using Wayroot.Inventory;
using UnityEngine;

namespace Wayroot.Gathering
{
    public sealed class PrototypeGatheringController : MonoBehaviour
    {
        private const float Range = 2.8f, StepInterval = 0.45f;
        private readonly List<GatheringNode> _nodes = new();
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private InventoryState _inventory = null!;
        private PrototypeGatheringSave _save = null!;
        private float _nextStepTime;
        public int GetCount(ResourceType resource) => _inventory.GetCount(resource);
        public void Configure(PrototypeInputReader input, PrototypePlayerController player, InventoryState inventory, IEnumerable<GatheringNode> nodes)
        {
            _input = input; _player = player; _inventory = inventory; _nodes.AddRange(nodes); _save = PrototypeGatheringSaveService.Load();
            _inventory.TryAdd(ResourceType.WildPetal, _save.petals, out _, out _); _inventory.TryAdd(ResourceType.Timber, _save.timber, out _, out _); _inventory.TryAdd(ResourceType.Stone, _save.stone, out _, out _);
            foreach (GatheringNode node in _nodes) { if (_save.depletedNodeIds.Contains(node.Id)) node.RestoreDepleted(); node.Completed += OnNodeCompleted; }
        }
        private void Update()
        {
            if (_player.IsPaused || !_input.InteractHeld || Time.time < _nextStepTime) return;
            GatheringNode? target = FindNearest(); if (target == null || !target.TryGather()) return; _nextStepTime = Time.time + StepInterval;
        }
        private GatheringNode? FindNearest()
        { GatheringNode? nearest = null; float best = Range * Range; foreach (GatheringNode node in _nodes) { if (!node.IsAvailable) continue; float d = (node.transform.position - _player.transform.position).sqrMagnitude; if (d <= best) { best = d; nearest = node; } } return nearest; }
        private void OnNodeCompleted(GatheringNode node)
        {
            _inventory.TryAdd(node.Resource, 1, out _, out _); _save.depletedNodeIds.Add(node.Id);
            _save.petals = _inventory.GetCount(ResourceType.WildPetal); _save.timber = _inventory.GetCount(ResourceType.Timber); _save.stone = _inventory.GetCount(ResourceType.Stone); PrototypeGatheringSaveService.Save(_save);
        }
        private void OnDestroy() { foreach (GatheringNode node in _nodes) if (node != null) node.Completed -= OnNodeCompleted; }
    }
}

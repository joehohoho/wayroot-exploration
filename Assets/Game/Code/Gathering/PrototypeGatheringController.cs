using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Input;
using Wayroot.Inventory;
using UnityEngine;

namespace Wayroot.Gathering
{
    public sealed class PrototypeGatheringController : MonoBehaviour
    {
        private const float Range = 2.8f;
        private const float StepInterval = 0.45f;
        private readonly List<GatheringNode> _nodes = new();
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private InventoryState _inventory = null!;
        private float _nextStepTime;

        public int GetCount(ResourceType resource) => _inventory.GetCount(resource);

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, InventoryState inventory, IEnumerable<GatheringNode> nodes)
        {
            _input = input; _player = player; _inventory = inventory; _nodes.AddRange(nodes);
            foreach (GatheringNode node in _nodes) node.Completed += OnNodeCompleted;
        }

        private void Update()
        {
            if (_player.IsPaused || !_input.InteractHeld || Time.time < _nextStepTime) return;
            GatheringNode? target = FindNearest();
            if (target == null || !target.TryGather()) return;
            _nextStepTime = Time.time + StepInterval;
        }

        private GatheringNode? FindNearest()
        {
            GatheringNode? nearest = null; float best = Range * Range;
            foreach (GatheringNode node in _nodes)
            {
                if (!node.IsAvailable) continue;
                float distance = (node.transform.position - _player.transform.position).sqrMagnitude;
                if (distance <= best) { best = distance; nearest = node; }
            }
            return nearest;
        }

        private void OnNodeCompleted(GatheringNode node) => _inventory.TryAdd(node.Resource, 1, out _, out _);
        private void OnDestroy() { foreach (GatheringNode node in _nodes) if (node != null) node.Completed -= OnNodeCompleted; }
    }
}

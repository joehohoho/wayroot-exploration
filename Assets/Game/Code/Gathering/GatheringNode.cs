using System;
using Wayroot.Inventory;
using UnityEngine;

namespace Wayroot.Gathering
{
    public sealed class GatheringNode : MonoBehaviour
    {
        private string _id = string.Empty;
        private ResourceType _resource;
        private int _requiredSteps;
        private int _steps;
        private Renderer _renderer = null!;
        public string Id => _id;
        public bool IsAvailable => isActiveAndEnabled && _renderer.enabled && _steps < _requiredSteps;
        public ResourceType Resource => _resource;
        public event Action<GatheringNode>? Completed;

        public void Configure(string id, ResourceType resource, int requiredSteps, Renderer nodeRenderer)
        { _id = id; _resource = resource; _requiredSteps = requiredSteps; _renderer = nodeRenderer; }
        public void RestoreDepleted() { _steps = _requiredSteps; _renderer.enabled = false; }
        public bool TryGather()
        {
            if (!GatheringRules.TryAdvance(_steps, _requiredSteps, out _steps, out bool complete)) return false;
            if (complete) { _renderer.enabled = false; Completed?.Invoke(this); }
            return true;
        }
    }
}

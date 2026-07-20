using System;
using Wayroot.Inventory;
using UnityEngine;

namespace Wayroot.Gathering
{
    public sealed class GatheringNode : MonoBehaviour
    {
        private string _id = string.Empty;
        private string _displayName = string.Empty;
        private string _resourceName = string.Empty;
        private ResourceType _resource;
        private int _requiredSteps;
        private int _steps;
        private long _renewalDeadlineUtcTicks;
        private Renderer _renderer = null!;
        private TextMesh? _worldLabel;

        public string Id => _id;
        public int Steps => _steps;
        public int RequiredSteps => _requiredSteps;
        public bool IsAvailable => isActiveAndEnabled && _renderer.enabled && _steps < _requiredSteps;
        public ResourceType Resource => _resource;
        public bool IsRenewing => _renewalDeadlineUtcTicks > 0;
        public long RenewalDeadlineUtcTicks => _renewalDeadlineUtcTicks;
        public event Action<GatheringNode>? Completed;

        public void Configure(string id, ResourceType resource, int requiredSteps, Renderer nodeRenderer)
        {
            _id = id;
            _resource = resource;
            _requiredSteps = requiredSteps;
            _renderer = nodeRenderer;
        }

        public void SetWorldLabel(TextMesh worldLabel, string displayName, string resourceName)
        {
            _worldLabel = worldLabel;
            _displayName = displayName;
            _resourceName = resourceName;
            RefreshVisual(DateTime.UtcNow);
        }

        public void RestoreDepleted(long renewalDeadlineUtcTicks)
        {
            _steps = _requiredSteps;
            _renewalDeadlineUtcTicks = renewalDeadlineUtcTicks;
            _renderer.enabled = false;
            RefreshVisual(DateTime.UtcNow);
        }

        public bool RefreshRenewal(DateTime utcNow)
        {
            if (!IsRenewing || !RenewalRules.IsDue(_renewalDeadlineUtcTicks, utcNow))
            {
                RefreshVisual(utcNow);
                return false;
            }

            _steps = 0;
            _renewalDeadlineUtcTicks = 0;
            _renderer.enabled = true;
            RefreshVisual(utcNow);
            return true;
        }

        public bool TryGather()
        {
            if (!GatheringRules.TryAdvance(_steps, _requiredSteps, out _steps, out bool complete)) return false;
            if (complete)
            {
                _renderer.enabled = false;
                Completed?.Invoke(this);
            }

            return true;
        }

        public void StartRenewal(long renewalDeadlineUtcTicks)
        {
            _steps = _requiredSteps;
            _renewalDeadlineUtcTicks = renewalDeadlineUtcTicks;
            _renderer.enabled = false;
            RefreshVisual(DateTime.UtcNow);
        }

        public void RefreshVisual(DateTime utcNow)
        {
            if (_worldLabel == null) return;
            _worldLabel.text = IsRenewing
                ? $"{_displayName}\nRENEWING {RenewalRules.FormatRemaining(_renewalDeadlineUtcTicks, utcNow)}"
                : IsAvailable
                    ? $"{_displayName}\n{_resourceName}"
                    : $"{_displayName}\nDEPLETED";
        }
    }
}

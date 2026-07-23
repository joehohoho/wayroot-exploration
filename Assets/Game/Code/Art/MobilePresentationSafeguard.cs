using UnityEngine;

namespace Wayroot.Art
{
    /// <summary>Automatically trades only existing decorative ambience for readability after sustained constrained-frame conditions.</summary>
    public sealed class MobilePresentationSafeguard : MonoBehaviour
    {
        private const float WarmupSeconds = 3f;
        private EnvironmentalAmbiencePresentation _ambience = null!;
        private MobilePresentationMonitor _monitor = null!;
        private float _monitoringStartsAt;

        public MobilePresentationSafeguardState State => _monitor == null ? MobilePresentationSafeguardState.Full : _monitor.State;

        public void Configure(EnvironmentalAmbiencePresentation ambience)
        {
            _ambience = ambience;
            _monitor = new MobilePresentationMonitor();
            _monitoringStartsAt = Time.unscaledTime + WarmupSeconds;
            ApplyState();
        }

        public void ReportFrameDuration(float duration)
        {
            if (_monitor == null || !_monitor.ReportFrameDuration(duration)) return;
            ApplyState();
        }

        private void Update()
        {
            if (_monitor == null || Time.unscaledTime < _monitoringStartsAt) return;
            ReportFrameDuration(Time.unscaledDeltaTime);
        }

        private void ApplyState()
        {
            _ambience.SetOptionalEffectsEnabled(State == MobilePresentationSafeguardState.Full);
        }
    }
}

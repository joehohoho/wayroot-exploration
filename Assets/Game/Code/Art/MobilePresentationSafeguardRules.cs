namespace Wayroot.Art
{
    /// <summary>States for the automatic visual-only device safeguard; gameplay and UI are never reduced.</summary>
    public enum MobilePresentationSafeguardState
    {
        Full,
        Reduced
    }

    /// <summary>Pure thresholds and hysteresis for reducing optional presentation after sustained frame pressure.</summary>
    public static class MobilePresentationSafeguardRules
    {
        public const float SlowFrameThresholdSeconds = 1f / 30f;
        public const float FastFrameThresholdSeconds = 1f / 45f;
        public const int SlowFrameSamplesToReduce = 30;
        public const int FastFrameSamplesToRestore = 180;

        public static bool IsSlowFrame(float duration) => duration >= SlowFrameThresholdSeconds;

        public static bool IsFastFrame(float duration) => duration <= FastFrameThresholdSeconds;

        public static bool KeepsRequiredGameplayVisible(MobilePresentationSafeguardState state) => true;

        public static bool KeepsTouchUiVisible(MobilePresentationSafeguardState state) => true;
    }

    /// <summary>Allocation-free monitor with asymmetric thresholds so a device does not visibly oscillate around a frame-time boundary.</summary>
    public sealed class MobilePresentationMonitor
    {
        private int _slowSamples;
        private int _fastSamples;

        public MobilePresentationMonitor(MobilePresentationSafeguardState initialState = MobilePresentationSafeguardState.Full)
        {
            State = initialState;
        }

        public MobilePresentationSafeguardState State { get; private set; }

        public bool ReportFrameDuration(float duration)
        {
            if (duration <= 0f) return false;

            if (State == MobilePresentationSafeguardState.Full)
            {
                _slowSamples = MobilePresentationSafeguardRules.IsSlowFrame(duration) ? _slowSamples + 1 : 0;
                if (_slowSamples < MobilePresentationSafeguardRules.SlowFrameSamplesToReduce) return false;
                State = MobilePresentationSafeguardState.Reduced;
                _slowSamples = 0;
                return true;
            }

            _fastSamples = MobilePresentationSafeguardRules.IsFastFrame(duration) ? _fastSamples + 1 : 0;
            if (_fastSamples < MobilePresentationSafeguardRules.FastFrameSamplesToRestore) return false;
            State = MobilePresentationSafeguardState.Full;
            _fastSamples = 0;
            return true;
        }
    }
}

using System;

namespace Wayroot.Gathering
{
    /// <summary>Pure, UTC-based timing rules for the short Phase 11 resource renewal loop.</summary>
    public static class RenewalRules
    {
        public const int PrototypeIntervalSeconds = 20;

        public static long CreateDeadlineUtcTicks(DateTime utcNow)
        {
            return utcNow.ToUniversalTime().AddSeconds(PrototypeIntervalSeconds).Ticks;
        }

        public static bool IsDue(long renewalDeadlineUtcTicks, DateTime utcNow)
        {
            return renewalDeadlineUtcTicks > 0 && utcNow.ToUniversalTime().Ticks >= renewalDeadlineUtcTicks;
        }

        public static string FormatRemaining(long renewalDeadlineUtcTicks, DateTime utcNow)
        {
            double remainingSeconds = Math.Max(0d, new TimeSpan(renewalDeadlineUtcTicks - utcNow.ToUniversalTime().Ticks).TotalSeconds);
            int roundedSeconds = (int)Math.Ceiling(remainingSeconds);
            return $"{roundedSeconds / 60}:{roundedSeconds % 60:D2}";
        }
    }
}

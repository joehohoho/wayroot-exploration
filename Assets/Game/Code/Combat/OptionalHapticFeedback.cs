namespace Wayroot.Combat
{
    /// <summary>
    /// Optional mobile haptic seam. This prototype intentionally uses no platform API, so Windows and
    /// unsupported devices safely retain the feedback call without requiring conditional plugins.
    /// </summary>
    public sealed class OptionalHapticFeedback
    {
        public CombatHapticCue LastCue { get; private set; }
        public bool UsesPlatformApi => false;

        public void Pulse(CombatHapticCue cue)
        {
            LastCue = cue;
        }
    }
}

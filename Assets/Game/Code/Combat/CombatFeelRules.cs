namespace Wayroot.Combat
{
    public enum CombatHapticCue
    {
        AttackContact,
        PlayerDamaged,
        EnemyDefeated,
        PlayerRespawn
    }

    public readonly struct HapticProfile
    {
        public HapticProfile(int durationMilliseconds, float intensity)
        {
            DurationMilliseconds = durationMilliseconds;
            Intensity = intensity;
        }

        public int DurationMilliseconds { get; }
        public float Intensity { get; }
    }

    /// <summary>Bounded presentation tuning only; it has no combat-rule or platform dependency.</summary>
    public static class CombatFeelRules
    {
        public const int MaximumHapticDurationMilliseconds = 45;

        public static HapticProfile GetHapticProfile(CombatHapticCue cue)
        {
            return cue switch
            {
                CombatHapticCue.AttackContact => new HapticProfile(18, 0.25f),
                CombatHapticCue.PlayerDamaged => new HapticProfile(32, 0.60f),
                CombatHapticCue.EnemyDefeated => new HapticProfile(40, 0.75f),
                CombatHapticCue.PlayerRespawn => new HapticProfile(28, 0.42f),
                _ => new HapticProfile(0, 0f)
            };
        }
    }
}

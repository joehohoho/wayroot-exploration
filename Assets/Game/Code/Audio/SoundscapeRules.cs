namespace Wayroot.Audio
{
    public enum SoundscapeCue
    {
        Ui,
        Gather,
        Renewal,
        CombatHit,
        Defeat,
        ShelterRest,
        WayrootRestore,
        MosslingGuide,
        BloomwellRestore,
        Dodge,
        EnemyAnticipate,
        AttackContact,
        PlayerDamaged,
        PlayerRespawn,
        EnemyRespawn
    }

    public readonly struct SoundscapeProfile
    {
        public SoundscapeProfile(float frequencyHz, float durationSeconds, float volume, float secondFrequencyHz = 0f)
        {
            FrequencyHz = frequencyHz;
            DurationSeconds = durationSeconds;
            Volume = volume;
            SecondFrequencyHz = secondFrequencyHz;
        }

        public float FrequencyHz { get; }
        public float DurationSeconds { get; }
        public float Volume { get; }
        public float SecondFrequencyHz { get; }
    }

    /// <summary>Deterministic, compact tuning for original procedural sound cues.</summary>
    public static class SoundscapeRules
    {
        public const bool DefaultSoundEnabled = true;
        public const float MaximumOneShotDurationSeconds = 0.65f;
        public const float MaximumOneShotVolume = 0.34f;

        public static bool IsAudible(bool soundEnabled, SoundscapeCue cue) => soundEnabled;

        public static SoundscapeProfile GetProfile(SoundscapeCue cue)
        {
            return cue switch
            {
                SoundscapeCue.Gather => new SoundscapeProfile(330f, 0.20f, 0.22f, 495f),
                SoundscapeCue.Renewal => new SoundscapeProfile(523f, 0.34f, 0.20f, 659f),
                SoundscapeCue.CombatHit => new SoundscapeProfile(196f, 0.13f, 0.22f, 247f),
                SoundscapeCue.Defeat => new SoundscapeProfile(294f, 0.42f, 0.27f, 440f),
                SoundscapeCue.ShelterRest => new SoundscapeProfile(262f, 0.46f, 0.26f, 392f),
                SoundscapeCue.WayrootRestore => new SoundscapeProfile(440f, 0.62f, 0.30f, 660f),
                SoundscapeCue.MosslingGuide => new SoundscapeProfile(587f, 0.18f, 0.16f, 740f),
                SoundscapeCue.BloomwellRestore => new SoundscapeProfile(523f, 0.65f, 0.32f, 784f),
                SoundscapeCue.Dodge => new SoundscapeProfile(659f, 0.16f, 0.18f, 880f),
                SoundscapeCue.EnemyAnticipate => new SoundscapeProfile(174f, 0.24f, 0.18f, 220f),
                SoundscapeCue.AttackContact => new SoundscapeProfile(220f, 0.10f, 0.20f, 330f),
                SoundscapeCue.PlayerDamaged => new SoundscapeProfile(148f, 0.16f, 0.24f, 185f),
                SoundscapeCue.PlayerRespawn => new SoundscapeProfile(392f, 0.32f, 0.22f, 523f),
                SoundscapeCue.EnemyRespawn => new SoundscapeProfile(247f, 0.20f, 0.18f, 294f),
                _ => new SoundscapeProfile(392f, 0.10f, 0.16f)
            };
        }
    }
}

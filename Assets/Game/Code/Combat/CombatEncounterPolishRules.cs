namespace Wayroot.Combat
{
    /// <summary>Presentation-only timings for Phase 26 combat polish. These values never alter combat tuning.</summary>
    public static class CombatEncounterPolishRules
    {
        public const float PlayerTrailSeconds = 0.16f;
        public const float PlayerImpactSeconds = 0.20f;
        public const float PlayerContactMarkerSeconds = 0.34f;
        public const float EnemyHitFlashSeconds = 0.18f;
        public const float EnemyRespawnCueSeconds = 0.70f;

        public static bool IsActive(float elapsed, float duration)
        {
            return elapsed >= 0f && elapsed < duration;
        }
    }
}

namespace Wayroot.Combat
{
    /// <summary>Static Phase 13 encounter tuning; runtime systems consume this profile without adding a new combat loop.</summary>
    public static class ThornGuardianRules
    {
        public const int PracticeSlimeHealth = 5;
        public const int PracticeSlimeContactDamage = 1;

        public static EnemyCombatProfile Profile => new("THORN GUARDIAN", 8, 2, 15f, 1.8f, 6.5f);

        public static bool CanEnterRestoredGrove(bool wayrootRestored) => wayrootRestored;
    }
}

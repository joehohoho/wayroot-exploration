namespace Wayroot.Exploration
{
    /// <summary>Defines the one-time Phase 17 route into the compact Moonlit Glade.</summary>
    public static class MoonlitGladeRules
    {
        public const string LockedStatus = "MOONLIT GLADE PATH SEALED — DEFEAT THORN GUARDIAN";
        public const string UnlockedStatus = "MOONLIT GLADE OPEN — FOLLOW THE VIOLET PATH";

        public static bool CanEnter(bool thornGuardianDefeated) => thornGuardianDefeated;
    }
}

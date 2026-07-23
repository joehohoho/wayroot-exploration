using UnityEngine;

namespace Wayroot.Building
{
    /// <summary>Presentation-only state and palette rules for the existing shelter loop.</summary>
    public static class ShelterCozyArtRules
    {
        public const float RestPulseSeconds = 0.62f;
        public static readonly Color LanternColor = new(1f, 0.72f, 0.28f, 1f);
        public static readonly Color HearthColor = new(1f, 0.38f, 0.16f, 1f);

        public static string GetStateLabel(bool built, bool activeReturnPoint)
        {
            if (!built) return "SHELTER\nBUILD PLOT";
            return activeReturnPoint ? "SHELTER\nACTIVE HOME" : "SHELTER\nREST HERE";
        }

        public static Color GetPlotColor(bool built)
        {
            return built ? new Color(0.30f, 0.62f, 0.35f) : new Color(0.77f, 0.58f, 0.22f);
        }

        public static bool IsRestPulseActive(float elapsed)
        {
            return elapsed >= 0f && elapsed < RestPulseSeconds;
        }
    }
}

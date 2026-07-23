using UnityEngine;

namespace Wayroot.Art
{
    /// <summary>Deterministic visual-only palette and scale limits for the public-playtest finish pass.</summary>
    public static class PhaseThirtySixVisualFinishRules
    {
        public const int MinimumVisualCount = 34;
        public const float MaximumDecorationHeight = 2.9f;
        public const float MaximumDecorationRadius = 2.4f;

        public static Color GetAccent(ExplorationRegion region)
        {
            return region switch
            {
                ExplorationRegion.Sunmeadow => new Color(1f, 0.74f, 0.34f),
                ExplorationRegion.RestoredGrove => new Color(0.56f, 0.88f, 0.36f),
                ExplorationRegion.MoonlitGlade => new Color(0.72f, 0.58f, 1f),
                ExplorationRegion.Bloomwell => new Color(0.42f, 0.94f, 0.82f),
                _ => Color.white
            };
        }

        public static bool IsSafeDecoration(Vector3 scale)
        {
            return scale.x > 0f && scale.y > 0f && scale.z > 0f &&
                   scale.y <= MaximumDecorationHeight &&
                   Mathf.Max(scale.x, scale.z) <= MaximumDecorationRadius;
        }
    }
}

using UnityEngine;

namespace Wayroot.Art
{
    public enum ExplorationRegion
    {
        Sunmeadow,
        RestoredGrove,
        MoonlitGlade,
        Bloomwell
    }

    public readonly struct RegionCompositionProfile
    {
        public RegionCompositionProfile(ExplorationRegion region, Color primaryColor, Color accentColor, string landmarkName)
        {
            Region = region;
            PrimaryColor = primaryColor;
            AccentColor = accentColor;
            LandmarkName = landmarkName;
        }

        public ExplorationRegion Region { get; }
        public Color PrimaryColor { get; }
        public Color AccentColor { get; }
        public string LandmarkName { get; }
    }

    /// <summary>Owns deterministic palette and safe-dressing gates for the existing exploration route.</summary>
    public static class ExplorationCompositionRules
    {
        private const float FocalZoneRadius = 1.7f;

        private static readonly RegionCompositionProfile[] Profiles =
        {
            new(ExplorationRegion.Sunmeadow, new Color(0.96f, 0.68f, 0.28f), new Color(1f, 0.86f, 0.48f), "Sunmeadow Creek"),
            new(ExplorationRegion.RestoredGrove, new Color(0.25f, 0.56f, 0.27f), new Color(0.68f, 0.86f, 0.32f), "Thorn Guardian"),
            new(ExplorationRegion.MoonlitGlade, new Color(0.34f, 0.34f, 0.76f), new Color(0.76f, 0.60f, 1f), "Moonlit Bloomwell"),
            new(ExplorationRegion.Bloomwell, new Color(0.42f, 0.78f, 0.94f), new Color(0.48f, 1f, 0.82f), "Bloomwell"),
        };

        public static readonly Vector3[] SunmeadowDressingPositions =
        {
            new(-7.2f, 0f, 1.5f), new(-4.8f, 0f, -6.6f), new(6.8f, 0f, 4.5f), new(7.8f, 0f, -4.4f)
        };

        public static readonly Vector3[] GroveDressingPositions =
        {
            new(-2.15f, 0f, 1.4f), new(2.2f, 0f, 1.25f), new(-2.25f, 0f, -1.4f), new(2.15f, 0f, -1.4f)
        };

        public static readonly Vector3[] GladeDressingPositions =
        {
            new(-2.1f, 0f, 1.55f), new(2.15f, 0f, 1.35f), new(-2.05f, 0f, -1.45f), new(2.1f, 0f, -1.5f)
        };

        public static RegionCompositionProfile[] AllProfiles => (RegionCompositionProfile[])Profiles.Clone();

        public static RegionCompositionProfile GetProfile(ExplorationRegion region)
        {
            for (int index = 0; index < Profiles.Length; index++)
            {
                if (Profiles[index].Region == region) return Profiles[index];
            }

            throw new System.ArgumentOutOfRangeException(nameof(region), region, "Existing exploration region profile was not found.");
        }

        public static bool IsOutsideFocalZone(Vector3 localPosition)
        {
            return new Vector2(localPosition.x, localPosition.z).sqrMagnitude >= FocalZoneRadius * FocalZoneRadius;
        }
    }
}

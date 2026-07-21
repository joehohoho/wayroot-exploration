using UnityEngine;

namespace Wayroot.Presentation
{
    /// <summary>Central alpha presentation constraints shared by runtime composition and focused coverage.</summary>
    public static class AlphaPresentationRules
    {
        public const float MaximumMarkerCharacterSize = 0.035f;
        public const float MaximumMarkerWorldScale = 0.72f;

        public static readonly HudLayoutProfile DefaultHud = new(0.25f, 0.42f, 0.25f, 0.115f, 0.22f);
        public static readonly CameraPresentationProfile DefaultCamera = new(8.5f, 13.5f, 10.5f, new Vector3(0f, 1.15f, 0.85f));

        public static WorldMarkerProfile CreateMarker(string name)
        {
            string displayName = string.IsNullOrWhiteSpace(name) ? "MARKER" : name.Trim().ToUpperInvariant();
            return new WorldMarkerProfile(displayName, MaximumMarkerCharacterSize, MaximumMarkerWorldScale);
        }
    }

    public readonly struct WorldMarkerProfile
    {
        public WorldMarkerProfile(string displayName, float characterSize, float worldScale)
        {
            DisplayName = displayName;
            CharacterSize = characterSize;
            WorldScale = worldScale;
        }

        public string DisplayName { get; }
        public float CharacterSize { get; }
        public float WorldScale { get; }
    }

    public readonly struct HudLayoutProfile
    {
        public HudLayoutProfile(float leftWidth, float centralWidth, float rightWidth, float topCardHeight, float bottomControlClearance)
        {
            LeftWidth = leftWidth;
            CentralWidth = centralWidth;
            RightWidth = rightWidth;
            TopCardHeight = topCardHeight;
            BottomControlClearance = bottomControlClearance;
        }

        public float LeftWidth { get; }
        public float CentralWidth { get; }
        public float RightWidth { get; }
        public float TopCardHeight { get; }
        public float BottomControlClearance { get; }
    }

    public readonly struct CameraPresentationProfile
    {
        public CameraPresentationProfile(float minimumZoom, float maximumZoom, float startingZoom, Vector3 focusOffset)
        {
            MinimumZoom = minimumZoom;
            MaximumZoom = maximumZoom;
            StartingZoom = startingZoom;
            FocusOffset = focusOffset;
        }

        public float MinimumZoom { get; }
        public float MaximumZoom { get; }
        public float StartingZoom { get; }
        public Vector3 FocusOffset { get; }
    }
}

using UnityEngine;

namespace Wayroot.UI
{
    /// <summary>Stores the compact visual-comfort preferences outside the prototype progression save schema.</summary>
    public sealed class AccessibilityPreferences : MonoBehaviour
    {
        private const string ReducedFlashKey = "wayroot.accessibility.reducedFlash";
        private const string ReducedMotionKey = "wayroot.accessibility.reducedMotion";

        public bool ReducedFlash { get; private set; }
        public bool ReducedMotion { get; private set; }

        public void Configure()
        {
            ReducedFlash = PlayerPrefs.GetInt(ReducedFlashKey, AccessibilityRules.DefaultReducedFlash ? 1 : 0) == 1;
            ReducedMotion = PlayerPrefs.GetInt(ReducedMotionKey, AccessibilityRules.DefaultReducedMotion ? 1 : 0) == 1;
        }

        public void ToggleReducedFlash() => SetReducedFlash(!ReducedFlash);

        public void ToggleReducedMotion() => SetReducedMotion(!ReducedMotion);

        public void SetReducedFlash(bool enabled)
        {
            ReducedFlash = enabled;
            PlayerPrefs.SetInt(ReducedFlashKey, enabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void SetReducedMotion(bool enabled)
        {
            ReducedMotion = enabled;
            PlayerPrefs.SetInt(ReducedMotionKey, enabled ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void ResetDefaults()
        {
            SetReducedFlash(AccessibilityRules.DefaultReducedFlash);
            SetReducedMotion(AccessibilityRules.DefaultReducedMotion);
        }
    }

    /// <summary>Pure, bounded presentation tuning for the optional visual-comfort preferences.</summary>
    public static class AccessibilityRules
    {
        public const bool DefaultReducedFlash = false;
        public const bool DefaultReducedMotion = false;
        public const float ReducedFlashDurationMultiplier = 0.45f;
        public const float ReducedMotionAmplitudeMultiplier = 0.28f;

        public static float GetFlashDuration(float standardDuration, bool reducedFlash) =>
            reducedFlash ? standardDuration * ReducedFlashDurationMultiplier : standardDuration;

        public static float GetMotionAmplitude(float standardAmplitude, bool reducedMotion) =>
            reducedMotion ? standardAmplitude * ReducedMotionAmplitudeMultiplier : standardAmplitude;
    }
}

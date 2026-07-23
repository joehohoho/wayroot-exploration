using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Connects one compact visual-comfort toggle to its persisted preference and readable HUD label.</summary>
    public sealed class AccessibilityPreferenceButton : MonoBehaviour
    {
        public enum PreferenceKind
        {
            ReducedFlash,
            ReducedMotion
        }

        [SerializeField] private Text label = null!;
        [SerializeField] private AccessibilityPreferences preferences = null!;
        [SerializeField] private PreferenceKind preferenceKind;

        public void Configure(Text text, AccessibilityPreferences accessibilityPreferences, PreferenceKind kind)
        {
            label = text;
            preferences = accessibilityPreferences;
            preferenceKind = kind;
            GetComponent<Button>().onClick.AddListener(Toggle);
            RefreshLabel();
        }

        private void Toggle()
        {
            if (preferenceKind == PreferenceKind.ReducedFlash) preferences.ToggleReducedFlash();
            else preferences.ToggleReducedMotion();
            RefreshLabel();
        }

        private void RefreshLabel()
        {
            bool enabled = preferenceKind == PreferenceKind.ReducedFlash ? preferences.ReducedFlash : preferences.ReducedMotion;
            label.text = preferenceKind == PreferenceKind.ReducedFlash
                ? enabled ? "FLASH LESS: ON" : "FLASH LESS: OFF"
                : enabled ? "MOTION LESS: ON" : "MOTION LESS: OFF";
        }
    }
}

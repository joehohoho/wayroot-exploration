using UnityEngine;

namespace Wayroot.UI
{
    public sealed class SafeAreaLayout : MonoBehaviour
    {
        [SerializeField] private RectTransform safeArea = null!;
        [SerializeField] private RectTransform joystick = null!;
        [SerializeField] private RectTransform actionButton = null!;
        [SerializeField] private bool leftHanded;
        private Rect _lastSafeArea;

        public void Configure(RectTransform safeAreaPanel, RectTransform joystickPanel, RectTransform actionPanel)
        {
            safeArea = safeAreaPanel;
            joystick = joystickPanel;
            actionButton = actionPanel;
            ApplySafeArea();
            ApplyHandedness();
        }

        public void SetLeftHanded(bool value)
        {
            leftHanded = value;
            ApplyHandedness();
        }

        private void Update()
        {
            if (_lastSafeArea != Screen.safeArea)
            {
                ApplySafeArea();
            }
        }

        private void ApplySafeArea()
        {
            Rect safe = Screen.safeArea;
            _lastSafeArea = safe;
            Vector2 min = safe.position;
            Vector2 max = safe.position + safe.size;
            min.x /= Screen.width; min.y /= Screen.height;
            max.x /= Screen.width; max.y /= Screen.height;
            safeArea.anchorMin = min;
            safeArea.anchorMax = max;
        }

        private void ApplyHandedness()
        {
            joystick.anchorMin = joystick.anchorMax = leftHanded ? new Vector2(1f, 0f) : new Vector2(0f, 0f);
            joystick.pivot = leftHanded ? new Vector2(1f, 0f) : new Vector2(0f, 0f);
            actionButton.anchorMin = actionButton.anchorMax = leftHanded ? new Vector2(0f, 0f) : new Vector2(1f, 0f);
            actionButton.pivot = leftHanded ? new Vector2(0f, 0f) : new Vector2(1f, 0f);
            joystick.anchoredPosition = leftHanded ? new Vector2(-56f, 56f) : new Vector2(56f, 56f);
            actionButton.anchoredPosition = leftHanded ? new Vector2(56f, 56f) : new Vector2(-56f, 56f);
        }
    }
}

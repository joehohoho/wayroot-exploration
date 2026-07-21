using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Gathering;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.Guidance
{
    /// <summary>Displays the current save-derived journey hint and a quiet firefly only when its existing target is off-screen.</summary>
    public sealed class JourneyGuidanceController : MonoBehaviour
    {
        private const float RefreshInterval = 0.25f;
        private const float EdgeInset = 0.13f;

        private readonly Dictionary<JourneyTarget, Transform> _targets = new();
        private PrototypePlayerController _player = null!;
        private UnityEngine.Camera _camera = null!;
        private Text _statusText = null!;
        private RectTransform _pointer = null!;
        private float _nextRefreshTime;

        public JourneyGuidanceState CurrentState { get; private set; }

        public void Configure(
            PrototypePlayerController player,
            UnityEngine.Camera sceneCamera,
            Text statusText,
            RectTransform pointer,
            Transform resource,
            Transform merchant,
            Transform shelter,
            Transform wayroot,
            Transform guardian,
            Transform bloomwell)
        {
            _player = player;
            _camera = sceneCamera;
            _statusText = statusText;
            _pointer = pointer;
            _targets[JourneyTarget.Resource] = resource;
            _targets[JourneyTarget.Merchant] = merchant;
            _targets[JourneyTarget.Shelter] = shelter;
            _targets[JourneyTarget.Wayroot] = wayroot;
            _targets[JourneyTarget.Guardian] = guardian;
            _targets[JourneyTarget.Bloomwell] = bloomwell;
            RefreshNow();
        }

        public void RefreshNow()
        {
            CurrentState = JourneyGuidanceRules.Select(PrototypeGatheringSaveService.Load());
            _statusText.text = CurrentState.Status;
            UpdatePointer();
        }

        private void Update()
        {
            if (Time.unscaledTime >= _nextRefreshTime)
            {
                _nextRefreshTime = Time.unscaledTime + RefreshInterval;
                RefreshNow();
            }
            else
            {
                UpdatePointer();
            }
        }

        private void UpdatePointer()
        {
            if (!CurrentState.HasPointer || !_targets.TryGetValue(CurrentState.Target, out Transform target) || target == null)
            {
                _pointer.gameObject.SetActive(false);
                return;
            }

            Vector3 viewport = _camera.WorldToViewportPoint(target.position + Vector3.up * 0.7f);
            bool offScreen = viewport.z <= 0f || viewport.x < 0f || viewport.x > 1f || viewport.y < 0f || viewport.y > 1f;
            _pointer.gameObject.SetActive(offScreen);
            if (!offScreen) return;

            if (viewport.z <= 0f)
            {
                viewport.x = 1f - viewport.x;
                viewport.y = 1f - viewport.y;
            }

            Vector2 centered = new(viewport.x - 0.5f, viewport.y - 0.5f);
            if (centered.sqrMagnitude < 0.001f) centered = Vector2.up;
            centered.Normalize();
            float scale = Mathf.Min((0.5f - EdgeInset) / Mathf.Abs(centered.x == 0f ? 1f : centered.x), (0.5f - EdgeInset) / Mathf.Abs(centered.y == 0f ? 1f : centered.y));
            _pointer.anchorMin = _pointer.anchorMax = new Vector2(0.5f, 0.5f);
            _pointer.anchoredPosition = centered * scale * new Vector2(_pointer.parent.GetComponent<RectTransform>().rect.width, _pointer.parent.GetComponent<RectTransform>().rect.height);
        }
    }
}

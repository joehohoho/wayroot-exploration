using Wayroot.Presentation;
using UnityEngine;

namespace Wayroot.UI
{
    /// <summary>Compact screen-horizontal marker that hides outside the clean focus frame.</summary>
    [RequireComponent(typeof(TextMesh))]
    public sealed class WorldIdentifier : MonoBehaviour
    {
        [SerializeField] private Transform target = null!;
        [SerializeField] private Vector3 localOffset;
        [SerializeField] private UnityEngine.Camera sceneCamera = null!;
        private TextMesh _text = null!;
        private MeshRenderer _renderer = null!;

        public void Configure(Transform trackedTarget, Vector3 offset, UnityEngine.Camera camera)
        {
            target = trackedTarget;
            localOffset = offset;
            sceneCamera = camera;
            _text = GetComponent<TextMesh>();
            _renderer = GetComponent<MeshRenderer>();
            _text.fontSize = 32;
            _text.characterSize = AlphaPresentationRules.MaximumMarkerCharacterSize;
            _text.anchor = TextAnchor.MiddleCenter;
            _text.alignment = TextAlignment.Center;
            _text.fontStyle = FontStyle.Bold;
        }

        private void LateUpdate()
        {
            if (target == null || sceneCamera == null)
            {
                return;
            }

            if (_text == null)
            {
                _text = GetComponent<TextMesh>();
                _renderer = GetComponent<MeshRenderer>();
            }

            // Controllers may refresh a semantic status; keep its world presentation to a single compact line.
            if (_text.text.Contains("\n")) _text.text = _text.text.Replace("\n", "  •  ");
            transform.position = target.position + localOffset;
            transform.rotation = Quaternion.LookRotation(-sceneCamera.transform.forward, sceneCamera.transform.up);
            float distance = Vector3.Distance(sceneCamera.transform.position, transform.position);
            float scale = Mathf.Clamp(distance * 0.022f, 0.38f, AlphaPresentationRules.MaximumMarkerWorldScale);
            transform.localScale = Vector3.one * scale;

            Vector3 viewport = sceneCamera.WorldToViewportPoint(transform.position);
            bool insideFocusFrame = viewport.z > 0f && viewport.x > 0.08f && viewport.x < 0.92f && viewport.y > 0.20f && viewport.y < 0.84f;
            _renderer.enabled = target.gameObject.activeInHierarchy && insideFocusFrame;
        }
    }
}

using Wayroot.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Wayroot.Camera
{
    public sealed class TopDownCameraController : MonoBehaviour
    {
        [SerializeField] private Transform target = null!;
        [SerializeField] private PrototypeInputReader inputReader = null!;
        [SerializeField] private float followSmoothTime = 0.16f;
        [SerializeField] private float minimumZoom = 8.5f;
        [SerializeField] private float maximumZoom = 13.5f;
        [SerializeField] private float startingZoom = 10.5f;
        [SerializeField] private float zoomSpeed = 0.008f;
        [SerializeField] private Vector3 viewDirection = new(0f, 9.5f, -8.5f);
        [SerializeField] private Vector3 focusOffset = new(0f, 1.15f, 0.85f);
        private Vector3 _velocity;
        private float _zoom;
        private bool _isPaused;

        public float CurrentZoom => _zoom;
        public Transform Target => target;

        private void Awake()
        {
            _zoom = CameraZoomRules.Clamp(startingZoom, minimumZoom, maximumZoom);
            transform.rotation = Quaternion.LookRotation(new Vector3(0f, -viewDirection.y, -viewDirection.z), Vector3.up);
        }

        private void LateUpdate()
        {
            if (_isPaused)
            {
                return;
            }

            float pinchDelta = ReadPinchDelta();
            _zoom = CameraZoomRules.Clamp(_zoom - ((inputReader.ZoomDelta + pinchDelta) * zoomSpeed), minimumZoom, maximumZoom);
            inputReader.ClearVirtualZoom();
            Vector3 offset = viewDirection.normalized * _zoom;
            Vector3 desiredPosition = target.position + focusOffset + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, followSmoothTime);
        }

        public void SetPaused(bool value) => _isPaused = value;

        public void Configure(Transform followTarget, PrototypeInputReader reader)
        {
            target = followTarget;
            inputReader = reader;
        }

        private static float ReadPinchDelta()
        {
            if (Touchscreen.current == null || Touchscreen.current.touches.Count < 2)
            {
                return 0f;
            }

            TouchControl first = Touchscreen.current.touches[0];
            TouchControl second = Touchscreen.current.touches[1];
            if (!first.press.isPressed || !second.press.isPressed)
            {
                return 0f;
            }

            Vector2 firstCurrent = first.position.ReadValue();
            Vector2 secondCurrent = second.position.ReadValue();
            Vector2 firstPrevious = first.position.ReadValue() - first.delta.ReadValue();
            Vector2 secondPrevious = second.position.ReadValue() - second.delta.ReadValue();
            return Vector2.Distance(firstCurrent, secondCurrent) - Vector2.Distance(firstPrevious, secondPrevious);
        }
    }
}

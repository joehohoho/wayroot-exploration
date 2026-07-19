using UnityEngine;
using UnityEngine.InputSystem;

namespace Wayroot.Input
{
    /// <summary>Owns device bindings and exposes semantic prototype commands.</summary>
    public sealed class PrototypeInputReader : MonoBehaviour
    {
        private InputActionMap _map = null!;
        private InputAction _move = null!;
        private InputAction _zoom = null!;
        private Vector2 _virtualMove;
        private float _virtualZoom;

        public Vector2 Move => _virtualMove.sqrMagnitude > 0.0001f ? _virtualMove : _move.ReadValue<Vector2>();
        public float ZoomDelta => _zoom.ReadValue<float>() + _virtualZoom;

        private void Awake()
        {
            _map = new InputActionMap("Prototype");
            _move = _map.AddAction("Move", InputActionType.Value);
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow").With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow").With("Right", "<Keyboard>/rightArrow");
            _move.AddBinding("<Gamepad>/leftStick");
            _zoom = _map.AddAction("Zoom", InputActionType.Value);
            _zoom.AddBinding("<Mouse>/scroll/y");
        }

        private void OnEnable() => _map.Enable();
        private void OnDisable() => _map.Disable();
        private void OnDestroy() => _map.Dispose();

        public void SetVirtualMove(Vector2 value) => _virtualMove = value;
        public void SetVirtualZoom(float value) => _virtualZoom = value;
        public void ClearVirtualZoom() => _virtualZoom = 0f;
    }
}

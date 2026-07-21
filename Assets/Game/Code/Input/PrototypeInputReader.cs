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
        private InputAction _interact = null!;
        private InputAction _attack = null!;
        private InputAction _dodge = null!;
        private Vector2 _virtualMove;
        private float _virtualZoom;
        private bool _virtualInteract;
        private bool _virtualAttack;
        private bool _virtualDodge;
        public Vector2 Move => _virtualMove.sqrMagnitude > 0.0001f ? _virtualMove : _move.ReadValue<Vector2>();
        public float ZoomDelta => _zoom.ReadValue<float>() + _virtualZoom;
        public bool InteractHeld => _virtualInteract || _interact.IsPressed();
        public bool AttackHeld => _virtualAttack || _attack.IsPressed();
        private void Awake()
        {
            _map = new InputActionMap("Prototype");
            _move = _map.AddAction("Move", InputActionType.Value);
            _move.AddCompositeBinding("2DVector").With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s").With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
            _move.AddCompositeBinding("2DVector").With("Up", "<Keyboard>/upArrow").With("Down", "<Keyboard>/downArrow").With("Left", "<Keyboard>/leftArrow").With("Right", "<Keyboard>/rightArrow");
            _move.AddBinding("<Gamepad>/leftStick");
            _zoom = _map.AddAction("Zoom", InputActionType.Value); _zoom.AddBinding("<Mouse>/scroll/y");
            _interact = _map.AddAction("Interact", InputActionType.Button); _interact.AddBinding("<Keyboard>/e"); _interact.AddBinding("<Gamepad>/buttonSouth");
            _attack = _map.AddAction("Attack", InputActionType.Button); _attack.AddBinding("<Keyboard>/space"); _attack.AddBinding("<Gamepad>/rightTrigger");
            _dodge = _map.AddAction("Dodge", InputActionType.Button); _dodge.AddBinding("<Keyboard>/leftShift"); _dodge.AddBinding("<Gamepad>/buttonEast");
        }
        private void OnEnable() => _map.Enable(); private void OnDisable() => _map.Disable(); private void OnDestroy() => _map.Dispose();
        public void SetVirtualMove(Vector2 value) => _virtualMove = value;
        public void SetVirtualZoom(float value) => _virtualZoom = value;
        public void SetVirtualInteract(bool value) => _virtualInteract = value;
        public void SetVirtualAttack(bool value) => _virtualAttack = value;
        public void RequestVirtualDodge() => _virtualDodge = true;
        public bool HasDodgeBinding(string path)
        {
            foreach (InputBinding binding in _dodge.bindings)
            {
                if (binding.path == path) return true;
            }

            return false;
        }
        public bool ConsumeDodgeRequested()
        {
            bool requested = _virtualDodge || _dodge.WasPressedThisFrame();
            _virtualDodge = false;
            return requested;
        }
        public void ClearVirtualZoom() => _virtualZoom = 0f;
    }
}

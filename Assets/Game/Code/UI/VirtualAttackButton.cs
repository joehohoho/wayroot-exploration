using Wayroot.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wayroot.UI
{
    public sealed class VirtualAttackButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private PrototypeInputReader _input = null!;
        public void Configure(PrototypeInputReader input) => _input = input;
        public void OnPointerDown(PointerEventData eventData) => _input.SetVirtualAttack(true);
        public void OnPointerUp(PointerEventData eventData) => _input.SetVirtualAttack(false);
        private void OnDisable() { if (_input != null) _input.SetVirtualAttack(false); }
    }
}

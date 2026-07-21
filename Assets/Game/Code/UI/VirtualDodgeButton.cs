using Wayroot.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wayroot.UI
{
    /// <summary>Mobile presentation bridge for the same one-shot semantic dodge action as keyboard and gamepad.</summary>
    public sealed class VirtualDodgeButton : MonoBehaviour, IPointerDownHandler
    {
        private PrototypeInputReader _input = null!;

        public void Configure(PrototypeInputReader input) => _input = input;
        public void OnPointerDown(PointerEventData eventData) => _input.RequestVirtualDodge();
    }
}

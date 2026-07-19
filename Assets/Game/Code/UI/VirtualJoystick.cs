using Wayroot.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wayroot.UI
{
    public sealed class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform handle = null!;
        [SerializeField] private PrototypeInputReader inputReader = null!;
        private RectTransform _area = null!;

        private void Awake() => _area = (RectTransform)transform;

        public void Configure(PrototypeInputReader reader, RectTransform joystickHandle)
        {
            inputReader = reader;
            handle = joystickHandle;
        }

        public void OnPointerDown(PointerEventData eventData) => ApplyPointer(eventData);
        public void OnDrag(PointerEventData eventData) => ApplyPointer(eventData);

        public void OnPointerUp(PointerEventData eventData)
        {
            handle.anchoredPosition = Vector2.zero;
            inputReader.SetVirtualMove(Vector2.zero);
        }

        private void ApplyPointer(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, eventData.position, eventData.pressEventCamera, out Vector2 local);
            Vector2 radius = _area.rect.size * 0.5f;
            Vector2 normalized = new(local.x / radius.x, local.y / radius.y);
            normalized = Vector2.ClampMagnitude(normalized, 1f);
            handle.anchoredPosition = Vector2.Scale(normalized, radius * 0.55f);
            inputReader.SetVirtualMove(normalized);
        }
    }
}

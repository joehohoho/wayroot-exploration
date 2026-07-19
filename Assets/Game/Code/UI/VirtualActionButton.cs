using Wayroot.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Wayroot.UI
{
    /// <summary>Presentation-only hold button for the semantic interaction command.</summary>
    public sealed class VirtualActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private PrototypeInputReader inputReader = null!;

        public void Configure(PrototypeInputReader reader) => inputReader = reader;
        public void OnPointerDown(PointerEventData eventData) => inputReader.SetVirtualInteract(true);
        public void OnPointerUp(PointerEventData eventData) => inputReader.SetVirtualInteract(false);
        private void OnDisable()
        {
            if (inputReader != null)
            {
                inputReader.SetVirtualInteract(false);
            }
        }
    }
}

using Wayroot.Gathering;
using Wayroot.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    public sealed class GatheringHud : MonoBehaviour
    {
        [SerializeField] private Text text = null!;
        [SerializeField] private PrototypeGatheringController gathering = null!;
        public void Configure(Text label, PrototypeGatheringController controller) { text = label; gathering = controller; }
        private void Update() => text.text = $"PETAL {gathering.GetCount(ResourceType.WildPetal)}  TIMBER {gathering.GetCount(ResourceType.Timber)}  STONE {gathering.GetCount(ResourceType.Stone)}";
    }
}

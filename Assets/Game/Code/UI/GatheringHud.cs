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
        private void Update()
        {
            GatheringNode? target = gathering.CurrentTarget;
            string prompt = target == null ? "Move close to a resource" : $"HOLD E / GATHER: {target.name}  {target.Steps}/{target.RequiredSteps}";
            text.text = $"{prompt}\nPETAL {gathering.GetCount(ResourceType.WildPetal)}  TIMBER {gathering.GetCount(ResourceType.Timber)}  STONE {gathering.GetCount(ResourceType.Stone)}";
        }
    }
}

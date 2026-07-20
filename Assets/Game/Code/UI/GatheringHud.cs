using Wayroot.Gathering;
using Wayroot.Inventory;
using Wayroot.Progression;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    public sealed class GatheringHud : MonoBehaviour
    {
        private Text _text = null!;
        private PrototypeGatheringController _gathering = null!;
        private PrototypeMerchantController _merchant = null!;

        public void Configure(Text text, PrototypeGatheringController gathering, PrototypeMerchantController merchant)
        {
            _text = text;
            _gathering = gathering;
            _merchant = merchant;
        }

        private void Update()
        {
            GatheringNode? target = _gathering.CurrentTarget;
            string prompt = _merchant.IsInRange
                ? $"HOLD E / GATHER: {_merchant.Status}"
                : target == null ? "Move close to a resource or merchant" : $"HOLD E / GATHER: {target.name}  {target.Steps}/{target.RequiredSteps}";
            _text.text = $"{prompt}\nPETAL {_gathering.GetCount(ResourceType.WildPetal)}  TIMBER {_gathering.GetCount(ResourceType.Timber)}  STONE {_gathering.GetCount(ResourceType.Stone)}  CORE {_gathering.GetCount(ResourceType.SlimeCore)}  WEAPON {_gathering.WeaponLevel}/1  ATK {_gathering.AttackDamage}";
        }
    }
}

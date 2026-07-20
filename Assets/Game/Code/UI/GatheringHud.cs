using Wayroot.Gathering;
using Wayroot.Inventory;
using Wayroot.Progression;
using Wayroot.Building;
using Wayroot.Creatures;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    public sealed class GatheringHud : MonoBehaviour
    {
        private Text _text = null!;
        private PrototypeGatheringController _gathering = null!;
        private PrototypeMerchantController _merchant = null!;
        private PrototypeBuildController _build = null!;
        private PrototypeCreatureController _creature = null!;

        public void Configure(Text text, PrototypeGatheringController gathering, PrototypeMerchantController merchant, PrototypeBuildController build, PrototypeCreatureController creature)
        {
            _text = text;
            _gathering = gathering;
            _merchant = merchant;
            _build = build;
            _creature = creature;
        }

        private void Update()
        {
            GatheringNode? target = _gathering.CurrentTarget;
            string prompt = _creature.IsInRange
                ? $"HOLD E / GATHER: {_creature.Status}"
                : _build.IsInRange
                ? $"HOLD E / GATHER: {_build.Status}"
                : _merchant.IsInRange
                ? $"HOLD E / GATHER: {_merchant.Status}"
                : target == null ? "Move close to a resource or merchant" : $"HOLD E / GATHER: {target.name}  {target.Steps}/{target.RequiredSteps}";
            string shelter = _gathering.ShelterBuilt ? "SHELTER BUILT" : "SHELTER 3 TIMBER + 3 STONE";
            string creature = _gathering.CreatureBefriended ? "MOSSling befriended" : "MOSSling nearby";
            _text.text = $"{prompt}\nPETAL {_gathering.GetCount(ResourceType.WildPetal)}  TIMBER {_gathering.GetCount(ResourceType.Timber)}  STONE {_gathering.GetCount(ResourceType.Stone)}  CORE {_gathering.GetCount(ResourceType.SlimeCore)}  WEAPON {_gathering.WeaponLevel}/1  ATK {_gathering.AttackDamage}\n{shelter}  |  {creature}";
        }
    }
}

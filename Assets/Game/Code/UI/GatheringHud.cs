using Wayroot.Building;
using Wayroot.Exploration;
using Wayroot.Gathering;
using Wayroot.Inventory;
using Wayroot.Progression;
using Wayroot.Wayroot;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Restrained top-right resource and progression card; interaction instructions live elsewhere.</summary>
    public sealed class GatheringHud : MonoBehaviour
    {
        private Text _text = null!;
        private PrototypeGatheringController _gathering = null!;
        private PrototypeMerchantController _merchant = null!;
        private PrototypeBuildController _build = null!;
        private PrototypeWayrootController _wayroot = null!;
        private BloomwellController _bloomwell = null!;

        public void Configure(Text text, PrototypeGatheringController gathering, PrototypeMerchantController merchant, PrototypeBuildController build, PrototypeWayrootController wayroot, global::Wayroot.Creatures.PrototypeCreatureController creature, BloomwellController bloomwell)
        {
            _text = text;
            _gathering = gathering;
            _merchant = merchant;
            _build = build;
            _wayroot = wayroot;
            _bloomwell = bloomwell;
        }

        private void Update()
        {
            string milestone = _gathering.BloomwellRestored ? "BLOOMWELL RESTORED"
                : _gathering.WayrootRestored ? "WAYROOT RESTORED"
                : _gathering.WeaponLevel == 0 ? "IRON EDGE AWAITS"
                : !_gathering.ShelterBuilt ? "SHELTER AWAITS"
                : "WAYROOT AWAITS";
            _text.text = $"PACK\nPETAL  {_gathering.GetCount(ResourceType.WildPetal)}    TIMBER  {_gathering.GetCount(ResourceType.Timber)}\nSTONE  {_gathering.GetCount(ResourceType.Stone)}    CORE  {_gathering.GetCount(ResourceType.SlimeCore)}\n{milestone}";
        }
    }
}

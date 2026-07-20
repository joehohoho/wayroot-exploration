using Wayroot.Inventory;

namespace Wayroot.Wayroot
{
    /// <summary>Pure, fixed-cost rules for the one finite Sunmeadow Wayroot objective.</summary>
    public static class WayrootRestorationRules
    {
        public const int PetalCost = 3;
        public const int TimberCost = 3;
        public const int StoneCost = 3;
        public const int CoreCost = 1;

        public static bool CanRestore(bool wayrootRestored, bool weaponUpgradeAcquired, bool shelterBuilt, InventoryState inventory)
        {
            return !wayrootRestored
                && weaponUpgradeAcquired
                && shelterBuilt
                && inventory.GetCount(ResourceType.WildPetal) >= PetalCost
                && inventory.GetCount(ResourceType.Timber) >= TimberCost
                && inventory.GetCount(ResourceType.Stone) >= StoneCost
                && inventory.GetCount(ResourceType.SlimeCore) >= CoreCost;
        }

        public static bool TryRestore(bool wayrootRestored, bool weaponUpgradeAcquired, bool shelterBuilt, InventoryState inventory)
        {
            if (!CanRestore(wayrootRestored, weaponUpgradeAcquired, shelterBuilt, inventory))
            {
                return false;
            }

            inventory.TrySpend(ResourceType.WildPetal, PetalCost);
            inventory.TrySpend(ResourceType.Timber, TimberCost);
            inventory.TrySpend(ResourceType.Stone, StoneCost);
            inventory.TrySpend(ResourceType.SlimeCore, CoreCost);
            return true;
        }
    }
}

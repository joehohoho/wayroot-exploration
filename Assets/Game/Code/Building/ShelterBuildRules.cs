using Wayroot.Inventory;

namespace Wayroot.Building
{
    /// <summary>Pure rules for the one fixed-cost Phase 5 shelter blueprint.</summary>
    public static class ShelterBuildRules
    {
        public const int TimberCost = 3;
        public const int StoneCost = 3;

        public static bool CanBuild(bool shelterBuilt, InventoryState inventory)
        {
            return !shelterBuilt
                && inventory.GetCount(ResourceType.Timber) >= TimberCost
                && inventory.GetCount(ResourceType.Stone) >= StoneCost;
        }
    }
}

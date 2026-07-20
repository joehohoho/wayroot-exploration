using Wayroot.Inventory;

namespace Wayroot.Combat
{
    /// <summary>Pure rules for the single, intentionally bounded Phase 4 weapon upgrade.</summary>
    public static class WeaponUpgradeRules
    {
        public const int MaximumLevel = 1;
        public const int PetalCost = 1;
        public const int SlimeCoreCost = 1;
        public const int BaseAttackDamage = 1;
        public const int DamagePerLevel = 1;

        public static int GetAttackDamage(int weaponLevel)
        {
            return BaseAttackDamage + (weaponLevel > 0 ? DamagePerLevel : 0);
        }

        public static bool CanPurchase(int weaponLevel, InventoryState inventory)
        {
            return weaponLevel < MaximumLevel
                && inventory.GetCount(ResourceType.WildPetal) >= PetalCost
                && inventory.GetCount(ResourceType.SlimeCore) >= SlimeCoreCost;
        }
    }
}

using System.Collections.Generic;
using System.Text;
using Wayroot.Inventory;

namespace Wayroot.Exploration
{
    /// <summary>Defines the compact, existing-resource-only Bloomwell finale cost and state gate.</summary>
    public static class BloomwellRestorationRules
    {
        public const int PetalCost = 2;
        public const int TimberCost = 2;
        public const int StoneCost = 2;
        public const int CoreCost = 1;
        public const string CompleteStatus = "BLOOMWELL RESTORED — SUNMEADOW BLOOMS ANEW";

        public static bool CanRestore(bool moonlitGladeOpen, bool bloomwellRestored, InventoryState inventory)
        {
            return moonlitGladeOpen
                && !bloomwellRestored
                && inventory.GetCount(ResourceType.WildPetal) >= PetalCost
                && inventory.GetCount(ResourceType.Timber) >= TimberCost
                && inventory.GetCount(ResourceType.Stone) >= StoneCost
                && inventory.GetCount(ResourceType.SlimeCore) >= CoreCost;
        }

        public static bool TryRestore(bool moonlitGladeOpen, bool bloomwellRestored, InventoryState inventory)
        {
            if (!CanRestore(moonlitGladeOpen, bloomwellRestored, inventory)) return false;

            inventory.TrySpend(ResourceType.WildPetal, PetalCost);
            inventory.TrySpend(ResourceType.Timber, TimberCost);
            inventory.TrySpend(ResourceType.Stone, StoneCost);
            inventory.TrySpend(ResourceType.SlimeCore, CoreCost);
            return true;
        }

        public static string GetRequirementStatus(bool moonlitGladeOpen, bool bloomwellRestored, InventoryState inventory)
        {
            if (!moonlitGladeOpen) return "BLOOMWELL SEALED — DEFEAT THORN GUARDIAN AND ENTER MOONLIT GLADE.";
            if (bloomwellRestored) return CompleteStatus;

            List<string> missing = new();
            AddMissing(missing, ResourceType.WildPetal, PetalCost, "PETAL", inventory);
            AddMissing(missing, ResourceType.Timber, TimberCost, "TIMBER", inventory);
            AddMissing(missing, ResourceType.Stone, StoneCost, "STONE", inventory);
            AddMissing(missing, ResourceType.SlimeCore, CoreCost, "CORE", inventory);
            return $"BLOOMWELL needs {string.Join(" + ", missing)}.";
        }

        private static void AddMissing(List<string> missing, ResourceType resource, int required, string name, InventoryState inventory)
        {
            int amount = required - inventory.GetCount(resource);
            if (amount > 0) missing.Add($"{amount} {name}");
        }
    }
}

using NUnit.Framework;
using Wayroot.Inventory;
using Wayroot.Wayroot;

namespace Wayroot.Tests.EditMode
{
    public sealed class WayrootRestorationRulesTests
    {
        [Test]
        public void CanRestore_RequiresAllResourcesAndBothProgressionPrerequisites()
        {
            InventoryState inventory = CreateFullCostInventory();

            Assert.That(WayrootRestorationRules.CanRestore(false, false, true, inventory), Is.False);
            Assert.That(WayrootRestorationRules.CanRestore(false, true, false, inventory), Is.False);
            Assert.That(WayrootRestorationRules.CanRestore(false, true, true, inventory), Is.True);

            inventory.TrySpend(ResourceType.SlimeCore, 1);
            Assert.That(WayrootRestorationRules.CanRestore(false, true, true, inventory), Is.False);
        }

        [Test]
        public void TryRestore_SpendsTheFixedCostExactlyOnceAndCannotSpendAgain()
        {
            InventoryState inventory = CreateFullCostInventory();

            Assert.That(WayrootRestorationRules.TryRestore(false, true, true, inventory), Is.True);
            Assert.That(inventory.GetCount(ResourceType.WildPetal), Is.EqualTo(0));
            Assert.That(inventory.GetCount(ResourceType.Timber), Is.EqualTo(0));
            Assert.That(inventory.GetCount(ResourceType.Stone), Is.EqualTo(0));
            Assert.That(inventory.GetCount(ResourceType.SlimeCore), Is.EqualTo(0));
            Assert.That(WayrootRestorationRules.TryRestore(true, true, true, inventory), Is.False);
        }

        [Test]
        public void TryRestore_LeavesInventoryUntouchedWhenARequirementIsMissing()
        {
            InventoryState inventory = CreateFullCostInventory();

            Assert.That(WayrootRestorationRules.TryRestore(false, true, false, inventory), Is.False);
            Assert.That(inventory.GetCount(ResourceType.WildPetal), Is.EqualTo(WayrootRestorationRules.PetalCost));
            Assert.That(inventory.GetCount(ResourceType.Timber), Is.EqualTo(WayrootRestorationRules.TimberCost));
            Assert.That(inventory.GetCount(ResourceType.Stone), Is.EqualTo(WayrootRestorationRules.StoneCost));
            Assert.That(inventory.GetCount(ResourceType.SlimeCore), Is.EqualTo(WayrootRestorationRules.CoreCost));
        }

        private static InventoryState CreateFullCostInventory()
        {
            InventoryState inventory = new();
            inventory.TryAdd(ResourceType.WildPetal, WayrootRestorationRules.PetalCost, out _, out _);
            inventory.TryAdd(ResourceType.Timber, WayrootRestorationRules.TimberCost, out _, out _);
            inventory.TryAdd(ResourceType.Stone, WayrootRestorationRules.StoneCost, out _, out _);
            inventory.TryAdd(ResourceType.SlimeCore, WayrootRestorationRules.CoreCost, out _, out _);
            return inventory;
        }
    }
}

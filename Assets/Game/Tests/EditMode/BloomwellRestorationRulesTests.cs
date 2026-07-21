using NUnit.Framework;
using Wayroot.Exploration;
using Wayroot.Inventory;

namespace Wayroot.Tests.EditMode
{
    public sealed class BloomwellRestorationRulesTests
    {
        [Test]
        public void CanRestore_RequiresMoonlitGladeAndTheExactExistingResourceBundle()
        {
            InventoryState inventory = CreateFullCostInventory();

            Assert.That(BloomwellRestorationRules.CanRestore(false, false, inventory), Is.False);
            Assert.That(BloomwellRestorationRules.CanRestore(true, false, inventory), Is.True);
            inventory.TrySpend(ResourceType.Stone, 1);
            Assert.That(BloomwellRestorationRules.CanRestore(true, false, inventory), Is.False);
        }

        [Test]
        public void TryRestore_SpendsExactlyOnceAndDoesNotSpendWhenAlreadyComplete()
        {
            InventoryState inventory = CreateFullCostInventory();

            Assert.That(BloomwellRestorationRules.TryRestore(true, false, inventory), Is.True);
            Assert.That(inventory.GetCount(ResourceType.WildPetal), Is.EqualTo(0));
            Assert.That(inventory.GetCount(ResourceType.Timber), Is.EqualTo(0));
            Assert.That(inventory.GetCount(ResourceType.Stone), Is.EqualTo(0));
            Assert.That(inventory.GetCount(ResourceType.SlimeCore), Is.EqualTo(0));
            Assert.That(BloomwellRestorationRules.TryRestore(true, true, inventory), Is.False);
        }

        [Test]
        public void RequirementStatus_NamesEveryMissingExistingItemAndTheCompletionState()
        {
            InventoryState inventory = new();

            string status = BloomwellRestorationRules.GetRequirementStatus(true, false, inventory);
            Assert.That(status, Does.Contain("2 PETAL"));
            Assert.That(status, Does.Contain("2 TIMBER"));
            Assert.That(status, Does.Contain("2 STONE"));
            Assert.That(status, Does.Contain("1 CORE"));
            Assert.That(BloomwellRestorationRules.GetRequirementStatus(true, true, inventory), Is.EqualTo(BloomwellRestorationRules.CompleteStatus));
        }

        private static InventoryState CreateFullCostInventory()
        {
            InventoryState inventory = new();
            inventory.TryAdd(ResourceType.WildPetal, BloomwellRestorationRules.PetalCost, out _, out _);
            inventory.TryAdd(ResourceType.Timber, BloomwellRestorationRules.TimberCost, out _, out _);
            inventory.TryAdd(ResourceType.Stone, BloomwellRestorationRules.StoneCost, out _, out _);
            inventory.TryAdd(ResourceType.SlimeCore, BloomwellRestorationRules.CoreCost, out _, out _);
            return inventory;
        }
    }
}

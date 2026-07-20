using NUnit.Framework;
using Wayroot.Inventory;

namespace Wayroot.Tests.EditMode
{
    public sealed class InventoryStateTests
    {
        [Test]
        public void TryAdd_CapsAStackAtNinetyNineAndReturnsRemainder()
        {
            InventoryState inventory = new();

            bool added = inventory.TryAdd(ResourceType.WildPetal, 120, out int accepted, out int remainder);

            Assert.That(added, Is.True);
            Assert.That(accepted, Is.EqualTo(99));
            Assert.That(remainder, Is.EqualTo(21));
            Assert.That(inventory.GetCount(ResourceType.WildPetal), Is.EqualTo(99));
        }

        [Test]
        public void TryAdd_RejectsZeroQuantity()
        {
            InventoryState inventory = new();

            bool added = inventory.TryAdd(ResourceType.Timber, 0, out int accepted, out int remainder);

            Assert.That(added, Is.False);
            Assert.That(accepted, Is.Zero);
            Assert.That(remainder, Is.Zero);
            Assert.That(inventory.GetCount(ResourceType.Timber), Is.Zero);
        }

        [Test]
        public void TrySpend_RequiresEnoughResourcesAndDeductsExactlyTheCost()
        {
            InventoryState inventory = new();
            inventory.TryAdd(ResourceType.SlimeCore, 1, out _, out _);

            Assert.That(inventory.TrySpend(ResourceType.SlimeCore, 2), Is.False);
            Assert.That(inventory.TrySpend(ResourceType.SlimeCore, 1), Is.True);
            Assert.That(inventory.GetCount(ResourceType.SlimeCore), Is.Zero);
        }
    }
}

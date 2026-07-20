using NUnit.Framework;
using Wayroot.Building;
using Wayroot.Inventory;

namespace Wayroot.Tests.EditMode
{
    public sealed class ShelterBuildRulesTests
    {
        [Test]
        public void CanBuild_RequiresThreeTimberAndThreeStoneBeforeTheShelterIsBuilt()
        {
            InventoryState inventory = new();
            inventory.TryAdd(ResourceType.Timber, 3, out _, out _);
            inventory.TryAdd(ResourceType.Stone, 3, out _, out _);

            Assert.That(ShelterBuildRules.CanBuild(false, inventory), Is.True);
            Assert.That(ShelterBuildRules.CanBuild(true, inventory), Is.False);
        }

        [Test]
        public void CanBuild_RejectsAnyMissingPartOfTheFixedCost()
        {
            InventoryState inventory = new();
            inventory.TryAdd(ResourceType.Timber, 3, out _, out _);
            inventory.TryAdd(ResourceType.Stone, 2, out _, out _);

            Assert.That(ShelterBuildRules.CanBuild(false, inventory), Is.False);
        }
    }
}

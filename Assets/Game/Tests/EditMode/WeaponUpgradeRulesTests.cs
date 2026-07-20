using NUnit.Framework;
using Wayroot.Combat;
using Wayroot.Inventory;

namespace Wayroot.Tests.EditMode
{
    public sealed class WeaponUpgradeRulesTests
    {
        [Test]
        public void GetAttackDamage_IncreasesOnlyForTheSinglePurchasedLevel()
        {
            Assert.That(WeaponUpgradeRules.GetAttackDamage(0), Is.EqualTo(1));
            Assert.That(WeaponUpgradeRules.GetAttackDamage(1), Is.EqualTo(2));
            Assert.That(WeaponUpgradeRules.GetAttackDamage(2), Is.EqualTo(2));
        }

        [Test]
        public void CanPurchase_RequiresOnePetalAndOneSlimeCoreBeforeTheLevelCap()
        {
            InventoryState inventory = new();
            inventory.TryAdd(ResourceType.WildPetal, 1, out _, out _);
            inventory.TryAdd(ResourceType.SlimeCore, 1, out _, out _);

            Assert.That(WeaponUpgradeRules.CanPurchase(0, inventory), Is.True);
            Assert.That(WeaponUpgradeRules.CanPurchase(1, inventory), Is.False);
        }
    }
}

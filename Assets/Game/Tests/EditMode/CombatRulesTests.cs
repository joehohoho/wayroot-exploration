using NUnit.Framework;
using Wayroot.Combat;

namespace Wayroot.Tests.EditMode
{
    public sealed class CombatRulesTests
    {
        [Test]
        public void ApplyDamage_ClampsHealthAndReportsDefeat()
        {
            int health = CombatRules.ApplyDamage(3, 5, out bool defeated);
            Assert.That(health, Is.Zero);
            Assert.That(defeated, Is.True);
        }

        [Test]
        public void CanAttack_RequiresRangeAndCooldown()
        {
            Assert.That(CombatRules.CanAttack(1.5f, 2f, 0.4f, 0.4f), Is.True);
            Assert.That(CombatRules.CanAttack(2.1f, 2f, 0f, 0.4f), Is.False);
            Assert.That(CombatRules.CanAttack(1f, 2f, 0.1f, 0.4f), Is.False);
        }
    }
}

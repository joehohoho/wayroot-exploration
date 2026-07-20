using NUnit.Framework;
using Wayroot.Combat;

namespace Wayroot.Tests.EditMode
{
    public sealed class ThornGuardianRulesTests
    {
        [Test]
        public void Profile_IsDistinctAndModestlyTougherThanPracticeSlime()
        {
            EnemyCombatProfile guardian = ThornGuardianRules.Profile;

            Assert.That(guardian.DisplayName, Is.EqualTo("THORN GUARDIAN"));
            Assert.That(guardian.MaxHealth, Is.GreaterThan(ThornGuardianRules.PracticeSlimeHealth));
            Assert.That(guardian.ContactDamage, Is.GreaterThan(ThornGuardianRules.PracticeSlimeContactDamage));
            Assert.That(guardian.MaxHealth, Is.LessThanOrEqualTo(8));
            Assert.That(guardian.RespawnDelaySeconds, Is.EqualTo(15f));
        }

        [Test]
        public void CanEnterRestoredGrove_RequiresTheFirstWayroot()
        {
            Assert.That(ThornGuardianRules.CanEnterRestoredGrove(false), Is.False);
            Assert.That(ThornGuardianRules.CanEnterRestoredGrove(true), Is.True);
        }
    }
}

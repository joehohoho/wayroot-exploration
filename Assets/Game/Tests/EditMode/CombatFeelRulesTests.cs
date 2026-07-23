using NUnit.Framework;
using Wayroot.Combat;

namespace Wayroot.Tests.EditMode
{
    public sealed class CombatFeelRulesTests
    {
        [Test]
        public void GetHapticProfile_CombatCuesAreBoundedAndDistinct()
        {
            HapticProfile attack = CombatFeelRules.GetHapticProfile(CombatHapticCue.AttackContact);
            HapticProfile damage = CombatFeelRules.GetHapticProfile(CombatHapticCue.PlayerDamaged);
            HapticProfile defeat = CombatFeelRules.GetHapticProfile(CombatHapticCue.EnemyDefeated);

            Assert.That(attack.DurationMilliseconds, Is.LessThanOrEqualTo(CombatFeelRules.MaximumHapticDurationMilliseconds));
            Assert.That(defeat.Intensity, Is.GreaterThan(attack.Intensity));
            Assert.That(damage.Intensity, Is.GreaterThan(attack.Intensity));
        }

        [Test]
        public void OptionalHapticFeedback_IsNoOpSafeWithoutPlatformIntegration()
        {
            OptionalHapticFeedback feedback = new();

            feedback.Pulse(CombatHapticCue.AttackContact);

            Assert.That(feedback.LastCue, Is.EqualTo(CombatHapticCue.AttackContact));
            Assert.That(feedback.UsesPlatformApi, Is.False);
        }
    }
}

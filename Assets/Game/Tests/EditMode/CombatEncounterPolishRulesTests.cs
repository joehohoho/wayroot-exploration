using NUnit.Framework;
using Wayroot.Combat;

namespace Wayroot.Tests.EditMode
{
    public sealed class CombatEncounterPolishRulesTests
    {
        [Test]
        public void PresentationWindows_AreBriefAndExcludeTheirEndBoundary()
        {
            Assert.That(CombatEncounterPolishRules.PlayerTrailSeconds, Is.LessThan(CombatEncounterPolishRules.PlayerContactMarkerSeconds));
            Assert.That(CombatEncounterPolishRules.EnemyHitFlashSeconds, Is.LessThan(CombatEncounterPolishRules.EnemyRespawnCueSeconds));
            Assert.That(CombatEncounterPolishRules.IsActive(0f, CombatEncounterPolishRules.PlayerTrailSeconds), Is.True);
            Assert.That(CombatEncounterPolishRules.IsActive(CombatEncounterPolishRules.PlayerTrailSeconds, CombatEncounterPolishRules.PlayerTrailSeconds), Is.False);
            Assert.That(CombatEncounterPolishRules.IsActive(-0.01f, CombatEncounterPolishRules.PlayerTrailSeconds), Is.False);
        }

        [Test]
        public void CombatValues_RemainTheExistingPracticeAndGuardianProfiles()
        {
            EnemyCombatProfile guardian = ThornGuardianRules.Profile;

            Assert.That(ThornGuardianRules.PracticeSlimeHealth, Is.EqualTo(5));
            Assert.That(ThornGuardianRules.PracticeSlimeContactDamage, Is.EqualTo(1));
            Assert.That(guardian.MaxHealth, Is.EqualTo(8));
            Assert.That(guardian.ContactDamage, Is.EqualTo(2));
            Assert.That(guardian.RespawnDelaySeconds, Is.EqualTo(15f));
        }
    }
}

using NUnit.Framework;
using Wayroot.Gathering;
using Wayroot.Guidance;

namespace Wayroot.Tests.EditMode
{
    public sealed class JourneyGuidanceRulesTests
    {
        [Test]
        public void Select_FreshSaveBeginsWithTheExistingResourcePath()
        {
            JourneyGuidanceState state = JourneyGuidanceRules.Select(new PrototypeGatheringSave());

            Assert.That(state.Target, Is.EqualTo(JourneyTarget.Resource));
            Assert.That(state.Status, Does.Contain("GATHER"));
            Assert.That(state.HasPointer, Is.True);
        }

        [Test]
        public void Select_ReadyIronEdgeSavePointsToTheExistingMerchant()
        {
            JourneyGuidanceState state = JourneyGuidanceRules.Select(new PrototypeGatheringSave { petals = 1, slimeCores = 1 });

            Assert.That(state.Target, Is.EqualTo(JourneyTarget.Merchant));
            Assert.That(state.Status, Does.Contain("MERCHANT"));
        }

        [Test]
        public void Select_ProgressesThroughShelterWayrootGuardianGladeAndBloomwell()
        {
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1 }).Target, Is.EqualTo(JourneyTarget.Shelter));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true }).Target, Is.EqualTo(JourneyTarget.Wayroot));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, wayrootRestored = true }).Target, Is.EqualTo(JourneyTarget.Guardian));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, wayrootRestored = true, moonlitGladeUnlocked = true }).Target, Is.EqualTo(JourneyTarget.Bloomwell));
        }

        [Test]
        public void Select_RestoredBloomwellBecomesNonDirectiveFreeExploration()
        {
            JourneyGuidanceState state = JourneyGuidanceRules.Select(new PrototypeGatheringSave { bloomwellRestored = true });

            Assert.That(state.Target, Is.EqualTo(JourneyTarget.None));
            Assert.That(state.HasPointer, Is.False);
            Assert.That(state.Status, Does.Contain("EXPLORE"));
        }
    }
}

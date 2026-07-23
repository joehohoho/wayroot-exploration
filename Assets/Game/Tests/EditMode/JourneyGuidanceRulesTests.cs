using NUnit.Framework;
using Wayroot.Gathering;
using Wayroot.Guidance;

namespace Wayroot.Tests.EditMode
{
    public sealed class JourneyGuidanceRulesTests
    {
        [Test]
        public void Select_FreshSaveBeginsWithTheExistingWildflower()
        {
            JourneyGuidanceState state = JourneyGuidanceRules.Select(new PrototypeGatheringSave());

            Assert.That(state.Target, Is.EqualTo(JourneyTarget.Wildflower));
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
        public void Select_FirstSessionPacesEachExistingPrerequisiteAsOneAction()
        {
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave()).Target, Is.EqualTo(JourneyTarget.Wildflower));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { petals = 1 }).Target, Is.EqualTo(JourneyTarget.PracticeSlime));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1 }).Target, Is.EqualTo(JourneyTarget.YoungTree));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, timber = 3 }).Target, Is.EqualTo(JourneyTarget.StoneOutcrop));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, timber = 3, stone = 3 }).Target, Is.EqualTo(JourneyTarget.Shelter));
        }

        [Test]
        public void Select_BloomwellPreparationUsesOnlyTheExistingResourcesAndCombatReward()
        {
            PrototypeGatheringSave prepared = new()
            {
                weaponLevel = 1,
                shelterBuilt = true,
                wayrootRestored = true,
                moonlitGladeUnlocked = true
            };

            Assert.That(JourneyGuidanceRules.Select(prepared).Target, Is.EqualTo(JourneyTarget.Wildflower));
            prepared.petals = 2;
            Assert.That(JourneyGuidanceRules.Select(prepared).Target, Is.EqualTo(JourneyTarget.YoungTree));
            prepared.timber = 2;
            Assert.That(JourneyGuidanceRules.Select(prepared).Target, Is.EqualTo(JourneyTarget.StoneOutcrop));
            prepared.stone = 2;
            Assert.That(JourneyGuidanceRules.Select(prepared).Target, Is.EqualTo(JourneyTarget.PracticeSlime));
            prepared.slimeCores = 1;
            Assert.That(JourneyGuidanceRules.Select(prepared).Target, Is.EqualTo(JourneyTarget.Bloomwell));
        }

        [Test]
        public void Select_ProgressesThroughExistingMilestonesOnceTheirResourcesAreReady()
        {
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, timber = 3, stone = 3 }).Target, Is.EqualTo(JourneyTarget.Shelter));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, petals = 3, timber = 3, stone = 3, slimeCores = 1 }).Target, Is.EqualTo(JourneyTarget.Wayroot));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, wayrootRestored = true }).Target, Is.EqualTo(JourneyTarget.Guardian));
            Assert.That(JourneyGuidanceRules.Select(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, wayrootRestored = true, moonlitGladeUnlocked = true, petals = 2, timber = 2, stone = 2, slimeCores = 1 }).Target, Is.EqualTo(JourneyTarget.Bloomwell));
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

using NUnit.Framework;
using UnityEngine;
using Wayroot.Art;

namespace Wayroot.Tests.EditMode
{
    public sealed class ExplorationCompositionRulesTests
    {
        [Test]
        public void Profiles_KeepTheFourExistingDestinationsChromaticallyDistinct()
        {
            RegionCompositionProfile sunmeadow = ExplorationCompositionRules.GetProfile(ExplorationRegion.Sunmeadow);
            RegionCompositionProfile grove = ExplorationCompositionRules.GetProfile(ExplorationRegion.RestoredGrove);
            RegionCompositionProfile glade = ExplorationCompositionRules.GetProfile(ExplorationRegion.MoonlitGlade);
            RegionCompositionProfile bloomwell = ExplorationCompositionRules.GetProfile(ExplorationRegion.Bloomwell);

            Assert.That(sunmeadow.PrimaryColor.r, Is.GreaterThan(sunmeadow.PrimaryColor.b));
            Assert.That(grove.PrimaryColor.g, Is.GreaterThan(grove.PrimaryColor.r));
            Assert.That(glade.PrimaryColor.b, Is.GreaterThan(glade.PrimaryColor.r));
            Assert.That(bloomwell.AccentColor.g, Is.GreaterThan(bloomwell.AccentColor.r));
            Assert.That(ExplorationCompositionRules.AllProfiles, Has.Length.EqualTo(4));
        }

        [Test]
        public void Dressings_StayOutsideTheReservedCentralFocalZone()
        {
            foreach (Vector3 localPosition in ExplorationCompositionRules.SunmeadowDressingPositions)
            {
                Assert.That(ExplorationCompositionRules.IsOutsideFocalZone(localPosition), Is.True, $"{localPosition} obscures the Sunmeadow focal zone.");
            }

            foreach (Vector3 localPosition in ExplorationCompositionRules.GroveDressingPositions)
            {
                Assert.That(ExplorationCompositionRules.IsOutsideFocalZone(localPosition), Is.True, $"{localPosition} obscures the Grove focal zone.");
            }

            foreach (Vector3 localPosition in ExplorationCompositionRules.GladeDressingPositions)
            {
                Assert.That(ExplorationCompositionRules.IsOutsideFocalZone(localPosition), Is.True, $"{localPosition} obscures the Glade focal zone.");
            }
        }
    }
}

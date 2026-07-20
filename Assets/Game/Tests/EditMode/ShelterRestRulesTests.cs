using NUnit.Framework;
using Wayroot.Building;

namespace Wayroot.Tests.EditMode
{
    public sealed class ShelterRestRulesTests
    {
        [Test]
        public void CanRest_RequiresABuiltShelter()
        {
            Assert.That(ShelterRestRules.CanRest(false), Is.False);
            Assert.That(ShelterRestRules.CanRest(true), Is.True);
        }

        [Test]
        public void GetRespawnDestination_UsesActiveShelterOtherwiseDefaultSpawn()
        {
            Assert.That(ShelterRestRules.GetRespawnDestination(false), Is.EqualTo(RespawnDestination.DefaultSpawn));
            Assert.That(ShelterRestRules.GetRespawnDestination(true), Is.EqualTo(RespawnDestination.ActiveShelter));
        }
    }
}

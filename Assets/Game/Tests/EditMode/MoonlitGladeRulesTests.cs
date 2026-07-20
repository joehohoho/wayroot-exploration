using NUnit.Framework;
using Wayroot.Exploration;

namespace Wayroot.Tests.EditMode
{
    public sealed class MoonlitGladeRulesTests
    {
        [Test]
        public void CanEnter_RequiresTheRecordedThornGuardianVictory()
        {
            Assert.That(MoonlitGladeRules.CanEnter(false), Is.False);
            Assert.That(MoonlitGladeRules.CanEnter(true), Is.True);
        }

        [Test]
        public void UnlockStatus_IsClearAndDoesNotAddANewEconomy()
        {
            Assert.That(MoonlitGladeRules.LockedStatus, Does.Contain("SEALED"));
            Assert.That(MoonlitGladeRules.UnlockedStatus, Does.Contain("MOONLIT GLADE"));
        }
    }
}

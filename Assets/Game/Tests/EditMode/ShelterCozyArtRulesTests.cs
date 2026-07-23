using NUnit.Framework;
using UnityEngine;
using Wayroot.Building;

namespace Wayroot.Tests.EditMode
{
    public sealed class ShelterCozyArtRulesTests
    {
        [Test]
        public void ShelterStates_KeepTheUnbuiltPlotAndBuiltHomeReadablyDistinct()
        {
            Assert.That(ShelterCozyArtRules.GetStateLabel(false, false), Is.EqualTo("SHELTER\nBUILD PLOT"));
            Assert.That(ShelterCozyArtRules.GetStateLabel(true, false), Is.EqualTo("SHELTER\nREST HERE"));
            Assert.That(ShelterCozyArtRules.GetStateLabel(true, true), Is.EqualTo("SHELTER\nACTIVE HOME"));
            Assert.That(ShelterCozyArtRules.GetPlotColor(false), Is.Not.EqualTo(ShelterCozyArtRules.GetPlotColor(true)));
        }

        [Test]
        public void RestPulse_IsBriefAndExcludesItsEndBoundary()
        {
            Assert.That(ShelterCozyArtRules.RestPulseSeconds, Is.GreaterThan(0f));
            Assert.That(ShelterCozyArtRules.IsRestPulseActive(0f), Is.True);
            Assert.That(ShelterCozyArtRules.IsRestPulseActive(ShelterCozyArtRules.RestPulseSeconds), Is.False);
            Assert.That(ShelterCozyArtRules.IsRestPulseActive(-0.01f), Is.False);
        }

        [Test]
        public void CozyPalette_IsWarmAndDoesNotEncodeGameplayValues()
        {
            Color lantern = ShelterCozyArtRules.LanternColor;
            Color hearth = ShelterCozyArtRules.HearthColor;

            Assert.That(lantern.r, Is.GreaterThan(lantern.b));
            Assert.That(hearth.r, Is.GreaterThan(hearth.b));
            Assert.That(ShelterBuildRules.TimberCost, Is.EqualTo(3));
            Assert.That(ShelterBuildRules.StoneCost, Is.EqualTo(3));
        }
    }
}

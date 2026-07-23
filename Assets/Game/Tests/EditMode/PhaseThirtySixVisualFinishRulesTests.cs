using NUnit.Framework;
using UnityEngine;
using Wayroot.Art;

namespace Wayroot.Tests.EditMode
{
    public sealed class PhaseThirtySixVisualFinishRulesTests
    {
        [TestCase(ExplorationRegion.Sunmeadow)]
        [TestCase(ExplorationRegion.RestoredGrove)]
        [TestCase(ExplorationRegion.MoonlitGlade)]
        [TestCase(ExplorationRegion.Bloomwell)]
        public void GetAccent_ReturnsVisibleOpaqueRoutePalette(ExplorationRegion region)
        {
            Color color = PhaseThirtySixVisualFinishRules.GetAccent(region);

            Assert.That(color.a, Is.EqualTo(1f));
            Assert.That(color.maxColorComponent, Is.GreaterThan(0.5f));
        }

        [Test]
        public void IsSafeDecoration_RejectsOversizedAndFlatInvalidGeometry()
        {
            Assert.That(PhaseThirtySixVisualFinishRules.IsSafeDecoration(new Vector3(1.6f, 0.24f, 1.2f)), Is.True);
            Assert.That(PhaseThirtySixVisualFinishRules.IsSafeDecoration(new Vector3(2.5f, 0.24f, 1.2f)), Is.False);
            Assert.That(PhaseThirtySixVisualFinishRules.IsSafeDecoration(new Vector3(1f, 3f, 1f)), Is.False);
            Assert.That(PhaseThirtySixVisualFinishRules.IsSafeDecoration(new Vector3(1f, 0f, 1f)), Is.False);
        }
    }
}

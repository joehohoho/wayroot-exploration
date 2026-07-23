using NUnit.Framework;
using UnityEngine;
using Wayroot.Exploration;

namespace Wayroot.Tests.EditMode
{
    public sealed class BloomwellFinalePresentationRulesTests
    {
        [Test]
        public void ClimaxWindow_IsShortBoundedAndExcludesItsEndBoundary()
        {
            Assert.That(BloomwellFinalePresentationRules.ClimaxDurationSeconds, Is.GreaterThan(0f));
            Assert.That(BloomwellFinalePresentationRules.ClimaxDurationSeconds, Is.LessThanOrEqualTo(2f));
            Assert.That(BloomwellFinalePresentationRules.IsClimaxActive(0f), Is.True);
            Assert.That(BloomwellFinalePresentationRules.IsClimaxActive(BloomwellFinalePresentationRules.ClimaxDurationSeconds), Is.False);
            Assert.That(BloomwellFinalePresentationRules.IsClimaxActive(-0.01f), Is.False);
        }

        [Test]
        public void OrbitPosition_RemainsCompactAroundTheLandmark()
        {
            Vector3 center = new(-8.15f, 0f, 6.15f);
            Vector3 mote = BloomwellFinalePresentationRules.GetOrbitPosition(center, 0, 0f);

            Assert.That(Vector3.Distance(new Vector3(center.x, mote.y, center.z), mote), Is.LessThanOrEqualTo(BloomwellFinalePresentationRules.OrbitRadius + 0.01f));
            Assert.That(mote.y, Is.GreaterThan(center.y));
        }

        [Test]
        public void CompletionJourney_IsPeacefulAndNonDirective()
        {
            Assert.That(BloomwellFinalePresentationRules.CompletionJourneyStatus, Does.Contain("RESTORED"));
            Assert.That(BloomwellFinalePresentationRules.CompletionJourneyStatus, Does.Contain("EXPLORE"));
        }
    }
}

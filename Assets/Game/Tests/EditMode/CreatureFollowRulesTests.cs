using NUnit.Framework;
using UnityEngine;
using Wayroot.Creatures;

namespace Wayroot.Tests.EditMode
{
    public sealed class CreatureFollowRulesTests
    {
        [Test]
        public void GetNextPosition_KeepsTheCompanionOutsideItsStopDistance()
        {
            Vector3 current = new(0f, 0.5f, 0f);
            Vector3 player = new(1f, 1f, 0f);

            Vector3 next = CreatureFollowRules.GetNextPosition(current, player, 1.5f, 4f, 1f);

            Assert.That(next, Is.EqualTo(current));
        }

        [Test]
        public void GetNextPosition_AdvancesOnlyByTheAvailableFollowStep()
        {
            Vector3 current = new(0f, 0.5f, 0f);
            Vector3 player = new(8f, 1f, 0f);

            Vector3 next = CreatureFollowRules.GetNextPosition(current, player, 1.5f, 4f, 0.25f);

            Assert.That(next.y, Is.EqualTo(current.y));
            Assert.That(Vector3.Distance(current, next), Is.EqualTo(1f).Within(0.001f));
            Assert.That(Vector3.Distance(next, player), Is.GreaterThanOrEqualTo(1.5f));
        }

        [Test]
        public void GetNextPosition_StopsAtTheDesiredSafeDistance()
        {
            Vector3 current = new(0f, 0.5f, 0f);
            Vector3 player = new(2f, 1f, 0f);

            Vector3 next = CreatureFollowRules.GetNextPosition(current, player, 1.5f, 8f, 1f);

            Assert.That(Vector3.Distance(next, new Vector3(player.x, current.y, player.z)), Is.EqualTo(1.5f).Within(0.001f));
        }
    }
}

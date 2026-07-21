using NUnit.Framework;
using UnityEngine;
using Wayroot.Character;

namespace Wayroot.Tests.EditMode
{
    public sealed class DodgeRulesTests
    {
        [Test]
        public void ResolveDirection_PrefersCurrentMovementOverFacing()
        {
            Vector3 direction = DodgeRules.ResolveDirection(new Vector2(0.3f, 0.4f), Vector3.left);

            Assert.That(direction.x, Is.EqualTo(0.6f).Within(0.001f));
            Assert.That(direction.z, Is.EqualTo(0.8f).Within(0.001f));
        }

        [Test]
        public void ResolveDirection_UsesFacingWhenStationary()
        {
            Vector3 direction = DodgeRules.ResolveDirection(Vector2.zero, new Vector3(-2f, 4f, 0f));

            Assert.That(direction, Is.EqualTo(Vector3.left));
        }

        [Test]
        public void ResolveDirection_UsesForwardWhenMovementAndFacingAreZero()
        {
            Assert.That(DodgeRules.ResolveDirection(Vector2.zero, Vector3.zero), Is.EqualTo(Vector3.forward));
        }

        [Test]
        public void CanStart_RequiresRequestAndElapsedCooldown()
        {
            Assert.That(DodgeRules.CanStart(true, 2f, 2f), Is.True);
            Assert.That(DodgeRules.CanStart(true, 1.99f, 2f), Is.False);
            Assert.That(DodgeRules.CanStart(false, 4f, 2f), Is.False);
        }

        [Test]
        public void IsInvulnerable_IsLimitedToDocumentedWindow()
        {
            Assert.That(DodgeRules.IsInvulnerable(1.2f, 1f, 0.24f), Is.True);
            Assert.That(DodgeRules.IsInvulnerable(1.24f, 1f, 0.24f), Is.False);
            Assert.That(DodgeRules.IsInvulnerable(0.99f, 1f, 0.24f), Is.False);
        }
    }
}

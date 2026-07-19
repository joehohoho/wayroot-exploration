using NUnit.Framework;
using UnityEngine;
using Wayroot.Character;

namespace Wayroot.Tests.EditMode.Character
{
    public sealed class MovementRulesTests
    {
        [Test]
        public void ClampMoveMagnitude_ClampsDiagonalInputToOne()
        {
            Vector2 result = MovementRules.ClampMoveMagnitude(new Vector2(3f, 4f));

            Assert.That(result.magnitude, Is.EqualTo(1f).Within(0.0001f));
        }

        [Test]
        public void TryGetFacing_ReturnsFalseForZeroInput()
        {
            bool didUpdate = MovementRules.TryGetFacing(Vector2.zero, Vector3.forward, out Vector3 facing);

            Assert.That(didUpdate, Is.False);
            Assert.That(facing, Is.EqualTo(Vector3.forward));
        }

        [Test]
        public void TryGetFacing_MapsMoveToHorizontalWorldDirection()
        {
            bool didUpdate = MovementRules.TryGetFacing(new Vector2(-1f, 0f), Vector3.forward, out Vector3 facing);

            Assert.That(didUpdate, Is.True);
            Assert.That(facing, Is.EqualTo(Vector3.left));
        }
    }
}

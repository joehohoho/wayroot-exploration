using NUnit.Framework;
using Wayroot.Camera;

namespace Wayroot.Tests.EditMode.Camera
{
    public sealed class CameraZoomRulesTests
    {
        [Test]
        public void Clamp_ConstrictsValuesBelowMinimum()
        {
            float zoom = CameraZoomRules.Clamp(2f, 5f, 12f);

            Assert.That(zoom, Is.EqualTo(5f));
        }

        [Test]
        public void Clamp_ConstrictsValuesAboveMaximum()
        {
            float zoom = CameraZoomRules.Clamp(25f, 5f, 12f);

            Assert.That(zoom, Is.EqualTo(12f));
        }

        [Test]
        public void Clamp_PreservesValueInsideRange()
        {
            float zoom = CameraZoomRules.Clamp(8f, 5f, 12f);

            Assert.That(zoom, Is.EqualTo(8f));
        }
    }
}

using NUnit.Framework;
using Wayroot.UI;

namespace Wayroot.Tests.EditMode
{
    public sealed class AccessibilityRulesTests
    {
        [Test]
        public void Defaults_LeaveBothVisualComfortPreferencesOff()
        {
            Assert.That(AccessibilityRules.DefaultReducedFlash, Is.False);
            Assert.That(AccessibilityRules.DefaultReducedMotion, Is.False);
        }

        [Test]
        public void ReducedPreferences_UseBoundedMultipliersWithoutRemovingFeedback()
        {
            Assert.That(AccessibilityRules.GetFlashDuration(0.20f, true), Is.GreaterThan(0f).And.LessThan(0.20f));
            Assert.That(AccessibilityRules.GetMotionAmplitude(1f, true), Is.GreaterThan(0f).And.LessThan(1f));
            Assert.That(AccessibilityRules.GetFlashDuration(0.20f, false), Is.EqualTo(0.20f));
            Assert.That(AccessibilityRules.GetMotionAmplitude(1f, false), Is.EqualTo(1f));
        }
    }
}

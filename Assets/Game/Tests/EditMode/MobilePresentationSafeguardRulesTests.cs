using NUnit.Framework;
using Wayroot.Art;

namespace Wayroot.Tests.EditMode
{
    public sealed class MobilePresentationSafeguardRulesTests
    {
        [Test]
        public void SustainedSlowFrames_ReduceOnlyOptionalPresentation()
        {
            MobilePresentationMonitor monitor = new();
            for (int index = 0; index < MobilePresentationSafeguardRules.SlowFrameSamplesToReduce; index++)
            {
                monitor.ReportFrameDuration(MobilePresentationSafeguardRules.SlowFrameThresholdSeconds + 0.01f);
            }

            Assert.That(monitor.State, Is.EqualTo(MobilePresentationSafeguardState.Reduced));
            Assert.That(MobilePresentationSafeguardRules.KeepsRequiredGameplayVisible(monitor.State), Is.True);
            Assert.That(MobilePresentationSafeguardRules.KeepsTouchUiVisible(monitor.State), Is.True);
        }

        [Test]
        public void RecoveredFrames_UseHysteresisBeforeRestoringFullPresentation()
        {
            MobilePresentationMonitor monitor = new(MobilePresentationSafeguardState.Reduced);
            for (int index = 1; index < MobilePresentationSafeguardRules.FastFrameSamplesToRestore; index++)
            {
                monitor.ReportFrameDuration(MobilePresentationSafeguardRules.FastFrameThresholdSeconds - 0.005f);
                Assert.That(monitor.State, Is.EqualTo(MobilePresentationSafeguardState.Reduced));
            }

            monitor.ReportFrameDuration(MobilePresentationSafeguardRules.FastFrameThresholdSeconds - 0.005f);
            Assert.That(monitor.State, Is.EqualTo(MobilePresentationSafeguardState.Full));
        }
    }
}

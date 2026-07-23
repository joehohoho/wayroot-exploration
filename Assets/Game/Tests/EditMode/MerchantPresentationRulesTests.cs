using NUnit.Framework;
using Wayroot.Progression;

namespace Wayroot.Tests.EditMode
{
    public sealed class MerchantPresentationRulesTests
    {
        [Test]
        public void GetOutcome_UsesPurchaseResultAndExistingStatusWithoutChangingPurchaseRules()
        {
            Assert.That(MerchantPresentationRules.GetOutcome(true, "IRON EDGE purchased: ATK 1 -> 2."), Is.EqualTo(MerchantPurchaseOutcome.Purchased));
            Assert.That(MerchantPresentationRules.GetOutcome(false, "Need 1 PETAL + 1 CORE."), Is.EqualTo(MerchantPurchaseOutcome.InsufficientResources));
            Assert.That(MerchantPresentationRules.GetOutcome(false, "IRON EDGE already purchased: ATK 2."), Is.EqualTo(MerchantPurchaseOutcome.AlreadyOwned));
        }

        [Test]
        public void IsFeedbackActive_IsBoundedForEveryVisualOnlyOutcome()
        {
            Assert.That(MerchantPresentationRules.IsFeedbackActive(0f), Is.True);
            Assert.That(MerchantPresentationRules.IsFeedbackActive(MerchantPresentationRules.FeedbackDurationSeconds - 0.01f), Is.True);
            Assert.That(MerchantPresentationRules.IsFeedbackActive(MerchantPresentationRules.FeedbackDurationSeconds), Is.False);
        }
    }
}

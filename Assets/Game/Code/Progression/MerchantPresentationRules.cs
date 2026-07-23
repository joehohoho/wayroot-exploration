namespace Wayroot.Progression
{
    public enum MerchantPurchaseOutcome
    {
        Purchased,
        InsufficientResources,
        AlreadyOwned
    }

    /// <summary>Maps the existing merchant result to presentation-only feedback; it owns no prices, inventory, or save state.</summary>
    public static class MerchantPresentationRules
    {
        public const float FeedbackDurationSeconds = 0.72f;

        public static MerchantPurchaseOutcome GetOutcome(bool purchased, string status)
        {
            if (purchased) return MerchantPurchaseOutcome.Purchased;
            return status.Contains("already purchased")
                ? MerchantPurchaseOutcome.AlreadyOwned
                : MerchantPurchaseOutcome.InsufficientResources;
        }

        public static bool IsFeedbackActive(float elapsedSeconds) => elapsedSeconds >= 0f && elapsedSeconds < FeedbackDurationSeconds;
    }
}

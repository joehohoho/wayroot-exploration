using Wayroot.Gathering;

namespace Wayroot.Guidance
{
    public enum JourneyTarget
    {
        None,
        Resource,
        Merchant,
        Shelter,
        Wayroot,
        Guardian,
        Bloomwell
    }

    public readonly struct JourneyGuidanceState
    {
        public JourneyGuidanceState(JourneyTarget target, string status, bool hasPointer)
        {
            Target = target;
            Status = status;
            HasPointer = hasPointer;
        }

        public JourneyTarget Target { get; }
        public string Status { get; }
        public bool HasPointer { get; }
    }

    /// <summary>Selects one gentle, existing-world milestone exclusively from the persisted prototype save.</summary>
    public static class JourneyGuidanceRules
    {
        public static JourneyGuidanceState Select(PrototypeGatheringSave save)
        {
            if (save.bloomwellRestored)
            {
                return new JourneyGuidanceState(JourneyTarget.None, Exploration.BloomwellFinalePresentationRules.CompletionJourneyStatus, false);
            }

            if (save.weaponLevel < 1)
            {
                bool canVisitMerchant = save.petals >= 1 && save.slimeCores >= 1;
                return canVisitMerchant
                    ? new JourneyGuidanceState(JourneyTarget.Merchant, "JOURNEY  •  VISIT THE IRON EDGE MERCHANT", true)
                    : new JourneyGuidanceState(JourneyTarget.Resource, "JOURNEY  •  GATHER WHAT THE MEADOW OFFERS", true);
            }

            if (!save.shelterBuilt)
            {
                return new JourneyGuidanceState(JourneyTarget.Shelter, "JOURNEY  •  MAKE A SHELTER IN THE CLEARING", true);
            }

            if (!save.wayrootRestored)
            {
                return new JourneyGuidanceState(JourneyTarget.Wayroot, "JOURNEY  •  RESTORE THE DORMANT WAYROOT", true);
            }

            if (!save.moonlitGladeUnlocked)
            {
                return new JourneyGuidanceState(JourneyTarget.Guardian, "JOURNEY  •  SEEK THE THORN GUARDIAN IN THE GROVE", true);
            }

            return new JourneyGuidanceState(JourneyTarget.Bloomwell, "JOURNEY  •  FOLLOW THE MOONLIT PATH TO THE BLOOMWELL", true);
        }
    }
}

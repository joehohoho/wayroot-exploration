using Wayroot.Gathering;

namespace Wayroot.Guidance
{
    public enum JourneyTarget
    {
        None,
        Wildflower,
        YoungTree,
        StoneOutcrop,
        PracticeSlime,
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
                if (save.petals < 1)
                {
                    return new JourneyGuidanceState(JourneyTarget.Wildflower, "JOURNEY  •  GATHER A WILDFLOWER PETAL", true);
                }

                if (save.slimeCores < 1)
                {
                    return new JourneyGuidanceState(JourneyTarget.PracticeSlime, "JOURNEY  •  FACE THE PRACTICE SLIME FOR A CORE", true);
                }

                return new JourneyGuidanceState(JourneyTarget.Merchant, "JOURNEY  •  VISIT THE IRON EDGE MERCHANT", true);
            }

            if (!save.shelterBuilt)
            {
                if (save.timber < 3)
                {
                    return new JourneyGuidanceState(JourneyTarget.YoungTree, "JOURNEY  •  GATHER TIMBER FOR THE SHELTER", true);
                }

                if (save.stone < 3)
                {
                    return new JourneyGuidanceState(JourneyTarget.StoneOutcrop, "JOURNEY  •  GATHER STONE FOR THE SHELTER", true);
                }

                return new JourneyGuidanceState(JourneyTarget.Shelter, "JOURNEY  •  MAKE A SHELTER IN THE CLEARING", true);
            }

            if (!save.wayrootRestored)
            {
                if (save.petals < 3)
                {
                    return new JourneyGuidanceState(JourneyTarget.Wildflower, "JOURNEY  •  GATHER PETALS FOR THE WAYROOT", true);
                }

                if (save.timber < 3)
                {
                    return new JourneyGuidanceState(JourneyTarget.YoungTree, "JOURNEY  •  GATHER TIMBER FOR THE WAYROOT", true);
                }

                if (save.stone < 3)
                {
                    return new JourneyGuidanceState(JourneyTarget.StoneOutcrop, "JOURNEY  •  GATHER STONE FOR THE WAYROOT", true);
                }

                if (save.slimeCores < 1)
                {
                    return new JourneyGuidanceState(JourneyTarget.PracticeSlime, "JOURNEY  •  FACE THE PRACTICE SLIME FOR A CORE", true);
                }

                return new JourneyGuidanceState(JourneyTarget.Wayroot, "JOURNEY  •  RESTORE THE DORMANT WAYROOT", true);
            }

            if (!save.moonlitGladeUnlocked)
            {
                return new JourneyGuidanceState(JourneyTarget.Guardian, "JOURNEY  •  SEEK THE THORN GUARDIAN IN THE GROVE", true);
            }

            if (save.petals < 2)
            {
                return new JourneyGuidanceState(JourneyTarget.Wildflower, "JOURNEY  •  GATHER PETALS FOR THE BLOOMWELL", true);
            }

            if (save.timber < 2)
            {
                return new JourneyGuidanceState(JourneyTarget.YoungTree, "JOURNEY  •  GATHER TIMBER FOR THE BLOOMWELL", true);
            }

            if (save.stone < 2)
            {
                return new JourneyGuidanceState(JourneyTarget.StoneOutcrop, "JOURNEY  •  GATHER STONE FOR THE BLOOMWELL", true);
            }

            if (save.slimeCores < 1)
            {
                return new JourneyGuidanceState(JourneyTarget.PracticeSlime, "JOURNEY  •  FACE THE PRACTICE SLIME FOR A CORE", true);
            }

            return new JourneyGuidanceState(JourneyTarget.Bloomwell, "JOURNEY  •  FOLLOW THE MOONLIT PATH TO THE BLOOMWELL", true);
        }
    }
}

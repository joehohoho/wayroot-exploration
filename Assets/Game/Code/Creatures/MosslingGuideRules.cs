using System.Collections.Generic;
using UnityEngine;

namespace Wayroot.Creatures
{
    public enum MosslingGuideKind
    {
        None,
        Available,
        Renewing
    }

    /// <summary>Immutable resource-node facts consumed by the Mossling's pure guidance selection.</summary>
    public readonly struct MosslingGuideCandidate
    {
        public MosslingGuideCandidate(string nodeName, Vector3 position, bool isAvailable, long renewalDeadlineUtcTicks)
        {
            NodeName = nodeName;
            Position = position;
            IsAvailable = isAvailable;
            RenewalDeadlineUtcTicks = renewalDeadlineUtcTicks;
        }

        public string NodeName { get; }
        public Vector3 Position { get; }
        public bool IsAvailable { get; }
        public long RenewalDeadlineUtcTicks { get; }
    }

    /// <summary>The guide's compact outcome. Renewing nodes deliberately never receive a target marker.</summary>
    public readonly struct MosslingGuideSelection
    {
        public MosslingGuideSelection(MosslingGuideKind kind, string nodeName, Vector3 position, long renewalDeadlineUtcTicks)
        {
            Kind = kind;
            NodeName = nodeName;
            Position = position;
            RenewalDeadlineUtcTicks = renewalDeadlineUtcTicks;
        }

        public MosslingGuideKind Kind { get; }
        public string NodeName { get; }
        public Vector3 Position { get; }
        public long RenewalDeadlineUtcTicks { get; }
        public bool ShowsTargetMarker => Kind == MosslingGuideKind.Available;
    }

    /// <summary>Pure, stable nearest-node selection for the non-combat Mossling resource guide.</summary>
    public static class MosslingGuideRules
    {
        public static MosslingGuideSelection Select(Vector3 playerPosition, IReadOnlyList<MosslingGuideCandidate> candidates)
        {
            int nearestAvailable = -1;
            float nearestAvailableDistance = float.MaxValue;
            int nearestRenewing = -1;
            float nearestRenewingDistance = float.MaxValue;
            for (int index = 0; index < candidates.Count; index++)
            {
                MosslingGuideCandidate candidate = candidates[index];
                float distance = (candidate.Position - playerPosition).sqrMagnitude;
                if (candidate.IsAvailable)
                {
                    if (distance < nearestAvailableDistance)
                    {
                        nearestAvailable = index;
                        nearestAvailableDistance = distance;
                    }
                }
                else if (candidate.RenewalDeadlineUtcTicks > 0 && distance < nearestRenewingDistance)
                {
                    nearestRenewing = index;
                    nearestRenewingDistance = distance;
                }
            }

            if (nearestAvailable >= 0)
            {
                MosslingGuideCandidate available = candidates[nearestAvailable];
                return new MosslingGuideSelection(MosslingGuideKind.Available, available.NodeName, available.Position, 0);
            }

            if (nearestRenewing >= 0)
            {
                MosslingGuideCandidate renewing = candidates[nearestRenewing];
                return new MosslingGuideSelection(MosslingGuideKind.Renewing, renewing.NodeName, renewing.Position, renewing.RenewalDeadlineUtcTicks);
            }

            return new MosslingGuideSelection(MosslingGuideKind.None, string.Empty, Vector3.zero, 0);
        }
    }
}

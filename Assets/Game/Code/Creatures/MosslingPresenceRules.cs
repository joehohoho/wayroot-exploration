using UnityEngine;

namespace Wayroot.Creatures
{
    /// <summary>Maps existing companion and guide facts to presentation-only Mossling reads.</summary>
    public enum MosslingPresenceState
    {
        Idle,
        Follow,
        Guide,
        Arrival,
        Renewal
    }

    public static class MosslingPresenceRules
    {
        public const float ArrivalRange = 2.25f;
        public const float FollowDistance = 1.7f;

        public static MosslingPresenceState Select(bool isBefriended, MosslingGuideSelection selection, Vector3 playerPosition, Vector3 mosslingPosition)
        {
            if (!isBefriended) return MosslingPresenceState.Idle;
            if (selection.Kind == MosslingGuideKind.Renewing) return MosslingPresenceState.Renewal;
            if (selection.Kind == MosslingGuideKind.Available)
            {
                return (selection.Position - playerPosition).sqrMagnitude <= ArrivalRange * ArrivalRange
                    ? MosslingPresenceState.Arrival
                    : MosslingPresenceState.Guide;
            }

            return (mosslingPosition - playerPosition).sqrMagnitude > FollowDistance * FollowDistance
                ? MosslingPresenceState.Follow
                : MosslingPresenceState.Idle;
        }
    }
}

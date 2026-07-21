using UnityEngine;

namespace Wayroot.Character
{
    /// <summary>Pure timing and direction contract for the one semantic dodge action.</summary>
    public static class DodgeRules
    {
        public const float Distance = 3.2f;
        public const float DurationSeconds = 0.20f;
        public const float CooldownSeconds = 0.90f;
        public const float ImmunitySeconds = 0.20f;

        public static Vector3 ResolveDirection(Vector2 move, Vector3 facing)
        {
            Vector3 direction = new(move.x, 0f, move.y);
            if (direction.sqrMagnitude > 0.0001f) return direction.normalized;

            facing.y = 0f;
            return facing.sqrMagnitude > 0.0001f ? facing.normalized : Vector3.forward;
        }

        public static bool CanStart(bool requested, float now, float nextAvailableAt)
        {
            return requested && now >= nextAvailableAt;
        }

        public static bool IsInvulnerable(float now, float startedAt, float immunitySeconds)
        {
            return now >= startedAt && now < startedAt + immunitySeconds;
        }
    }
}

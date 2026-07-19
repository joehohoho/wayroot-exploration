using UnityEngine;

namespace Wayroot.Character
{
    /// <summary>Pure movement and facing rules for the movement command boundary.</summary>
    public static class MovementRules
    {
        public static Vector2 ClampMoveMagnitude(Vector2 move)
        {
            return move.sqrMagnitude > 1f ? move.normalized : move;
        }

        public static bool TryGetFacing(Vector2 move, Vector3 fallback, out Vector3 facing)
        {
            Vector2 clampedMove = ClampMoveMagnitude(move);
            if (clampedMove.sqrMagnitude < 0.0001f)
            {
                facing = fallback;
                return false;
            }

            facing = new Vector3(clampedMove.x, 0f, clampedMove.y).normalized;
            return true;
        }
    }
}

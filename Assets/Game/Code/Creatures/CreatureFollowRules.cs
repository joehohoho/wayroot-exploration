using UnityEngine;

namespace Wayroot.Creatures
{
    /// <summary>Pure horizontal follow movement that preserves a safe buffer around the player.</summary>
    public static class CreatureFollowRules
    {
        public static Vector3 GetNextPosition(Vector3 current, Vector3 player, float stopDistance, float speed, float deltaTime)
        {
            Vector3 flatPlayer = new(player.x, current.y, player.z);
            Vector3 offset = current - flatPlayer;
            float distance = offset.magnitude;
            if (distance <= stopDistance || deltaTime <= 0f || speed <= 0f)
            {
                return current;
            }

            Vector3 destination = flatPlayer + (offset / distance) * stopDistance;
            return Vector3.MoveTowards(current, destination, speed * deltaTime);
        }
    }
}

using UnityEngine;

namespace Wayroot.Exploration
{
    /// <summary>Bounded presentation tuning for the existing persistent Bloomwell finale.</summary>
    public static class BloomwellFinalePresentationRules
    {
        public const float ClimaxDurationSeconds = 1.35f;
        public const float OrbitRadius = 1.08f;
        public const string CompletionJourneyStatus = "JOURNEY  •  BLOOMWELL RESTORED — EXPLORE FREELY";

        public static bool IsClimaxActive(float elapsed)
        {
            return elapsed >= 0f && elapsed < ClimaxDurationSeconds;
        }

        public static Vector3 GetOrbitPosition(Vector3 center, int index, float elapsed)
        {
            float angle = index * Mathf.PI * 2f / 6f + elapsed * 2.8f;
            return center + new Vector3(Mathf.Cos(angle) * OrbitRadius, 1.02f + 0.18f * Mathf.Sin(angle * 2f), Mathf.Sin(angle) * OrbitRadius);
        }
    }
}

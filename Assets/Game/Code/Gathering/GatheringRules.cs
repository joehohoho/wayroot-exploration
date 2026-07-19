namespace Wayroot.Gathering
{
    /// <summary>Pure progress rules; reward issuance belongs to the gathering controller.</summary>
    public static class GatheringRules
    {
        public static bool TryAdvance(int currentSteps, int requiredSteps, out int nextSteps, out bool isComplete)
        {
            nextSteps = currentSteps;
            isComplete = false;
            if (requiredSteps <= 0 || currentSteps < 0 || currentSteps >= requiredSteps)
            {
                return false;
            }

            nextSteps = currentSteps + 1;
            isComplete = nextSteps == requiredSteps;
            return true;
        }
    }
}

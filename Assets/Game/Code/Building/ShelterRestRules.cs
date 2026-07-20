namespace Wayroot.Building
{
    /// <summary>Pure Phase 12 rules for the single shelter rest and return utility.</summary>
    public static class ShelterRestRules
    {
        public static bool CanRest(bool shelterBuilt) => shelterBuilt;

        public static RespawnDestination GetRespawnDestination(bool hasActiveReturnPoint)
        {
            return hasActiveReturnPoint ? RespawnDestination.ActiveShelter : RespawnDestination.DefaultSpawn;
        }
    }

    public enum RespawnDestination
    {
        DefaultSpawn,
        ActiveShelter
    }
}

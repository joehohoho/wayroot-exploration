namespace Wayroot.Combat
{
    public readonly struct EnemyCombatProfile
    {
        public EnemyCombatProfile(string displayName, int maxHealth, int contactDamage, float respawnDelaySeconds, float chaseSpeed, float chaseRange)
        {
            DisplayName = displayName;
            MaxHealth = maxHealth;
            ContactDamage = contactDamage;
            RespawnDelaySeconds = respawnDelaySeconds;
            ChaseSpeed = chaseSpeed;
            ChaseRange = chaseRange;
        }

        public string DisplayName { get; }
        public int MaxHealth { get; }
        public int ContactDamage { get; }
        public float RespawnDelaySeconds { get; }
        public float ChaseSpeed { get; }
        public float ChaseRange { get; }
    }
}

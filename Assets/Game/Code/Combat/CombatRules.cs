namespace Wayroot.Combat
{
    public static class CombatRules
    {
        public static int ApplyDamage(int currentHealth, int damage, out bool defeated)
        {
            int health = currentHealth - damage;
            if (health < 0) health = 0;
            defeated = health == 0;
            return health;
        }

        public static bool CanAttack(float distance, float range, float elapsedSinceAttack, float cooldown)
        {
            return distance <= range && elapsedSinceAttack >= cooldown;
        }
    }
}

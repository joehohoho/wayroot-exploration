using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeEnemy : MonoBehaviour
    {
        [SerializeField] private int health = 5;
        [SerializeField] private Renderer body = null!;
        public bool IsDefeated => health <= 0;
        public int Health => health;
        public void Configure(Renderer renderer) => body = renderer;
        public void TakeDamage(int damage)
        {
            health = CombatRules.ApplyDamage(health, damage, out bool defeated);
            if (defeated) body.enabled = false;
        }
    }
}

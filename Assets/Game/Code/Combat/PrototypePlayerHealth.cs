using UnityEngine;
using Wayroot.UI;

namespace Wayroot.Combat
{
    public sealed class PrototypePlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 10;
        private int _health;
        private Vector3 _home;
        private ActionFeedbackHud? _feedback;
        public int Health => _health;
        private void Awake() { _health = maxHealth; _home = transform.position; }
        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void TakeDamage(int damage)
        {
            _health = CombatRules.ApplyDamage(_health, damage, out bool defeated);
            if (defeated) { transform.position = _home; _health = maxHealth; _feedback?.Show("RESPAWNED: HEALTH RESTORED"); }
        }
    }
}

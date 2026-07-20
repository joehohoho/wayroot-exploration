using UnityEngine;
using Wayroot.UI;

namespace Wayroot.Combat
{
    public sealed class PrototypeEnemy : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private float respawnDelay = 5f;
        [SerializeField] private Renderer body = null!;
        private int _health;
        private Collider _collider = null!;
        private Vector3 _home;
        private float _respawnAt;
        private ActionFeedbackHud? _feedback;
        public bool IsDefeated => _health <= 0;
        public int Health => _health;
        public event System.Action? Defeated;
        private void Awake()
        {
            _health = maxHealth;
            _home = transform.position;
            _collider = GetComponent<Collider>();
        }
        private void Update()
        {
            if (IsDefeated && Time.time >= _respawnAt) Respawn();
        }
        public void Configure(Renderer renderer) => body = renderer;
        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void TakeDamage(int damage)
        {
            _health = CombatRules.ApplyDamage(_health, damage, out bool defeated);
            if (!defeated) return;
            _respawnAt = Time.time + respawnDelay;
            body.enabled = false;
            _collider.enabled = false;
            Defeated?.Invoke();
        }
        private void Respawn()
        {
            transform.position = _home;
            _health = maxHealth;
            body.enabled = true;
            _collider.enabled = true;
            _feedback?.Show("SLIME RESPAWNED");
        }
    }
}

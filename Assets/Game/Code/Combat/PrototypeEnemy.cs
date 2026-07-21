using UnityEngine;
using Wayroot.Art;
using Wayroot.UI;

namespace Wayroot.Combat
{
    public sealed class PrototypeEnemy : MonoBehaviour
    {
        [SerializeField] private Renderer body = null!;
        private EnemyCombatProfile _profile;
        private int _health;
        private Collider _collider = null!;
        private Vector3 _home;
        private float _respawnAt;
        private float _lastHitAt = float.NegativeInfinity;
        private float _lastRespawnAt = float.NegativeInfinity;
        private bool _hidePrimitiveVisual;
        private ActionFeedbackHud? _feedback;

        public bool IsDefeated => _health <= 0;
        public int Health => _health;
        public int MaxHealth => _profile.MaxHealth;
        public string DisplayName => _profile.DisplayName;
        public float HitElapsed => Time.time - _lastHitAt;
        public float RespawnElapsed => Time.time - _lastRespawnAt;
        public event System.Action? Defeated;

        private void Awake()
        {
            _profile = new EnemyCombatProfile("SLIME", ThornGuardianRules.PracticeSlimeHealth, ThornGuardianRules.PracticeSlimeContactDamage, 5f, 1.5f, 6f);
            _health = _profile.MaxHealth;
            _home = transform.position;
            _collider = GetComponent<Collider>();
        }

        private void Update()
        {
            if (IsDefeated && Time.time >= _respawnAt) Respawn();
        }

        public void Configure(Renderer renderer, EnemyCombatProfile profile)
        {
            body = renderer;
            _profile = profile;
            _health = profile.MaxHealth;
        }

        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;

        public void HidePrimitiveVisual()
        {
            _hidePrimitiveVisual = true;
            body.enabled = false;
        }

        public void TakeDamage(int damage)
        {
            if (IsDefeated) return;
            _lastHitAt = Time.time;
            _health = CombatRules.ApplyDamage(_health, damage, out bool defeated);
            GetComponent<ProceduralStylizedAnimator>()?.Emphasize(defeated ? 0.48f : 0.22f);
            if (!defeated) return;

            _respawnAt = Time.time + _profile.RespawnDelaySeconds;
            body.enabled = false;
            _collider.enabled = false;
            Defeated?.Invoke();
        }

        private void Respawn()
        {
            _lastRespawnAt = Time.time;
            transform.position = _home;
            _health = _profile.MaxHealth;
            body.enabled = !_hidePrimitiveVisual;
            _collider.enabled = true;
            _feedback?.Show($"{DisplayName} RESPAWNED");
        }
    }
}

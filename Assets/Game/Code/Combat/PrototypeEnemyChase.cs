using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeEnemyChase : MonoBehaviour
    {
        private Transform _player = null!;
        private PrototypePlayerHealth _health = null!;
        private PrototypeEnemy _enemy = null!;
        private EnemyCombatProfile _profile;
        private float _nextDamage;

        public void Configure(Transform player, PrototypePlayerHealth health, EnemyCombatProfile profile)
        {
            _player = player;
            _health = health;
            _profile = profile;
            _enemy = GetComponent<PrototypeEnemy>();
        }

        private void Update()
        {
            if (!_enemy.gameObject.activeInHierarchy || _enemy.IsDefeated) return;
            Vector3 delta = _player.position - transform.position;
            delta.y = 0f;
            float rangeSquared = _profile.ChaseRange * _profile.ChaseRange;
            if (delta.sqrMagnitude > rangeSquared || delta.sqrMagnitude < 0.01f) return;

            transform.position += delta.normalized * (_profile.ChaseSpeed * Time.deltaTime);
            if (delta.sqrMagnitude <= 1.6f && Time.time >= _nextDamage)
            {
                _nextDamage = Time.time + 1f;
                _health.TakeDamage(_profile.ContactDamage);
            }
        }
    }
}

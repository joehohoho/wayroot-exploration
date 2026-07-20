using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeEnemyChase : MonoBehaviour
    {
        private Transform _player = null!;
        private PrototypePlayerHealth _health = null!;
        private float _nextDamage;
        public void Configure(Transform player, PrototypePlayerHealth health) { _player = player; _health = health; }
        private void Update()
        {
            Vector3 delta = _player.position - transform.position; delta.y = 0f;
            if (delta.sqrMagnitude > 36f || delta.sqrMagnitude < 0.01f) return;
            transform.position += delta.normalized * (1.5f * Time.deltaTime);
            if (delta.sqrMagnitude <= 1.6f && Time.time >= _nextDamage) { _nextDamage = Time.time + 1f; _health.TakeDamage(1); }
        }
    }
}

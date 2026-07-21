using UnityEngine;
using Wayroot.Art;
using Wayroot.Audio;

namespace Wayroot.Combat
{
    public sealed class PrototypeEnemyChase : MonoBehaviour
    {
        private Transform _player = null!;
        private PrototypePlayerHealth _health = null!;
        private PrototypeEnemy _enemy = null!;
        private EnemyCombatProfile _profile;
        private float _nextDamage;
        private float _contactAnticipationStartedAt = float.NegativeInfinity;
        private ProceduralSoundscape? _soundscape;

        public const float ContactAnticipationSeconds = 0.42f;
        public bool IsAnticipating => Time.time >= _contactAnticipationStartedAt && Time.time < _contactAnticipationStartedAt + ContactAnticipationSeconds;

        public void Configure(Transform player, PrototypePlayerHealth health, EnemyCombatProfile profile)
        {
            _player = player;
            _health = health;
            _profile = profile;
            _enemy = GetComponent<PrototypeEnemy>();
        }

        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        private void Update()
        {
            if (!_enemy.gameObject.activeInHierarchy || _enemy.IsDefeated) return;
            Vector3 delta = _player.position - transform.position;
            delta.y = 0f;
            float rangeSquared = _profile.ChaseRange * _profile.ChaseRange;
            if (delta.sqrMagnitude > rangeSquared || delta.sqrMagnitude < 0.01f) return;

            if (!IsAnticipating)
            {
                transform.position += delta.normalized * (_profile.ChaseSpeed * Time.deltaTime);
            }

            if (delta.sqrMagnitude > 1.6f)
            {
                _contactAnticipationStartedAt = float.NegativeInfinity;
                return;
            }

            if (Time.time >= _nextDamage && _contactAnticipationStartedAt < 0f)
            {
                _contactAnticipationStartedAt = Time.time;
                GetComponent<ProceduralStylizedAnimator>()?.Emphasize(ContactAnticipationSeconds);
                _soundscape?.Play(SoundscapeCue.EnemyAnticipate);
                return;
            }

            if (_contactAnticipationStartedAt >= 0f && Time.time >= _contactAnticipationStartedAt + ContactAnticipationSeconds)
            {
                _nextDamage = Time.time + 1f;
                _contactAnticipationStartedAt = float.NegativeInfinity;
                _health.TakeDamage(_profile.ContactDamage);
            }
        }
    }
}

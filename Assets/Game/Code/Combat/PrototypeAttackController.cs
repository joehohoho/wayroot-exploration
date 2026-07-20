using Wayroot.Character;
using Wayroot.Input;
using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeAttackController : MonoBehaviour
    {
        private const float Range = 2f, Cooldown = 0.4f;
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeEnemy _enemy = null!;
        private float _lastAttack = -100f;
        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeEnemy enemy) { _input = input; _player = player; _enemy = enemy; }
        private void Update()
        {
            if (_player.IsPaused || !_input.AttackHeld || _enemy.IsDefeated) return;
            float distance = Vector3.Distance(_player.transform.position, _enemy.transform.position);
            if (!CombatRules.CanAttack(distance, Range, Time.time - _lastAttack, Cooldown)) return;
            _lastAttack = Time.time; _enemy.TakeDamage(1);
        }
    }
}

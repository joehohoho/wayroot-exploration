using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeAttackController : MonoBehaviour
    {
        private const float Range = 2f, Cooldown = 0.4f;
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeEnemy _enemy = null!;
        private PrototypeGatheringController _inventory = null!;
        private float _lastAttack = -100f;
        private ActionFeedbackHud? _feedback;
        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeEnemy enemy, PrototypeGatheringController inventory)
        {
            _input = input; _player = player; _enemy = enemy; _inventory = inventory; _enemy.Defeated += AwardCore;
        }
        private void Update()
        {
            if (_player.IsPaused || !_input.AttackHeld || _enemy.IsDefeated) return;
            float distance = Vector3.Distance(_player.transform.position, _enemy.transform.position);
            if (!CombatRules.CanAttack(distance, Range, Time.time - _lastAttack, Cooldown)) return;
            _lastAttack = Time.time;
            _enemy.TakeDamage(_inventory.AttackDamage);
            _feedback?.Show(_enemy.IsDefeated ? "SLIME DEFEATED: CORE DROPPED" : $"HIT: SLIME {_enemy.Health}/5");
        }
        private void AwardCore() => _inventory.AwardCombatCore();
        private void OnDestroy() { if (_enemy != null) _enemy.Defeated -= AwardCore; }
    }
}

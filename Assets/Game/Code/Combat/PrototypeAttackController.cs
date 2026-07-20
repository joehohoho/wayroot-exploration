using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using Wayroot.Audio;
using Wayroot.Art;
using UnityEngine;

namespace Wayroot.Combat
{
    public sealed class PrototypeAttackController : MonoBehaviour
    {
        private const float Range = 2f;
        private const float Cooldown = 0.4f;
        private readonly List<PrototypeEnemy> _enemies = new();
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _inventory = null!;
        private float _lastAttack = -100f;
        private ActionFeedbackHud? _feedback;
        private ProceduralSoundscape? _soundscape;

        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController inventory, params PrototypeEnemy[] enemies)
        {
            _input = input;
            _player = player;
            _inventory = inventory;
            foreach (PrototypeEnemy enemy in enemies)
            {
                _enemies.Add(enemy);
                enemy.Defeated += AwardCore;
            }
        }

        private void Update()
        {
            if (_player.IsPaused || !_input.AttackHeld) return;
            PrototypeEnemy? target = FindAttackTarget();
            if (target == null || !CombatRules.CanAttack(Vector3.Distance(_player.transform.position, target.transform.position), Range, Time.time - _lastAttack, Cooldown)) return;

            _lastAttack = Time.time;
            target.TakeDamage(_inventory.AttackDamage);
            _player.GetComponent<ProceduralStylizedAnimator>()?.Emphasize();
            _soundscape?.Play(target.IsDefeated ? SoundscapeCue.Defeat : SoundscapeCue.CombatHit);
            _feedback?.Show(target.IsDefeated
                ? $"{target.DisplayName} DEFEATED: +1 CORE"
                : $"HIT: {target.DisplayName} {target.Health}/{target.MaxHealth}");
        }

        private PrototypeEnemy? FindAttackTarget()
        {
            PrototypeEnemy? nearest = null;
            float nearestDistance = Range;
            foreach (PrototypeEnemy enemy in _enemies)
            {
                if (!enemy.gameObject.activeInHierarchy || enemy.IsDefeated) continue;
                float distance = Vector3.Distance(_player.transform.position, enemy.transform.position);
                if (distance <= nearestDistance)
                {
                    nearest = enemy;
                    nearestDistance = distance;
                }
            }

            return nearest;
        }

        private void AwardCore() => _inventory.AwardCombatCore();

        private void OnDestroy()
        {
            foreach (PrototypeEnemy enemy in _enemies)
            {
                if (enemy != null) enemy.Defeated -= AwardCore;
            }
        }
    }
}

using Wayroot.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    public sealed class CombatHud : MonoBehaviour
    {
        private Text _text = null!;
        private PrototypePlayerHealth _player = null!;
        private PrototypeEnemy _enemy = null!;
        public void Configure(Text text, PrototypePlayerHealth player, PrototypeEnemy enemy) { _text = text; _player = player; _enemy = enemy; }
        private void Update() => _text.text = _enemy.IsDefeated ? $"HEALTH {_player.Health}/10   SLIME DEFEATED" : $"HEALTH {_player.Health}/10   SLIME {_enemy.Health}/5";
    }
}

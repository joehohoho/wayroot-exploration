using Wayroot.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Compact, player-facing vitality card with no debug or raw progression strings.</summary>
    public sealed class CombatHud : MonoBehaviour
    {
        private Text _text = null!;
        private PrototypePlayerHealth _player = null!;
        private PrototypeEnemy _slime = null!;
        private RestoredGroveController _grove = null!;

        public void Configure(Text text, PrototypePlayerHealth player, PrototypeEnemy slime, RestoredGroveController grove)
        {
            _text = text;
            _player = player;
            _slime = slime;
            _grove = grove;
        }

        private void Update()
        {
            string threat = _slime.IsDefeated ? "MEADOW CLEAR" : $"SLIME {_slime.Health}/{_slime.MaxHealth}";
            string returnPoint = _player.HasActiveShelterReturnPoint ? "HOME SET" : "HOME: MEADOW";
            _text.text = $"HEALTH  {_player.Health}/10\n{threat}  •  {returnPoint}";
        }
    }
}

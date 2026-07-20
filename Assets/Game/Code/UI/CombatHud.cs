using Wayroot.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
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
            string returnPoint = _player.HasActiveShelterReturnPoint ? "RETURN SHELTER" : "RETURN DEFAULT";
            string slime = _slime.IsDefeated ? "SLIME DEFEATED" : $"SLIME {_slime.Health}/{_slime.MaxHealth}";
            string grove = _grove.IsOpen ? "GROVE OPEN: GUARDIAN 8 HP / 2 DMG" : "GROVE LOCKED: RESTORE WAYROOT";
            _text.text = $"HEALTH {_player.Health}/10   {slime}   {returnPoint}\n{grove}";
        }
    }
}

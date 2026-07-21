using Wayroot.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Compact readable cooldown presentation; no gameplay authority lives here.</summary>
    public sealed class DodgeCooldownHud : MonoBehaviour
    {
        private Text _text = null!;
        private PrototypePlayerController _player = null!;

        public void Configure(Text text, PrototypePlayerController player)
        {
            _text = text;
            _player = player;
        }

        private void Update()
        {
            float remaining = _player.DodgeCooldownRemaining;
            _text.text = remaining <= 0f ? "DODGE\nREADY" : $"DODGE\n{remaining:0.0}s";
        }
    }
}

using Wayroot.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Keeps the compact safe-area sound toggle readable and backed by the persisted prototype preference.</summary>
    public sealed class SoundToggleButton : MonoBehaviour
    {
        [SerializeField] private Text label = null!;
        [SerializeField] private ProceduralSoundscape soundscape = null!;

        public void Configure(Text text, ProceduralSoundscape proceduralSoundscape)
        {
            label = text;
            soundscape = proceduralSoundscape;
            GetComponent<Button>().onClick.AddListener(Toggle);
            RefreshLabel();
        }

        private void Toggle()
        {
            soundscape.ToggleSound();
            RefreshLabel();
        }

        private void RefreshLabel()
        {
            label.text = soundscape.IsSoundEnabled ? "SOUND ON" : "SOUND OFF";
        }
    }
}

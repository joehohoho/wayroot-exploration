using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using Wayroot.Audio;
using UnityEngine;

namespace Wayroot.Wayroot
{
    /// <summary>Range-gates the existing semantic interaction command at the one persistent Wayroot landmark.</summary>
    public sealed class PrototypeWayrootController : MonoBehaviour
    {
        private const float InteractionRange = 2.8f;
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _gathering = null!;
        private GameObject _restoredVisual = null!;
        private Renderer _dormantRenderer = null!;
        private TextMesh _label = null!;
        private bool _wasInteracting;
        private ActionFeedbackHud? _feedback;
        private ProceduralSoundscape? _soundscape;

        public bool IsInRange { get; private set; }
        public string Status { get; private set; } = "WAYROOT: restore the clearing";

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, GameObject restoredVisual, Renderer dormantRenderer, TextMesh label)
        {
            _input = input;
            _player = player;
            _gathering = gathering;
            _restoredVisual = restoredVisual;
            _dormantRenderer = dormantRenderer;
            _label = label;
            ApplyRestoredVisual(_gathering.WayrootRestored);
        }

        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        private void Update()
        {
            IsInRange = !_player.IsPaused
                && (transform.position - _player.transform.position).sqrMagnitude <= InteractionRange * InteractionRange;
            bool interacting = IsInRange && _input.InteractHeld;
            if (interacting && !_wasInteracting)
            {
                _gathering.TryRestoreWayroot(out string status);
                Status = status;
                _feedback?.Show(status);
                if (_gathering.WayrootRestored) _soundscape?.Play(SoundscapeCue.WayrootRestore);
                ApplyRestoredVisual(_gathering.WayrootRestored);
            }

            _wasInteracting = interacting;
        }

        private void ApplyRestoredVisual(bool restored)
        {
            _restoredVisual.SetActive(restored);
            _label.text = restored ? "WAYROOT  •  RESTORED" : "WAYROOT  •  DORMANT";
            Material material = _dormantRenderer.material;
            Color color = restored ? new Color(0.22f, 0.72f, 0.50f) : new Color(0.24f, 0.16f, 0.38f);
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }
            else
            {
                material.color = color;
            }
        }
    }
}

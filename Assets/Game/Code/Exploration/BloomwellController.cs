using System;
using Wayroot.Audio;
using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using UnityEngine;

namespace Wayroot.Exploration
{
    /// <summary>Range-gates the existing semantic gather command at the Guardian-gated Bloomwell finale landmark.</summary>
    public sealed class BloomwellController : MonoBehaviour
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
        public string Status { get; private set; } = "BLOOMWELL: gather its restoration.";
        public event Action? Restored;

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, GameObject restoredVisual, Renderer dormantRenderer, TextMesh label)
        {
            _input = input;
            _player = player;
            _gathering = gathering;
            _restoredVisual = restoredVisual;
            _dormantRenderer = dormantRenderer;
            _label = label;
            ApplyRestoredVisual(_gathering.BloomwellRestored);
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
                bool restored = _gathering.TryRestoreBloomwell(out string status);
                Status = status;
                _feedback?.Show(status);
                if (restored)
                {
                    _soundscape?.Play(SoundscapeCue.BloomwellRestore);
                    ApplyRestoredVisual(true);
                    Restored?.Invoke();
                }
            }

            _wasInteracting = interacting;
        }

        private void ApplyRestoredVisual(bool restored)
        {
            _restoredVisual.SetActive(restored);
            Status = restored ? BloomwellRestorationRules.CompleteStatus : "BLOOMWELL: gather its restoration.";
            _label.text = restored ? "BLOOMWELL\nRESTORED" : "BLOOMWELL\nHOLD GATHER";
            Material material = _dormantRenderer.material;
            Color color = restored ? new Color(0.52f, 0.92f, 0.84f) : new Color(0.30f, 0.25f, 0.52f);
            if (material.HasProperty("_BaseColor")) material.SetColor("_BaseColor", color);
            else material.color = color;
        }
    }
}

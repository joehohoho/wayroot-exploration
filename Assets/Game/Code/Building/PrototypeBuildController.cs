using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using UnityEngine;

namespace Wayroot.Building
{
    /// <summary>Range-gates the existing semantic interaction command at the one Phase 5 build plot.</summary>
    public sealed class PrototypeBuildController : MonoBehaviour
    {
        private const float InteractionRange = 2.8f;
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _gathering = null!;
        private GameObject _shelter = null!;
        private Renderer _plotRenderer = null!;
        private bool _wasInteracting;
        private ActionFeedbackHud? _feedback;

        public bool IsInRange { get; private set; }
        public string Status { get; private set; } = "SHELTER: 3 TIMBER + 3 STONE";
        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, GameObject shelter, Renderer plotRenderer)
        {
            _input = input;
            _player = player;
            _gathering = gathering;
            _shelter = shelter;
            _plotRenderer = plotRenderer;
            ApplyBuiltVisual(_gathering.ShelterBuilt);
            if (_gathering.ShelterBuilt)
            {
                Status = "SHELTER already built.";
            }
        }

        private void Update()
        {
            IsInRange = !_player.IsPaused
                && (transform.position - _player.transform.position).sqrMagnitude <= InteractionRange * InteractionRange;
            bool interacting = IsInRange && _input.InteractHeld;
            if (interacting && !_wasInteracting)
            {
                _gathering.TryBuildShelter(out string status);
                Status = status;
                _feedback?.Show(Status);
                ApplyBuiltVisual(_gathering.ShelterBuilt);
            }

            _wasInteracting = interacting;
        }

        private void ApplyBuiltVisual(bool built)
        {
            _shelter.SetActive(built);
            Material material = _plotRenderer.material;
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", built ? new Color(0.3f, 0.62f, 0.35f) : new Color(0.77f, 0.58f, 0.22f));
            }
            else
            {
                material.color = built ? new Color(0.3f, 0.62f, 0.35f) : new Color(0.77f, 0.58f, 0.22f);
            }
        }
    }
}

using Wayroot.Character;
using Wayroot.Combat;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using Wayroot.Audio;
using UnityEngine;

namespace Wayroot.Building
{
    /// <summary>Range-gates the existing semantic interaction command at the fixed shelter build/rest location.</summary>
    public sealed class PrototypeBuildController : MonoBehaviour
    {
        private const float InteractionRange = 2.8f;
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _gathering = null!;
        private PrototypePlayerHealth _playerHealth = null!;
        private GameObject _shelter = null!;
        private Renderer _plotRenderer = null!;
        private TextMesh _shelterLabel = null!;
        private ShelterCozyPresentation _cozyPresentation = null!;
        private Vector3 _shelterReturnPoint;
        private bool _wasInteracting;
        private ActionFeedbackHud? _feedback;
        private ProceduralSoundscape? _soundscape;

        public bool IsInRange { get; private set; }
        public string Status { get; private set; } = "SHELTER: 3 TIMBER + 3 STONE";

        public void SetFeedback(ActionFeedbackHud feedback)
        {
            _feedback = feedback;
            if (_gathering.HasActiveShelterReturnPoint) _feedback.Show("RETURN POINT RESTORED: SHELTER ACTIVE.");
        }

        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, PrototypePlayerHealth playerHealth, GameObject shelter, Renderer plotRenderer, TextMesh shelterLabel, Vector3 shelterReturnPoint, ShelterCozyPresentation cozyPresentation)
        {
            _input = input;
            _player = player;
            _gathering = gathering;
            _playerHealth = playerHealth;
            _shelter = shelter;
            _plotRenderer = plotRenderer;
            _shelterLabel = shelterLabel;
            _shelterReturnPoint = shelterReturnPoint;
            _cozyPresentation = cozyPresentation;
            if (_gathering.HasActiveShelterReturnPoint) _playerHealth.ActivateShelterReturnPoint(_shelterReturnPoint);
            ApplyBuiltVisual(_gathering.ShelterBuilt);
            if (_gathering.ShelterBuilt)
            {
                Status = _gathering.HasActiveShelterReturnPoint ? "SHELTER ACTIVE HOME." : "SHELTER: REST TO ACTIVATE HOME.";
            }
        }

        private void Update()
        {
            IsInRange = !_player.IsPaused
                && (transform.position - _player.transform.position).sqrMagnitude <= InteractionRange * InteractionRange;
            bool interacting = IsInRange && _input.InteractHeld;
            if (interacting && !_wasInteracting)
            {
                if (_gathering.ShelterBuilt)
                {
                    _gathering.TryRestAtShelter(out string status);
                    if (_gathering.HasActiveShelterReturnPoint) _playerHealth.ActivateShelterReturnPoint(_shelterReturnPoint);
                    Status = status;
                    _cozyPresentation.PlayRestFeedback();
                }
                else
                {
                    _gathering.TryBuildShelter(out string status);
                    Status = status;
                }

                _feedback?.Show(Status);
                if (_gathering.ShelterBuilt) _soundscape?.Play(SoundscapeCue.ShelterRest);
                ApplyBuiltVisual(_gathering.ShelterBuilt);
            }

            _wasInteracting = interacting;
        }

        private void ApplyBuiltVisual(bool built)
        {
            _shelter.SetActive(built);
            _cozyPresentation.ApplyState(built);
            _shelterLabel.text = ShelterCozyArtRules.GetStateLabel(built, _gathering.HasActiveShelterReturnPoint);
            Material material = _plotRenderer.material;
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", ShelterCozyArtRules.GetPlotColor(built));
            }
            else
            {
                material.color = ShelterCozyArtRules.GetPlotColor(built);
            }
        }
    }
}

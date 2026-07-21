using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using Wayroot.UI;
using Wayroot.Audio;
using UnityEngine;

namespace Wayroot.Creatures
{
    /// <summary>Owns the one friendly creature's befriend prompt, shelter restore, and safe player follow loop.</summary>
    public sealed class PrototypeCreatureController : MonoBehaviour
    {
        private const float InteractionRange = 2.8f;
        private const float HomeActivationRange = 3.5f;
        private const float FollowStopDistance = 1.5f;
        private const float FollowSpeed = 4f;

        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _gathering = null!;
        private Vector3 _shelterHome;
        private bool _wasInteracting;
        private bool _waitingAtShelter;
        private ActionFeedbackHud? _feedback;
        private MosslingResourceGuide? _resourceGuide;
        private ProceduralSoundscape? _soundscape;

        public bool IsInRange { get; private set; }
        public bool IsBefriended => _gathering.CreatureBefriended;
        public string Status { get; private set; } = "MOSSling: hold E / GATHER to befriend.";
        public string GuideStatus => _resourceGuide?.Status ?? string.Empty;
        public bool IsCelebratingFinale { get; private set; }
        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;

        public void SetFinaleCelebration(bool celebrating)
        {
            IsCelebratingFinale = celebrating;
            if (celebrating) Status = "MOSSling celebrates the restored Bloomwell.";
        }
        public void SetResourceGuide(MosslingResourceGuide resourceGuide) => _resourceGuide = resourceGuide;
        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, Vector3 shelterHome)
        {
            _input = input;
            _player = player;
            _gathering = gathering;
            _shelterHome = shelterHome;
            if (IsBefriended)
            {
                transform.position = _shelterHome;
                _waitingAtShelter = true;
                Status = "MOSSling is waiting at the shelter.";
            }

            _resourceGuide?.SetGuidanceEnabled(IsBefriended);
        }

        private void Update()
        {
            if (!IsBefriended)
            {
                _resourceGuide?.SetGuidanceEnabled(false);
                UpdateBefriendPrompt();
                return;
            }

            _resourceGuide?.SetGuidanceEnabled(true);
            UpdateFollow();
        }

        private void UpdateBefriendPrompt()
        {
            IsInRange = !_player.IsPaused
                && (transform.position - _player.transform.position).sqrMagnitude <= InteractionRange * InteractionRange;
            bool interacting = IsInRange && _input.InteractHeld;
            if (interacting && !_wasInteracting)
            {
                _gathering.BefriendCreature();
                Status = "MOSSling befriended: it will follow safely.";
                _feedback?.Show(Status);
                _soundscape?.Play(SoundscapeCue.MosslingGuide);
                _waitingAtShelter = false;
                _resourceGuide?.SetGuidanceEnabled(true);
            }

            _wasInteracting = interacting;
        }

        private void UpdateFollow()
        {
            IsInRange = false;
            _wasInteracting = false;
            if (_player.IsPaused)
            {
                return;
            }

            if (_waitingAtShelter)
            {
                float homeDistance = (transform.position - _player.transform.position).sqrMagnitude;
                if (homeDistance > HomeActivationRange * HomeActivationRange)
                {
                    return;
                }

                _waitingAtShelter = false;
            }

            transform.position = CreatureFollowRules.GetNextPosition(
                transform.position,
                _player.transform.position,
                FollowStopDistance,
                FollowSpeed,
                Time.deltaTime);
            Status = IsCelebratingFinale ? "MOSSling celebrates the restored Bloomwell." : "MOSSling companion: following safely.";
        }
    }
}

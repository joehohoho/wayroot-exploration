using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
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

        public bool IsInRange { get; private set; }
        public bool IsBefriended => _gathering.CreatureBefriended;
        public string Status { get; private set; } = "MOSSling: hold E / GATHER to befriend.";

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
        }

        private void Update()
        {
            if (!IsBefriended)
            {
                UpdateBefriendPrompt();
                return;
            }

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
                _waitingAtShelter = false;
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
            Status = "MOSSling companion: following safely.";
        }
    }
}

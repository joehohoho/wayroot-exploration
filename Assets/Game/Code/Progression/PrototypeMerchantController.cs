using Wayroot.Character;
using Wayroot.Gathering;
using Wayroot.Input;
using UnityEngine;

namespace Wayroot.Progression
{
    /// <summary>Range-gates the existing semantic interaction command at the one Phase 4 merchant.</summary>
    public sealed class PrototypeMerchantController : MonoBehaviour
    {
        private const float InteractionRange = 2.8f;
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _progression = null!;
        private bool _wasInteracting;

        public bool IsInRange { get; private set; }
        public string Status { get; private set; } = "IRON EDGE: 1 PETAL + 1 CORE -> ATK 2";

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController progression)
        {
            _input = input;
            _player = player;
            _progression = progression;
        }

        private void Update()
        {
            IsInRange = !_player.IsPaused
                && (transform.position - _player.transform.position).sqrMagnitude <= InteractionRange * InteractionRange;
            bool interacting = IsInRange && _input.InteractHeld;
            if (interacting && !_wasInteracting)
            {
                _progression.TryPurchaseWeaponUpgrade(out string status);
                Status = status;
            }

            _wasInteracting = interacting;
        }
    }
}

using Wayroot.Gathering;
using Wayroot.UI;
using UnityEngine;

namespace Wayroot.Combat
{
    /// <summary>Gates the single Phase 13 grove composition behind the persisted first Wayroot restoration.</summary>
    public sealed class RestoredGroveController : MonoBehaviour
    {
        private PrototypeGatheringController _gathering = null!;
        private GameObject _grove = null!;
        private PrototypeEnemy _guardian = null!;
        private ActionFeedbackHud? _feedback;
        private bool _wasOpen;

        public bool IsOpen { get; private set; }
        public PrototypeEnemy Guardian => _guardian;

        public void Configure(PrototypeGatheringController gathering, GameObject grove, PrototypeEnemy guardian)
        {
            _gathering = gathering;
            _grove = grove;
            _guardian = guardian;
            ApplyAvailability(false);
        }

        public void SetFeedback(ActionFeedbackHud feedback)
        {
            _feedback = feedback;
            _guardian.SetFeedback(feedback);
        }

        private void Update()
        {
            ApplyAvailability(true);
        }

        private void ApplyAvailability(bool announce)
        {
            bool open = ThornGuardianRules.CanEnterRestoredGrove(_gathering.WayrootRestored);
            if (open == IsOpen) return;

            IsOpen = open;
            _grove.SetActive(open);
            if (open && announce && !_wasOpen)
            {
                _feedback?.Show("RESTORED GROVE OPEN: THORN GUARDIAN AWAITS");
            }

            _wasOpen = open;
        }
    }
}

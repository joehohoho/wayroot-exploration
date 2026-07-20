using System.Collections.Generic;
using Wayroot.Combat;
using Wayroot.Gathering;
using Wayroot.UI;
using UnityEngine;

namespace Wayroot.Exploration
{
    /// <summary>Reveals the authored Moonlit Glade only after the first Thorn Guardian victory and registers its existing-resource nodes.</summary>
    public sealed class MoonlitGladeController : MonoBehaviour
    {
        private PrototypeGatheringController _gathering = null!;
        private PrototypeEnemy _guardian = null!;
        private GameObject _sealedPath = null!;
        private GameObject _glade = null!;
        private IReadOnlyList<GatheringNode> _nodes = null!;
        private ActionFeedbackHud? _feedback;
        private bool _nodesRegistered;
        private bool _wasOpen;

        public bool IsOpen { get; private set; }

        public void Configure(PrototypeGatheringController gathering, PrototypeEnemy guardian, GameObject sealedPath, GameObject glade, IReadOnlyList<GatheringNode> nodes)
        {
            _gathering = gathering;
            _guardian = guardian;
            _sealedPath = sealedPath;
            _glade = glade;
            _nodes = nodes;
            _guardian.Defeated += OnGuardianDefeated;
            ApplyAvailability(false);
        }

        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;

        private void Update() => ApplyAvailability(true);

        private void OnGuardianDefeated()
        {
            if (_gathering.UnlockMoonlitGlade())
            {
                ApplyAvailability(true);
            }
        }

        private void ApplyAvailability(bool announce)
        {
            bool open = MoonlitGladeRules.CanEnter(_gathering.MoonlitGladeUnlocked);
            if (open == IsOpen) return;

            IsOpen = open;
            _sealedPath.SetActive(!open);
            _glade.SetActive(open);
            if (open && !_nodesRegistered)
            {
                _gathering.RegisterNodes(_nodes);
                _nodesRegistered = true;
            }

            if (open && announce && !_wasOpen)
            {
                _feedback?.Show(MoonlitGladeRules.UnlockedStatus);
            }

            _wasOpen = open;
        }

        private void OnDestroy()
        {
            if (_guardian != null) _guardian.Defeated -= OnGuardianDefeated;
        }
    }
}

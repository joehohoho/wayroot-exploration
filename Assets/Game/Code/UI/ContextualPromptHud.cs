using Wayroot.Building;
using Wayroot.Creatures;
using Wayroot.Exploration;
using Wayroot.Gathering;
using Wayroot.Progression;
using Wayroot.Wayroot;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Shows an action instruction only while the player is near a semantic interaction target.</summary>
    public sealed class ContextualPromptHud : MonoBehaviour
    {
        private Text _text = null!;
        private Image _background = null!;
        private PrototypeGatheringController _gathering = null!;
        private PrototypeMerchantController _merchant = null!;
        private PrototypeBuildController _build = null!;
        private PrototypeWayrootController _wayroot = null!;
        private PrototypeCreatureController _creature = null!;
        private BloomwellController _bloomwell = null!;

        public void Configure(Text text, PrototypeGatheringController gathering, PrototypeMerchantController merchant, PrototypeBuildController build, PrototypeWayrootController wayroot, PrototypeCreatureController creature, BloomwellController bloomwell)
        {
            _text = text;
            _background = text.GetComponentInParent<Image>();
            _gathering = gathering;
            _merchant = merchant;
            _build = build;
            _wayroot = wayroot;
            _creature = creature;
            _bloomwell = bloomwell;
        }

        private void Update()
        {
            string prompt = _bloomwell.IsInRange ? _bloomwell.Status
                : _creature.IsInRange ? _creature.Status
                : _wayroot.IsInRange ? _wayroot.Status
                : _build.IsInRange ? _build.Status
                : _merchant.IsInRange ? _merchant.Status
                : _gathering.CurrentTarget == null ? string.Empty
                : $"Gather {_gathering.CurrentTarget.DisplayName}  {_gathering.CurrentTarget.Steps}/{_gathering.CurrentTarget.RequiredSteps}";
            bool hasPrompt = !string.IsNullOrEmpty(prompt);
            _text.text = hasPrompt ? $"HOLD GATHER  •  {prompt}" : string.Empty;
            _text.enabled = hasPrompt;
            _background.enabled = hasPrompt;
        }
    }
}

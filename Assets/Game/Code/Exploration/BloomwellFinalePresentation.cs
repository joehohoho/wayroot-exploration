using Wayroot.Creatures;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Exploration
{
    /// <summary>Applies the small persistent completion palette, motif, and companion response without changing gameplay loops.</summary>
    public sealed class BloomwellFinalePresentation : MonoBehaviour
    {
        private PrototypeGatheringController _gathering = null!;
        private PrototypeCreatureController _creature = null!;
        private GameObject _gladeBloom = null!;
        private GameObject _sunmeadowBloom = null!;
        private Light _sunmeadowLight = null!;
        private bool _applied;

        public bool IsApplied => _applied;

        public void Configure(PrototypeGatheringController gathering, PrototypeCreatureController creature, BloomwellController bloomwell, GameObject gladeBloom, GameObject sunmeadowBloom, Light sunmeadowLight)
        {
            _gathering = gathering;
            _creature = creature;
            _gladeBloom = gladeBloom;
            _sunmeadowBloom = sunmeadowBloom;
            _sunmeadowLight = sunmeadowLight;
            bloomwell.Restored += Apply;
            Apply();
        }

        public void Apply()
        {
            _applied = _gathering.BloomwellRestored;
            _gladeBloom.SetActive(_applied);
            _sunmeadowBloom.SetActive(_applied);
            _sunmeadowLight.color = _applied ? new Color(0.74f, 0.64f, 1f) : new Color(1f, 0.82f, 0.60f);
            _sunmeadowLight.intensity = _applied ? 1.45f : 1.25f;
            _creature.SetFinaleCelebration(_applied);
        }
    }
}

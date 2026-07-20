using UnityEngine;
using Wayroot.Building;
using Wayroot.UI;
using Wayroot.Audio;

namespace Wayroot.Combat
{
    public sealed class PrototypePlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 10;
        private int _health;
        private Vector3 _home;
        private Vector3 _activeShelterReturnPoint;
        private bool _hasActiveShelterReturnPoint;
        private ActionFeedbackHud? _feedback;
        private ProceduralSoundscape? _soundscape;

        public int Health => _health;
        public bool HasActiveShelterReturnPoint => _hasActiveShelterReturnPoint;

        private void Awake() { _health = maxHealth; _home = transform.position; }
        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        public void ActivateShelterReturnPoint(Vector3 returnPoint)
        {
            _activeShelterReturnPoint = returnPoint;
            _hasActiveShelterReturnPoint = true;
            _health = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _health = CombatRules.ApplyDamage(_health, damage, out bool defeated);
            if (!defeated) return;

            bool shelterReturn = ShelterRestRules.GetRespawnDestination(_hasActiveShelterReturnPoint) == RespawnDestination.ActiveShelter;
            transform.position = shelterReturn ? _activeShelterReturnPoint : _home;
            _health = maxHealth;
            _soundscape?.Play(SoundscapeCue.Defeat);
            _feedback?.Show(shelterReturn ? "RESPAWNED AT ACTIVE SHELTER: HEALTH RESTORED" : "RESPAWNED AT DEFAULT SPAWN: HEALTH RESTORED");
        }
    }
}

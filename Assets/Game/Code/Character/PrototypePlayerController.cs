using Wayroot.Input;
using Wayroot.Combat;
using Wayroot.Art;
using Wayroot.Audio;
using UnityEngine;

namespace Wayroot.Character
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PrototypePlayerController : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float rotationDegreesPerSecond = 720f;
        [SerializeField] private PrototypeInputReader inputReader = null!;
        [SerializeField] private bool isPaused;
        private CharacterController _controller = null!;
        private PrototypePlayerHealth _health = null!;
        private Vector3 _dodgeDirection;
        private float _dodgeStartedAt = float.NegativeInfinity;
        private float _nextDodgeAt;
        private ProceduralSoundscape? _soundscape;

        public Vector2 CurrentMove { get; private set; }
        public bool IsPaused => isPaused;
        public bool IsDodging => Time.time < _dodgeStartedAt + DodgeRules.DurationSeconds;
        public float DodgeCooldownRemaining => Mathf.Max(0f, _nextDodgeAt - Time.time);

        private void Awake() => _controller = GetComponent<CharacterController>();

        private void Update()
        {
            if (isPaused)
            {
                CurrentMove = Vector2.zero;
                return;
            }

            CurrentMove = MovementRules.ClampMoveMagnitude(inputReader.Move);
            if (DodgeRules.CanStart(inputReader.ConsumeDodgeRequested(), Time.time, _nextDodgeAt))
            {
                _dodgeStartedAt = Time.time;
                _nextDodgeAt = Time.time + DodgeRules.CooldownSeconds;
                _dodgeDirection = DodgeRules.ResolveDirection(CurrentMove, transform.forward);
                _health.BeginDodge(_dodgeStartedAt, DodgeRules.ImmunitySeconds);
                GetComponent<ProceduralStylizedAnimator>()?.Emphasize(DodgeRules.DurationSeconds);
                _soundscape?.Play(SoundscapeCue.Dodge);
            }

            if (IsDodging)
            {
                _controller.Move(_dodgeDirection * (DodgeRules.Distance / DodgeRules.DurationSeconds * Time.deltaTime));
                transform.rotation = Quaternion.LookRotation(_dodgeDirection, Vector3.up);
                return;
            }

            Vector3 worldMove = new Vector3(CurrentMove.x, 0f, CurrentMove.y);
            _controller.Move(worldMove * (movementSpeed * Time.deltaTime));
            if (MovementRules.TryGetFacing(CurrentMove, transform.forward, out Vector3 facing))
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, Quaternion.LookRotation(facing, Vector3.up), rotationDegreesPerSecond * Time.deltaTime);
            }
        }

        public void SetPaused(bool value) => isPaused = value;
        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        public void Configure(PrototypeInputReader reader, PrototypePlayerHealth health)
        {
            inputReader = reader;
            _health = health;
        }
    }
}

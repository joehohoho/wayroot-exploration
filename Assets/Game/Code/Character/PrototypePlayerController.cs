using Wayroot.Input;
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

        public Vector2 CurrentMove { get; private set; }
        public bool IsPaused => isPaused;

        private void Awake() => _controller = GetComponent<CharacterController>();

        private void Update()
        {
            if (isPaused)
            {
                CurrentMove = Vector2.zero;
                return;
            }

            CurrentMove = MovementRules.ClampMoveMagnitude(inputReader.Move);
            Vector3 worldMove = new Vector3(CurrentMove.x, 0f, CurrentMove.y);
            _controller.Move(worldMove * (movementSpeed * Time.deltaTime));
            if (MovementRules.TryGetFacing(CurrentMove, transform.forward, out Vector3 facing))
            {
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, Quaternion.LookRotation(facing, Vector3.up), rotationDegreesPerSecond * Time.deltaTime);
            }
        }

        public void SetPaused(bool value) => isPaused = value;

        public void Configure(PrototypeInputReader reader)
        {
            inputReader = reader;
        }
    }
}

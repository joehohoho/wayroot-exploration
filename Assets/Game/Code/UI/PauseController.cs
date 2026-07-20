using Wayroot.Camera;
using Wayroot.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Wayroot.UI
{
    public sealed class PauseController : MonoBehaviour
    {
        [SerializeField] private PrototypePlayerController player = null!;
        [SerializeField] private TopDownCameraController cameraController = null!;
        private ActionFeedbackHud? _feedback;
        public bool IsPaused { get; private set; }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Toggle();
            }
        }

        private void OnDestroy() => Time.timeScale = 1f;

        public void Configure(PrototypePlayerController prototypePlayer, TopDownCameraController prototypeCamera)
        {
            player = prototypePlayer;
            cameraController = prototypeCamera;
        }

        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;

        public void Toggle() => SetPaused(!IsPaused);

        public void SetPaused(bool value)
        {
            IsPaused = value;
            player.SetPaused(value);
            cameraController.SetPaused(value);
            Time.timeScale = value ? 0f : 1f;
            _feedback?.Show(value ? "PAUSED" : "RESUMED");
        }
    }
}

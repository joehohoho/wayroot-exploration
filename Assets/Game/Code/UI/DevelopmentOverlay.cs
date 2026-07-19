using Wayroot.Camera;
using Wayroot.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    public sealed class DevelopmentOverlay : MonoBehaviour
    {
        [SerializeField] private Text statusText = null!;
        [SerializeField] private PrototypePlayerController player = null!;
        [SerializeField] private TopDownCameraController cameraController = null!;
        [SerializeField] private PauseController pauseController = null!;

        public void Configure(Text text, PrototypePlayerController playerController, TopDownCameraController camera, PauseController pause)
        {
            statusText = text;
            player = playerController;
            cameraController = camera;
            pauseController = pause;
        }

        private void Update()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            statusText.text = $"DEV  Move {player.CurrentMove.x:0.00}, {player.CurrentMove.y:0.00}  Zoom {cameraController.CurrentZoom:0.0}  {(pauseController.IsPaused ? "PAUSED" : "RUNNING")}";
#else
            statusText.enabled = false;
#endif
        }
    }
}

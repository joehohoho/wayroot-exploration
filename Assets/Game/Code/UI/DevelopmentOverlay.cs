using Wayroot.Camera;
using Wayroot.Character;
using TMPro;
using UnityEngine;

namespace Wayroot.UI
{
    public sealed class DevelopmentOverlay : MonoBehaviour
    {
        [SerializeField] private TMP_Text text = null!;
        [SerializeField] private PrototypePlayerController player = null!;
        [SerializeField] private TopDownCameraController cameraController = null!;
        [SerializeField] private PauseController pauseController = null!;

        public void Configure(TMP_Text display, PrototypePlayerController prototypePlayer, TopDownCameraController prototypeCamera, PauseController pause)
        {
            text = display;
            player = prototypePlayer;
            cameraController = prototypeCamera;
            pauseController = pause;
        }

        private void Update()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            text.text = $"DEV  Move {player.CurrentMove.x:0.00}, {player.CurrentMove.y:0.00}  Zoom {cameraController.CurrentZoom:0.0}  {(pauseController.IsPaused ? "PAUSED" : "RUNNING")}";
#else
            text.enabled = false;
#endif
        }
    }
}

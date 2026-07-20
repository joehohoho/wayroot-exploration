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
        private float _nextRefresh;
        private float _smoothedFrameRate;
        private int _visibleRendererCount;

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
            if (Time.unscaledTime < _nextRefresh)
            {
                return;
            }

            _nextRefresh = Time.unscaledTime + 0.5f;
            _smoothedFrameRate = 1f / Mathf.Max(Time.unscaledDeltaTime, 0.001f);
            _visibleRendererCount = FindObjectsByType<Renderer>(FindObjectsSortMode.None).Length;
            statusText.text = $"DEV  {_smoothedFrameRate:0} FPS  {_visibleRendererCount} RENDERERS  ZOOM {cameraController.CurrentZoom:0.0}  {(pauseController.IsPaused ? "PAUSED" : "RUNNING")}";
#else
            statusText.enabled = false;
#endif
        }
    }
}

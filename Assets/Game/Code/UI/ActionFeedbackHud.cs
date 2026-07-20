using UnityEngine;
using UnityEngine.UI;

namespace Wayroot.UI
{
    /// <summary>Shows the latest player-facing action result briefly without obscuring touch controls.</summary>
    public sealed class ActionFeedbackHud : MonoBehaviour
    {
        [SerializeField] private Text feedbackText = null!;
        [SerializeField] private float visibleDuration = 2.8f;
        private float _hideAt;

        public void Configure(Text text)
        {
            feedbackText = text;
            feedbackText.enabled = false;
        }

        public void Show(string message)
        {
            feedbackText.text = message;
            feedbackText.enabled = true;
            _hideAt = Time.unscaledTime + visibleDuration;
        }

        private void Update()
        {
            if (feedbackText.enabled && Time.unscaledTime >= _hideAt)
            {
                feedbackText.enabled = false;
            }
        }
    }
}

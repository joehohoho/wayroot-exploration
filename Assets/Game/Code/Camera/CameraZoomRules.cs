using UnityEngine;

namespace Wayroot.Camera
{
    /// <summary>Pure zoom range rules shared by touch and desktop input.</summary>
    public static class CameraZoomRules
    {
        public static float Clamp(float zoom, float minimum, float maximum)
        {
            float lower = Mathf.Min(minimum, maximum);
            float upper = Mathf.Max(minimum, maximum);
            return Mathf.Clamp(zoom, lower, upper);
        }
    }
}

using UnityEngine;

namespace Wayroot.Core
{
    /// <summary>Minimal Phase 0 entry point; it intentionally creates no gameplay systems.</summary>
    public sealed class GameBootstrap : MonoBehaviour
    {
        private void Awake()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            Debug.Log($"{ProjectIdentity.ProductName}: Phase 0 bootstrap loaded.", this);
#endif
        }
    }
}

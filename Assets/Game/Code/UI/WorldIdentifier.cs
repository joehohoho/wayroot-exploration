using UnityEngine;

namespace Wayroot.UI
{
    /// <summary>Faces a concise built-in world label toward the active top-down camera.</summary>
    public sealed class WorldIdentifier : MonoBehaviour
    {
        [SerializeField] private Transform target = null!;
        [SerializeField] private Vector3 localOffset;
        [SerializeField] private UnityEngine.Camera sceneCamera = null!;

        public void Configure(Transform trackedTarget, Vector3 offset, UnityEngine.Camera camera)
        {
            target = trackedTarget;
            localOffset = offset;
            sceneCamera = camera;
        }

        private void LateUpdate()
        {
            if (target == null || sceneCamera == null)
            {
                return;
            }

            GetComponent<MeshRenderer>().enabled = target.gameObject.activeInHierarchy;
            if (!target.gameObject.activeInHierarchy)
            {
                return;
            }

            transform.position = target.position + localOffset;
            transform.rotation = Quaternion.LookRotation(sceneCamera.transform.position - transform.position, Vector3.up);
        }
    }
}

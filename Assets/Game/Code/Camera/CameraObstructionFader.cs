using System.Collections.Generic;
using UnityEngine;

namespace Wayroot.Camera
{
    public sealed class CameraObstructionFader : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera sourceCamera = null!;
        [SerializeField] private Transform target = null!;
        [SerializeField] private LayerMask obstructionMask = ~0;
        private readonly RaycastHit[] _hits = new RaycastHit[16];
        private readonly HashSet<FadeableObstruction> _current = new();
        private readonly HashSet<FadeableObstruction> _previous = new();

        private void LateUpdate()
        {
            Vector3 start = sourceCamera.transform.position;
            Vector3 end = target.position + (Vector3.up * 0.6f);
            Vector3 direction = end - start;
            int count = Physics.RaycastNonAlloc(start, direction.normalized, _hits, direction.magnitude, obstructionMask, QueryTriggerInteraction.Ignore);
            _current.Clear();
            for (int index = 0; index < count; index++)
            {
                if (_hits[index].collider.TryGetComponent(out FadeableObstruction obstruction))
                {
                    _current.Add(obstruction);
                    obstruction.SetObstructing(true);
                }
            }

            foreach (FadeableObstruction obstruction in _previous)
            {
                if (!_current.Contains(obstruction))
                {
                    obstruction.SetObstructing(false);
                }
            }

            _previous.Clear();
            foreach (FadeableObstruction obstruction in _current)
            {
                _previous.Add(obstruction);
            }
        }

        private void OnDisable()
        {
            foreach (FadeableObstruction obstruction in _previous)
            {
                obstruction.SetObstructing(false);
            }

            _previous.Clear();
        }

        public void Configure(UnityEngine.Camera cameraSource, Transform targetTransform)
        {
            sourceCamera = cameraSource;
            target = targetTransform;
        }
    }
}

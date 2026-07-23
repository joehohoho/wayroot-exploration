using UnityEngine;

namespace Wayroot.Guidance
{
    /// <summary>Places one quiet, non-interactive glow over the current existing Journey landmark.</summary>
    public sealed class JourneyLandmarkEmphasis : MonoBehaviour
    {
        private Transform? _target;
        private Transform[] _motifs = null!;
        private float _phase;

        public Transform? Target => _target;

        public void Configure()
        {
            _motifs = new Transform[3];
            Material material = Resources.Load<Material>("ActorSpriteUnlit");
            Color[] colors =
            {
                new(1f, 0.84f, 0.42f, 0.82f),
                new(0.74f, 0.94f, 0.54f, 0.74f),
                new(0.62f, 0.82f, 1f, 0.68f)
            };

            for (int index = 0; index < _motifs.Length; index++)
            {
                GameObject motif = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                motif.name = $"Journey Landmark Glow {index + 1}";
                motif.transform.SetParent(transform, false);
                motif.transform.localScale = Vector3.one * 0.14f;
                Destroy(motif.GetComponent<Collider>());
                Renderer renderer = motif.GetComponent<Renderer>();
                renderer.sharedMaterial = material;
                MaterialPropertyBlock properties = new();
                properties.SetColor("_BaseColor", colors[index]);
                properties.SetColor("_Color", colors[index]);
                renderer.SetPropertyBlock(properties);
                _motifs[index] = motif.transform;
            }

            gameObject.SetActive(false);
        }

        public void SetTarget(Transform? target)
        {
            _target = target;
            gameObject.SetActive(target != null);
        }

        private void Update()
        {
            if (_target == null || !_target.gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                return;
            }

            _phase += Time.unscaledDeltaTime;
            transform.position = _target.position + Vector3.up * 1.42f;
            for (int index = 0; index < _motifs.Length; index++)
            {
                float angle = _phase * 1.8f + index * Mathf.PI * 2f / _motifs.Length;
                _motifs[index].localPosition = new Vector3(Mathf.Cos(angle) * 0.34f, Mathf.Sin(_phase * 2.1f + index) * 0.07f, Mathf.Sin(angle) * 0.34f);
            }
        }
    }
}

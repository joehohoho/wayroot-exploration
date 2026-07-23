using Wayroot.Creatures;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Exploration
{
    /// <summary>Applies a bounded, visual-only restoration climax and the existing persistent peaceful finale state.</summary>
    public sealed class BloomwellFinalePresentation : MonoBehaviour
    {
        private PrototypeGatheringController _gathering = null!;
        private PrototypeCreatureController _creature = null!;
        private GameObject _gladeBloom = null!;
        private GameObject _sunmeadowBloom = null!;
        private Light _sunmeadowLight = null!;
        private Light _bloomwellLight = null!;
        private Light _finaleLight = null!;
        private GameObject _climaxArt = null!;
        private Transform[] _orbitingPetals = null!;
        private Transform[] _waterRipples = null!;
        private float _climaxStartedAt = -1f;
        private bool _applied;

        public bool IsApplied => _applied;
        public bool IsClimaxPlaying => _climaxStartedAt >= 0f;

        public void Configure(PrototypeGatheringController gathering, PrototypeCreatureController creature, BloomwellController bloomwell, Transform landmark, GameObject gladeBloom, GameObject sunmeadowBloom, Light sunmeadowLight, Light bloomwellLight, Light finaleLight)
        {
            _gathering = gathering;
            _creature = creature;
            _gladeBloom = gladeBloom;
            _sunmeadowBloom = sunmeadowBloom;
            _sunmeadowLight = sunmeadowLight;
            _bloomwellLight = bloomwellLight;
            _finaleLight = finaleLight;
            CreateClimaxArt(landmark.position);
            bloomwell.Restored += BeginRestorationPresentation;
            Apply();
        }

        public void Apply()
        {
            _applied = _gathering.BloomwellRestored;
            _gladeBloom.SetActive(_applied);
            _sunmeadowBloom.SetActive(_applied);
            _climaxArt.SetActive(_applied);
            _sunmeadowLight.color = _applied ? new Color(0.74f, 0.64f, 1f) : new Color(1f, 0.82f, 0.60f);
            _sunmeadowLight.intensity = _applied ? 1.45f : 1.25f;
            _bloomwellLight.color = _applied ? new Color(0.50f, 0.92f, 1f) : new Color(0.62f, 0.48f, 1f);
            _bloomwellLight.intensity = _applied ? 1.85f : 1.45f;
            _finaleLight.intensity = _applied ? 2.45f : 2.25f;
            _creature.SetFinaleCelebration(_applied);
        }

        private void BeginRestorationPresentation()
        {
            Apply();
            _climaxStartedAt = Time.time;
            _climaxArt.SetActive(true);
        }

        private void Update()
        {
            if (_climaxStartedAt < 0f) return;
            float elapsed = Time.time - _climaxStartedAt;
            if (!BloomwellFinalePresentationRules.IsClimaxActive(elapsed))
            {
                _climaxStartedAt = -1f;
                ResetClimaxArt();
                return;
            }

            for (int index = 0; index < _orbitingPetals.Length; index++)
            {
                _orbitingPetals[index].position = BloomwellFinalePresentationRules.GetOrbitPosition(_climaxArt.transform.position, index, elapsed);
            }

            float pulse = 1f + elapsed / BloomwellFinalePresentationRules.ClimaxDurationSeconds * 0.72f;
            for (int index = 0; index < _waterRipples.Length; index++)
            {
                _waterRipples[index].localScale = new Vector3(pulse + index * 0.24f, 0.025f, pulse + index * 0.24f);
            }

            _bloomwellLight.intensity = 1.85f + 0.7f * Mathf.Sin(elapsed * Mathf.PI / BloomwellFinalePresentationRules.ClimaxDurationSeconds);
            _finaleLight.intensity = 2.45f + 0.8f * Mathf.Sin(elapsed * Mathf.PI / BloomwellFinalePresentationRules.ClimaxDurationSeconds);
        }

        private void CreateClimaxArt(Vector3 landmarkPosition)
        {
            _climaxArt = new GameObject("Bloomwell Restoration Climax Art");
            // Visual art stays outside the landmark/gameplay hierarchy: future root scaling cannot
            // inflate it or disturb the Bloomwell interaction, collider, or label geometry.
            _climaxArt.transform.position = landmarkPosition;
            _orbitingPetals = new Transform[6];
            _waterRipples = new Transform[2];
            Color[] colors = { new(1f, 0.72f, 0.96f), new(0.64f, 0.90f, 1f), new(0.58f, 1f, 0.84f) };
            for (int index = 0; index < _orbitingPetals.Length; index++)
            {
                GameObject petal = CreatePiece($"Bloomwell Finale Orbit Petal {index + 1}", PrimitiveType.Sphere, _climaxArt.transform, new Vector3(0f, 1.02f, 0f), new Vector3(0.17f, 0.08f, 0.30f), colors[index % colors.Length]);
                _orbitingPetals[index] = petal.transform;
            }

            for (int index = 0; index < _waterRipples.Length; index++)
            {
                GameObject ripple = CreatePiece($"Bloomwell Restored Water Ripple {index + 1}", PrimitiveType.Cylinder, _climaxArt.transform, new Vector3(0f, 0.12f + index * 0.015f, 0f), new Vector3(0.72f + index * 0.24f, 0.025f, 0.72f + index * 0.24f), new Color(0.52f, 0.94f, 0.92f, 0.52f));
                _waterRipples[index] = ripple.transform;
            }
        }

        private void ResetClimaxArt()
        {
            for (int index = 0; index < _orbitingPetals.Length; index++)
            {
                _orbitingPetals[index].localPosition = new Vector3(0f, 1.02f, 0f);
            }

            for (int index = 0; index < _waterRipples.Length; index++)
            {
                _waterRipples[index].localScale = new Vector3(0.72f + index * 0.24f, 0.025f, 0.72f + index * 0.24f);
            }

            _bloomwellLight.intensity = 1.85f;
            _finaleLight.intensity = 2.45f;
        }

        private static GameObject CreatePiece(string name, PrimitiveType type, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject piece = GameObject.CreatePrimitive(type);
            piece.name = name;
            piece.transform.SetParent(parent, false);
            piece.transform.localPosition = localPosition;
            piece.transform.localScale = localScale;
            Destroy(piece.GetComponent<Collider>());
            Renderer renderer = piece.GetComponent<Renderer>();
            renderer.sharedMaterial = Resources.Load<Material>("ActorSpriteUnlit");
            MaterialPropertyBlock properties = new();
            properties.SetColor("_BaseColor", color);
            properties.SetColor("_Color", color);
            renderer.SetPropertyBlock(properties);
            return piece;
        }
    }
}

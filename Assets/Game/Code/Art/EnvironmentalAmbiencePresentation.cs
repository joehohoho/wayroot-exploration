using System.Collections.Generic;
using UnityEngine;
using Wayroot.UI;

namespace Wayroot.Art
{
    /// <summary>Restrained visual-only motion for the existing regions; no clock, weather, or gameplay objects are changed.</summary>
    public sealed class EnvironmentalAmbiencePresentation : MonoBehaviour
    {
        private readonly List<Transform> _waterRipples = new();
        private readonly List<Transform> _foliage = new();
        private readonly List<Transform> _motes = new();
        private readonly List<Transform> _landmarkRoots = new();
        private readonly List<Transform> _landmarkAnchors = new();
        private readonly List<Vector3> _baseScales = new();
        private readonly List<float> _phases = new();
        private Transform _shelter = null!;
        private Transform _grove = null!;
        private Transform _glade = null!;
        private Transform _bloomwell = null!;
        private AccessibilityPreferences _preferences = null!;
        private bool _optionalEffectsEnabled = true;

        public int VisualCount => _waterRipples.Count + _foliage.Count + _motes.Count;
        public bool OptionalEffectsEnabled => _optionalEffectsEnabled;

        public void Configure(Transform shelter, Transform grove, Transform glade, Transform bloomwell, AccessibilityPreferences preferences)
        {
            _shelter = shelter;
            _grove = grove;
            _glade = glade;
            _bloomwell = bloomwell;
            _preferences = preferences;
            transform.localScale = Vector3.one;
            CreateSunmeadowAmbience();
            CreateLandmarkAmbience();
            SetOptionalEffectsEnabled(true);
        }

        /// <summary>Disables decorative ripple, foliage, and mote renderers only; landmark/gameplay roots remain untouched.</summary>
        public void SetOptionalEffectsEnabled(bool enabled)
        {
            _optionalEffectsEnabled = enabled;
            SetVisualsActive(_waterRipples, enabled);
            SetVisualsActive(_foliage, enabled);
            SetVisualsActive(_motes, enabled);
        }

        private void Update()
        {
            float time = Time.time;
            for (int index = 0; index < _landmarkRoots.Count; index++)
            {
                Transform anchor = _landmarkAnchors[index];
                Transform root = _landmarkRoots[index];
                root.position = anchor.position + new Vector3(0f, 0.1f, 0f);
                root.gameObject.SetActive(anchor.gameObject.activeInHierarchy);
            }

            for (int index = 0; index < _waterRipples.Count; index++)
            {
                float pulse = 1f + AccessibilityRules.GetMotionAmplitude(0.14f, _preferences.ReducedMotion) * Mathf.Sin(time * 1.8f + _phases[index]);
                _waterRipples[index].localScale = Vector3.Scale(_baseScales[index], new Vector3(pulse, 1f, pulse));
            }

            int foliageOffset = _waterRipples.Count;
            for (int index = 0; index < _foliage.Count; index++)
            {
                _foliage[index].localRotation = Quaternion.Euler(0f, 0f, AccessibilityRules.GetMotionAmplitude(6f, _preferences.ReducedMotion) * Mathf.Sin(time * 1.25f + _phases[foliageOffset + index]));
            }

            int moteOffset = foliageOffset + _foliage.Count;
            for (int index = 0; index < _motes.Count; index++)
            {
                Transform mote = _motes[index];
                Vector3 basePosition = mote.parent!.position;
                float phase = _phases[moteOffset + index];
                float amplitude = AccessibilityRules.GetMotionAmplitude(1f, _preferences.ReducedMotion);
                mote.position = basePosition + new Vector3(Mathf.Sin(time * 0.75f + phase) * 0.18f * amplitude, 0.16f + Mathf.Sin(time * 1.5f + phase) * 0.08f * amplitude, Mathf.Cos(time * 0.65f + phase) * 0.12f * amplitude);
                mote.gameObject.SetActive(_optionalEffectsEnabled && mote.parent!.gameObject.activeInHierarchy);
            }
        }

        private static void SetVisualsActive(List<Transform> visuals, bool active)
        {
            for (int index = 0; index < visuals.Count; index++)
            {
                visuals[index].gameObject.SetActive(active);
            }
        }

        private void CreateSunmeadowAmbience()
        {
            Transform sunmeadow = new GameObject("Phase 31 Sunmeadow Ambient Motion").transform;
            sunmeadow.SetParent(transform, false);
            Transform creek = GameObject.Find("Sunmeadow Creek")?.transform;
            if (creek != null)
            {
                CreateWaterRipple(sunmeadow, creek.position + new Vector3(-0.38f, 0.065f, -2.4f), 0.34f, 0.4f);
                CreateWaterRipple(sunmeadow, creek.position + new Vector3(0.22f, 0.065f, 1.2f), 0.28f, 1.6f);
            }

            CreateFoliage(sunmeadow, new Vector3(-3.7f, 0.34f, 3.9f), 0.36f, 0.4f);
            CreateFoliage(sunmeadow, new Vector3(2.8f, 0.34f, -5.3f), 0.30f, 1.1f);
            CreateMote(sunmeadow, new Vector3(-2.8f, 0f, 4.0f), new Color(1f, 0.82f, 0.38f), 0.12f, 0.8f);
        }

        private void CreateLandmarkAmbience()
        {
            CreateLandmarkMote("Phase 31 Shelter Hearth Ambience", _shelter, new Color(1f, 0.55f, 0.22f), 0.11f, 0.5f);
            CreateLandmarkMote("Phase 31 Grove Fern Ambience", _grove, new Color(0.68f, 0.94f, 0.30f), 0.10f, 1.3f);
            CreateLandmarkMote("Phase 31 Glade Moon Ambience", _glade, new Color(0.62f, 0.72f, 1f), 0.10f, 2.1f);
            CreateLandmarkMote("Phase 31 Bloomwell Glow Ambience", _bloomwell, new Color(0.40f, 0.96f, 0.86f), 0.12f, 2.8f);
        }

        private void CreateLandmarkMote(string name, Transform anchor, Color color, float size, float phase)
        {
            Transform root = new GameObject(name).transform;
            root.SetParent(transform, false);
            root.position = anchor.position + new Vector3(0f, 0.1f, 0f);
            _landmarkRoots.Add(root);
            _landmarkAnchors.Add(anchor);
            CreateMote(root, Vector3.zero, color, size, phase);
        }

        private void CreateWaterRipple(Transform parent, Vector3 position, float radius, float phase)
        {
            Transform ripple = CreateVisual("Phase 31 Creek Ripple", PrimitiveType.Cylinder, parent, position, new Vector3(radius, 0.012f, radius), new Color(0.48f, 0.92f, 0.94f));
            _waterRipples.Add(ripple);
            _baseScales.Add(ripple.localScale);
            _phases.Add(phase);
        }

        private void CreateFoliage(Transform parent, Vector3 position, float size, float phase)
        {
            Transform leaf = CreateVisual("Phase 31 Meadow Sway Leaf", PrimitiveType.Sphere, parent, position, new Vector3(size, size * 0.18f, size * 0.78f), new Color(0.58f, 0.82f, 0.28f));
            _foliage.Add(leaf);
            _baseScales.Add(leaf.localScale);
            _phases.Add(phase);
        }

        private void CreateMote(Transform parent, Vector3 position, Color color, float size, float phase)
        {
            Transform mote = CreateVisual("Phase 31 Ambient Mote", PrimitiveType.Sphere, parent, parent.position + position, Vector3.one * size, color);
            _motes.Add(mote);
            _baseScales.Add(mote.localScale);
            _phases.Add(phase);
        }

        private static Transform CreateVisual(string name, PrimitiveType primitive, Transform parent, Vector3 position, Vector3 scale, Color color)
        {
            GameObject visual = GameObject.CreatePrimitive(primitive);
            visual.name = name;
            visual.transform.SetParent(parent, true);
            visual.transform.position = position;
            visual.transform.localScale = scale;
            Destroy(visual.GetComponent<Collider>());
            Renderer renderer = visual.GetComponent<Renderer>();
            Material? material = Resources.Load<Material>("ActorSpriteUnlit");
            if (material != null) renderer.sharedMaterial = material;
            MaterialPropertyBlock properties = new();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            renderer.SetPropertyBlock(properties);
            return visual.transform;
        }
    }
}

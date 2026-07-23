using System.Collections.Generic;
using UnityEngine;
using Wayroot.UI;

namespace Wayroot.Art
{
    /// <summary>Visual-only, original runtime-composed finish dressing for the established public-playtest route.</summary>
    public sealed class PhaseThirtySixVisualFinish : MonoBehaviour
    {
        private readonly List<Transform> _motionDetails = new();
        private readonly List<Renderer> _glowDetails = new();
        private readonly List<Color> _glowColors = new();
        private AccessibilityPreferences _accessibilityPreferences = null!;

        public int VisualCount { get; private set; }
        public bool HasOnlyVisualColliders => GetComponentsInChildren<Collider>(true).Length == 0;

        public void Configure(AccessibilityPreferences accessibilityPreferences)
        {
            _accessibilityPreferences = accessibilityPreferences;
            name = "Phase 36 Public Playtest Visual Finish";
            CreateSunmeadowFinish();
            CreateShelterFinish();
            CreateCombatAndGroveFinish();
            CreateGladeAndBloomwellFinish();
        }

        private void Update()
        {
            float amplitude = AccessibilityRules.GetMotionAmplitude(0.045f, _accessibilityPreferences.ReducedMotion);
            for (int index = 0; index < _motionDetails.Count; index++)
            {
                Transform detail = _motionDetails[index];
                Vector3 position = detail.localPosition;
                position.y = detail.GetComponent<PhaseThirtySixRestHeight>().Height + Mathf.Sin(Time.time * 1.2f + index) * amplitude;
                detail.localPosition = position;
            }

            float pulse = _accessibilityPreferences.ReducedFlash ? 0.94f : 0.88f + Mathf.Sin(Time.time * 1.7f) * 0.08f;
            for (int index = 0; index < _glowDetails.Count; index++)
            {
                Renderer renderer = _glowDetails[index];
                MaterialPropertyBlock block = new();
                renderer.GetPropertyBlock(block);
                Color color = _glowColors[index];
                block.SetColor("_BaseColor", color * pulse);
                block.SetColor("_Color", color * pulse);
                renderer.SetPropertyBlock(block);
            }
        }

        private void CreateSunmeadowFinish()
        {
            Transform root = CreateRoot("Sunmeadow Finish Dressing", new Vector3(0f, 0f, 0f));
            Color gold = PhaseThirtySixVisualFinishRules.GetAccent(ExplorationRegion.Sunmeadow);
            CreateDisc("Meadow Soft Path Inlay", root, new Vector3(-1.6f, 0.045f, -1.7f), new Vector3(1.38f, 0.028f, 2.2f), new Color(0.82f, 0.57f, 0.30f));
            CreateDisc("Meadow Merchant Welcome Inlay", root, new Vector3(2f, 0.04f, -3f), new Vector3(1.52f, 0.025f, 1.52f), new Color(0.58f, 0.25f, 0.12f));
            CreateCluster("Sunmeadow Gold Petal Cluster", root, new Vector3(-3.8f, 0f, 2.4f), gold);
            CreateCluster("Sunmeadow Gold Petal Cluster East", root, new Vector3(3.8f, 0f, 4.1f), gold);
            CreateLantern("Sunmeadow Merchant Warm Lantern", root, new Vector3(3.0f, 0.66f, -2.5f), gold);
            CreateLantern("Sunmeadow Creek Warm Lantern", root, new Vector3(5.35f, 0.54f, 1.9f), gold);
        }

        private void CreateShelterFinish()
        {
            Transform root = CreateRoot("Shelter Finish Dressing", new Vector3(-6f, 0f, -5f));
            Color gold = new(1f, 0.68f, 0.30f);
            CreateDisc("Shelter Hearthstone Rug", root, new Vector3(0f, 0.038f, -0.32f), new Vector3(1.45f, 0.024f, 1.15f), new Color(0.42f, 0.21f, 0.12f));
            CreateLantern("Shelter Porch Lantern Left", root, new Vector3(-1.28f, 0.64f, -0.82f), gold);
            CreateLantern("Shelter Porch Lantern Right", root, new Vector3(1.28f, 0.64f, -0.82f), gold);
            CreateCluster("Shelter Cozy Flower Cluster", root, new Vector3(-1.52f, 0f, 0.64f), new Color(0.94f, 0.42f, 0.49f));
        }

        private void CreateCombatAndGroveFinish()
        {
            Transform root = CreateRoot("Grove Finish Dressing", new Vector3(-6.7f, 0f, 1.2f));
            Color grove = PhaseThirtySixVisualFinishRules.GetAccent(ExplorationRegion.RestoredGrove);
            CreateDisc("Grove Combat Clearing Inlay", root, new Vector3(0f, 0.036f, 0f), new Vector3(2.18f, 0.024f, 1.78f), new Color(0.16f, 0.33f, 0.18f));
            CreateLantern("Grove Threshold Firefly Left", root, new Vector3(-2.18f, 0.74f, 0.58f), grove);
            CreateLantern("Grove Threshold Firefly Right", root, new Vector3(2.18f, 0.74f, 0.58f), grove);
            CreateCluster("Grove Bright Fern Cluster", root, new Vector3(-2.0f, 0f, -1.12f), grove);
            CreateCluster("Grove Bright Fern Cluster East", root, new Vector3(2.0f, 0f, -1.12f), grove);
        }

        private void CreateGladeAndBloomwellFinish()
        {
            Transform glade = CreateRoot("Moonlit Glade Finish Dressing", new Vector3(-8.15f, 0f, 5.9f));
            Color violet = PhaseThirtySixVisualFinishRules.GetAccent(ExplorationRegion.MoonlitGlade);
            CreateDisc("Moonlit Glade Soft Pool", glade, new Vector3(0f, 0.032f, 0f), new Vector3(2.22f, 0.022f, 1.96f), new Color(0.18f, 0.19f, 0.45f));
            CreateLantern("Moonlit Glade Mote Left", glade, new Vector3(-1.72f, 0.82f, 0.62f), violet);
            CreateLantern("Moonlit Glade Mote Right", glade, new Vector3(1.72f, 0.82f, 0.32f), violet);
            Transform bloomwell = CreateRoot("Bloomwell Finish Dressing", new Vector3(-8.15f, 0f, 6.15f));
            Color aqua = PhaseThirtySixVisualFinishRules.GetAccent(ExplorationRegion.Bloomwell);
            CreateDisc("Bloomwell Quiet Halo", bloomwell, new Vector3(0f, 0.03f, 0f), new Vector3(1.58f, 0.018f, 1.58f), new Color(0.17f, 0.36f, 0.42f));
            CreateLantern("Bloomwell Aqua Mote Left", bloomwell, new Vector3(-1.15f, 0.92f, 0.42f), aqua);
            CreateLantern("Bloomwell Aqua Mote Right", bloomwell, new Vector3(1.15f, 1.08f, -0.22f), aqua);
            CreateCluster("Bloomwell Luminous Petal Cluster", bloomwell, new Vector3(-1.38f, 0f, 0.82f), aqua);
        }

        private Transform CreateRoot(string rootName, Vector3 position)
        {
            Transform root = new GameObject(rootName).transform;
            root.SetParent(transform, false);
            root.position = position;
            return root;
        }

        private void CreateCluster(string detailName, Transform parent, Vector3 localPosition, Color color)
        {
            for (int index = 0; index < 3; index++)
            {
                float angle = index * 2.1f;
                Vector3 offset = new(Mathf.Cos(angle) * 0.32f, 0.16f, Mathf.Sin(angle) * 0.32f);
                CreateVisual(detailName, PrimitiveType.Sphere, parent, localPosition + offset, new Vector3(0.20f, 0.11f, 0.20f), color, false);
            }
        }

        private void CreateLantern(string detailName, Transform parent, Vector3 localPosition, Color color)
        {
            Transform lantern = CreateVisual(detailName, PrimitiveType.Sphere, parent, localPosition, new Vector3(0.16f, 0.24f, 0.16f), color, true);
            PhaseThirtySixRestHeight restHeight = lantern.gameObject.AddComponent<PhaseThirtySixRestHeight>();
            restHeight.Height = localPosition.y;
            _motionDetails.Add(lantern);
        }

        private void CreateDisc(string detailName, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            CreateVisual(detailName, PrimitiveType.Cylinder, parent, localPosition, localScale, color, false);
        }

        private Transform CreateVisual(string detailName, PrimitiveType primitive, Transform parent, Vector3 localPosition, Vector3 localScale, Color color, bool glow)
        {
            if (!PhaseThirtySixVisualFinishRules.IsSafeDecoration(localScale)) throw new System.ArgumentOutOfRangeException(nameof(localScale));
            GameObject detail = GameObject.CreatePrimitive(primitive);
            detail.name = detailName;
            detail.transform.SetParent(parent, false);
            detail.transform.localPosition = localPosition;
            detail.transform.localScale = localScale;
            Destroy(detail.GetComponent<Collider>());
            Renderer renderer = detail.GetComponent<Renderer>();
            Material material = Resources.Load<Material>(glow ? "Phase14/Coral" : "Phase14/Moss");
            if (material != null) renderer.sharedMaterial = material;
            MaterialPropertyBlock block = new();
            renderer.GetPropertyBlock(block);
            block.SetColor("_BaseColor", color);
            block.SetColor("_Color", color);
            renderer.SetPropertyBlock(block);
            if (glow)
            {
                _glowDetails.Add(renderer);
                _glowColors.Add(color);
            }
            VisualCount++;
            return detail.transform;
        }
    }

    public sealed class PhaseThirtySixRestHeight : MonoBehaviour
    {
        public float Height { get; set; }
    }
}

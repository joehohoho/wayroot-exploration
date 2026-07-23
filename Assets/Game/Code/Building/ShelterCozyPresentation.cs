using UnityEngine;

namespace Wayroot.Building
{
    /// <summary>Runtime-composed visual-only shelter silhouette, homestead dressing, and rest warmth cue.</summary>
    public sealed class ShelterCozyPresentation : MonoBehaviour
    {
        private GameObject _blueprint = null!;
        private GameObject _restPulse = null!;
        private float _restPulseStartedAt = -1f;

        public bool IsRestPulseShowing => _restPulse.activeSelf;

        public void Configure(Transform plot, GameObject shelter)
        {
            _blueprint = new GameObject("Unbuilt Shelter Blueprint Frame");
            // The build plot is deliberately scaled to form a broad interaction pad. Parenting
            // art to it multiplies every local piece by that scale, turning the roof-outline
            // into the giant teal square seen in the player. Keep the visual at the plot's
            // world position but outside the scaled gameplay-root hierarchy.
            _blueprint.transform.position = plot.position;
            // An unbuilt plan must read as a ground footprint, not a hovering wall/roof the player
            // can walk through. Four low outline beams keep the interaction area clear while
            // showing the intended shelter dimensions.
            Color blueprintColor = new(0.30f, 0.62f, 0.82f);
            CreatePiece("Unbuilt Shelter Footprint North", PrimitiveType.Cube, _blueprint.transform, new Vector3(0f, 0.035f, 1.02f), new Vector3(2.8f, 0.035f, 0.07f), blueprintColor);
            CreatePiece("Unbuilt Shelter Footprint South", PrimitiveType.Cube, _blueprint.transform, new Vector3(0f, 0.035f, -1.02f), new Vector3(2.8f, 0.035f, 0.07f), blueprintColor);
            CreatePiece("Unbuilt Shelter Footprint West", PrimitiveType.Cube, _blueprint.transform, new Vector3(-1.36f, 0.035f, 0f), new Vector3(0.07f, 0.035f, 2.1f), blueprintColor);
            CreatePiece("Unbuilt Shelter Footprint East", PrimitiveType.Cube, _blueprint.transform, new Vector3(1.36f, 0.035f, 0f), new Vector3(0.07f, 0.035f, 2.1f), blueprintColor);
            CreatePiece("Unbuilt Shelter Post Left", PrimitiveType.Cube, _blueprint.transform, new Vector3(-1.25f, 0.72f, -0.85f), new Vector3(0.10f, 1.25f, 0.10f), new Color(0.42f, 0.78f, 0.96f));
            CreatePiece("Unbuilt Shelter Post Right", PrimitiveType.Cube, _blueprint.transform, new Vector3(1.25f, 0.72f, -0.85f), new Vector3(0.10f, 1.25f, 0.10f), new Color(0.42f, 0.78f, 0.96f));
            GameObject markers = new("Unbuilt Shelter Material Markers");
            markers.transform.SetParent(_blueprint.transform, false);
            CreatePiece("Blueprint Timber Marker", PrimitiveType.Cylinder, markers.transform, new Vector3(-0.58f, 0.28f, -1.25f), new Vector3(0.17f, 0.15f, 0.17f), new Color(0.58f, 0.34f, 0.16f));
            CreatePiece("Blueprint Stone Marker", PrimitiveType.Sphere, markers.transform, new Vector3(0.58f, 0.25f, -1.25f), new Vector3(0.30f, 0.22f, 0.30f), new Color(0.48f, 0.52f, 0.58f));

            CreatePiece("Shelter Foundation", PrimitiveType.Cube, shelter.transform, new Vector3(0f, 0.19f, 0f), new Vector3(3.25f, 0.18f, 2.45f), new Color(0.32f, 0.24f, 0.17f));
            CreatePiece("Shelter Roof Peak", PrimitiveType.Cube, shelter.transform, new Vector3(0f, 2.72f, 0.10f), new Vector3(2.95f, 0.18f, 2.32f), new Color(0.36f, 0.12f, 0.10f));
            CreatePiece("Shelter Door", PrimitiveType.Cube, shelter.transform, new Vector3(0f, 0.94f, -0.98f), new Vector3(0.72f, 1.45f, 0.10f), new Color(0.20f, 0.10f, 0.05f));
            CreatePiece("Shelter Warm Window", PrimitiveType.Cube, shelter.transform, new Vector3(0.82f, 1.40f, -0.99f), new Vector3(0.42f, 0.44f, 0.08f), ShelterCozyArtRules.LanternColor);
            CreatePiece("Shelter Lantern Hook", PrimitiveType.Cube, shelter.transform, new Vector3(-1.25f, 2.05f, -0.85f), new Vector3(0.08f, 0.46f, 0.08f), new Color(0.22f, 0.12f, 0.05f));
            CreatePiece("Shelter Lantern Glow", PrimitiveType.Sphere, shelter.transform, new Vector3(-1.25f, 1.78f, -0.85f), new Vector3(0.34f, 0.48f, 0.34f), ShelterCozyArtRules.LanternColor);
            CreatePiece("Shelter Hearth Glow", PrimitiveType.Sphere, shelter.transform, new Vector3(-0.42f, 0.55f, -0.94f), new Vector3(0.36f, 0.36f, 0.22f), ShelterCozyArtRules.HearthColor);

            GameObject garden = new("Shelter Garden Patch");
            garden.transform.SetParent(shelter.transform, false);
            CreatePiece("Shelter Garden Soil", PrimitiveType.Cube, garden.transform, new Vector3(-1.85f, 0.14f, -0.55f), new Vector3(0.68f, 0.08f, 1.25f), new Color(0.25f, 0.16f, 0.08f));
            CreatePiece("Shelter Garden Bloom A", PrimitiveType.Sphere, garden.transform, new Vector3(-1.86f, 0.35f, -0.86f), new Vector3(0.20f, 0.35f, 0.20f), new Color(1f, 0.58f, 0.68f));
            CreatePiece("Shelter Garden Bloom B", PrimitiveType.Sphere, garden.transform, new Vector3(-1.86f, 0.32f, -0.25f), new Vector3(0.18f, 0.30f, 0.18f), new Color(1f, 0.82f, 0.30f));

            GameObject path = new("Shelter Welcome Path");
            path.transform.SetParent(shelter.transform, false);
            for (int index = 0; index < 4; index++)
            {
                CreatePiece($"Shelter Path Stone {index + 1}", PrimitiveType.Cylinder, path.transform, new Vector3(0f, 0.08f, -1.55f - index * 0.47f), new Vector3(0.45f, 0.05f, 0.34f), new Color(0.54f, 0.47f, 0.34f));
            }

            _restPulse = CreatePiece("Shelter Rest Warmth Pulse", PrimitiveType.Cylinder, shelter.transform, new Vector3(0f, 0.08f, 0f), new Vector3(3.5f, 0.025f, 3.5f), new Color(1f, 0.62f, 0.22f, 0.72f));
            _restPulse.SetActive(false);
        }

        public void ApplyState(bool built)
        {
            _blueprint.SetActive(!built);
            if (!built) _restPulse.SetActive(false);
        }

        public void PlayRestFeedback()
        {
            _restPulseStartedAt = Time.time;
            _restPulse.SetActive(true);
        }

        private void Update()
        {
            if (_restPulseStartedAt < 0f) return;
            float elapsed = Time.time - _restPulseStartedAt;
            _restPulse.SetActive(ShelterCozyArtRules.IsRestPulseActive(elapsed));
            if (!ShelterCozyArtRules.IsRestPulseActive(elapsed)) _restPulseStartedAt = -1f;
        }

        private static GameObject CreatePiece(string name, PrimitiveType type, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject piece = GameObject.CreatePrimitive(type);
            piece.name = name;
            piece.transform.SetParent(parent, false);
            piece.transform.localPosition = localPosition;
            piece.transform.localScale = localScale;
            Object.Destroy(piece.GetComponent<Collider>());
            Renderer renderer = piece.GetComponent<Renderer>();
            // Use the source-controlled URP unlit asset already retained for player builds;
            // primitive fallback materials can be stripped in a Windows player.
            Material? shelterMaterial = Resources.Load<Material>("ActorSpriteUnlit");
            if (shelterMaterial != null) renderer.sharedMaterial = shelterMaterial;
            // Do not instantiate/mutate the primitive's fallback material. That path can retain a
            // stripped Built-in shader in a URP player and renders Unity's magenta error surface.
            // The established world-composition path colors the verified URP primitive material
            // with a property block instead.
            MaterialPropertyBlock properties = new();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            properties.SetColor("_Color", color);
            renderer.SetPropertyBlock(properties);
            return piece;
        }
    }
}

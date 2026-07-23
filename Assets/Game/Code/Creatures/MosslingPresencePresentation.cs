using Wayroot.Character;
using UnityEngine;

namespace Wayroot.Creatures
{
    /// <summary>Visual-only Mossling accents driven by the existing guide selection; gameplay root and follow logic stay untouched.</summary>
    public sealed class MosslingPresencePresentation : MonoBehaviour
    {
        private PrototypeCreatureController _creature = null!;
        private MosslingResourceGuide _guide = null!;
        private PrototypePlayerController _player = null!;
        private Transform _visualRoot = null!;
        private Transform _guidePointer = null!;
        private Transform _arrivalBloom = null!;
        private Transform _renewalHalo = null!;
        private Transform _followLeaf = null!;
        private Transform _idleMote = null!;
        private Light _guideLight = null!;

        public MosslingPresenceState CurrentState { get; private set; }
        public Transform VisualRoot => _visualRoot;

        public void Configure(PrototypeCreatureController creature, MosslingResourceGuide guide, PrototypePlayerController player)
        {
            _creature = creature;
            _guide = guide;
            _player = player;
            CreateVisuals();
        }

        private void LateUpdate()
        {
            if (_visualRoot == null || _player == null) return;
            CurrentState = MosslingPresenceRules.Select(_creature.IsBefriended, _guide.Selection, _player.transform.position, transform.position);
            float time = Time.unscaledTime;
            _idleMote.gameObject.SetActive(CurrentState == MosslingPresenceState.Idle);
            _followLeaf.gameObject.SetActive(CurrentState == MosslingPresenceState.Follow);
            _guidePointer.gameObject.SetActive(CurrentState == MosslingPresenceState.Guide);
            _arrivalBloom.gameObject.SetActive(CurrentState == MosslingPresenceState.Arrival);
            _renewalHalo.gameObject.SetActive(CurrentState == MosslingPresenceState.Renewal);
            _guideLight.enabled = CurrentState == MosslingPresenceState.Guide || CurrentState == MosslingPresenceState.Arrival;

            _idleMote.localPosition = new Vector3(-0.32f, 1.13f + Mathf.Sin(time * 2.2f) * 0.07f, 0f);
            _followLeaf.localPosition = new Vector3(0f, 0.92f + Mathf.Sin(time * 4f) * 0.04f, -0.38f);
            _guidePointer.localRotation = Quaternion.Euler(0f, time * 85f, 24f + Mathf.Sin(time * 3f) * 8f);
            _arrivalBloom.localScale = Vector3.one * (0.72f + Mathf.Sin(time * 5f) * 0.08f);
            _renewalHalo.localRotation = Quaternion.Euler(0f, time * -45f, 0f);
        }

        private void CreateVisuals()
        {
            GameObject root = new("Mossling Presence Visual Root");
            _visualRoot = root.transform;
            _visualRoot.SetParent(transform, false);
            Vector3 inheritedScale = transform.lossyScale;
            _visualRoot.localScale = new Vector3(1f / inheritedScale.x, 1f / inheritedScale.y, 1f / inheritedScale.z);
            _visualRoot.localPosition = Vector3.zero;

            _idleMote = CreatePiece("Mossling Idle Mote", PrimitiveType.Sphere, new Vector3(-0.32f, 1.13f, 0f), new Vector3(0.11f, 0.11f, 0.11f), new Color(1f, 0.82f, 0.30f));
            _followLeaf = CreatePiece("Mossling Follow Leaf", PrimitiveType.Sphere, new Vector3(0f, 0.92f, -0.38f), new Vector3(0.52f, 0.07f, 0.24f), new Color(0.32f, 0.88f, 0.48f));
            _guidePointer = CreatePiece("Mossling Guide Pointer", PrimitiveType.Cylinder, new Vector3(0f, 1.08f, 0f), new Vector3(0.12f, 0.38f, 0.12f), new Color(0.72f, 1f, 0.58f));
            _arrivalBloom = CreatePiece("Mossling Arrival Bloom", PrimitiveType.Sphere, new Vector3(0f, 1.12f, 0f), new Vector3(0.72f, 0.06f, 0.72f), new Color(1f, 0.78f, 0.35f));
            _renewalHalo = CreatePiece("Mossling Renewal Halo", PrimitiveType.Cylinder, new Vector3(0f, 0.12f, 0f), new Vector3(0.78f, 0.018f, 0.78f), new Color(0.42f, 0.76f, 1f));
            _guideLight = root.AddComponent<Light>();
            _guideLight.type = LightType.Point;
            _guideLight.color = new Color(0.72f, 1f, 0.58f);
            _guideLight.intensity = 0.65f;
            _guideLight.range = 2.1f;
            _guideLight.enabled = false;
            _idleMote.gameObject.SetActive(false);
            _followLeaf.gameObject.SetActive(false);
            _guidePointer.gameObject.SetActive(false);
            _arrivalBloom.gameObject.SetActive(false);
            _renewalHalo.gameObject.SetActive(false);
        }

        private Transform CreatePiece(string name, PrimitiveType primitive, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject piece = GameObject.CreatePrimitive(primitive);
            piece.name = name;
            piece.transform.SetParent(_visualRoot, false);
            piece.transform.localPosition = localPosition;
            piece.transform.localScale = localScale;
            Destroy(piece.GetComponent<Collider>());
            Renderer renderer = piece.GetComponent<Renderer>();
            Material material = Resources.Load<Material>("ActorSpriteUnlit");
            if (material != null) renderer.sharedMaterial = material;
            MaterialPropertyBlock properties = new();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            properties.SetColor("_Color", color);
            renderer.SetPropertyBlock(properties);
            return piece.transform;
        }
    }
}

using System;
using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Creatures
{
    /// <summary>Shows the befriended Mossling's small, live-updating resource focus without automating gathering.</summary>
    public sealed class MosslingResourceGuide : MonoBehaviour
    {
        private readonly List<MosslingGuideCandidate> _candidates = new();
        private PrototypePlayerController _player = null!;
        private PrototypeGatheringController _gathering = null!;
        private GameObject _marker = null!;
        private Transform[] _trail = Array.Empty<Transform>();
        private bool _guidanceEnabled;

        public string Status { get; private set; } = string.Empty;
        public MosslingGuideSelection Selection { get; private set; }

        public void Configure(PrototypePlayerController player, PrototypeGatheringController gathering)
        {
            _player = player;
            _gathering = gathering;
            CreateVisuals();
            SetGuidanceEnabled(false);
        }

        public void SetGuidanceEnabled(bool enabled)
        {
            _guidanceEnabled = enabled;
            if (!enabled)
            {
                Status = string.Empty;
                if (_marker != null) _marker.SetActive(false);
                SetTrailActive(false);
            }
        }

        private void Update()
        {
            if (!_guidanceEnabled || _player.IsPaused)
            {
                if (_marker != null) _marker.SetActive(false);
                SetTrailActive(false);
                return;
            }

            _candidates.Clear();
            IReadOnlyList<GatheringNode> nodes = _gathering.Nodes;
            for (int index = 0; index < nodes.Count; index++)
            {
                GatheringNode node = nodes[index];
                if (node.IsAvailable || node.IsRenewing)
                {
                    _candidates.Add(new MosslingGuideCandidate(node.DisplayName, node.transform.position, node.IsAvailable, node.RenewalDeadlineUtcTicks));
                }
            }

            Selection = MosslingGuideRules.Select(_player.transform.position, _candidates);
            if (Selection.Kind == MosslingGuideKind.Available)
            {
                Status = $"MOSSling GUIDE → {Selection.NodeName.ToUpperInvariant()}";
                _marker.SetActive(true);
                _marker.transform.position = Selection.Position + new Vector3(0f, 1.15f, 0f);
                AnimateMarkerAndTrail(Selection.Position);
                return;
            }

            _marker.SetActive(false);
            SetTrailActive(false);
            Status = Selection.Kind == MosslingGuideKind.Renewing
                ? $"MOSSling GUIDE: {Selection.NodeName.ToUpperInvariant()} renews {RenewalRules.FormatRemaining(Selection.RenewalDeadlineUtcTicks, DateTime.UtcNow)}"
                : "MOSSling GUIDE: resources are quiet.";
        }

        private void CreateVisuals()
        {
            _marker = new GameObject("Mossling Guide Available Marker");
            Material guideMaterial = Resources.Load<Material>("Phase14/WayrootGlow");
            CreateGlowPiece("Guide Leaf", PrimitiveType.Sphere, _marker.transform, new Vector3(-0.23f, 0f, 0f), new Vector3(0.28f, 0.10f, 0.48f), guideMaterial);
            CreateGlowPiece("Guide Spark", PrimitiveType.Sphere, _marker.transform, new Vector3(0.23f, 0.08f, 0f), new Vector3(0.14f, 0.14f, 0.14f), guideMaterial);
            CreateGlowPiece("Guide Mote", PrimitiveType.Sphere, _marker.transform, new Vector3(0f, 0.24f, 0f), new Vector3(0.10f, 0.10f, 0.10f), guideMaterial);
            Light glow = _marker.AddComponent<Light>();
            glow.type = LightType.Point;
            glow.color = new Color(0.72f, 1f, 0.58f);
            glow.intensity = 1.1f;
            glow.range = 2.8f;

            _trail = new Transform[3];
            for (int index = 0; index < _trail.Length; index++)
            {
                GameObject mote = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                mote.name = $"Mossling Guide Trail Mote {index + 1}";
                Destroy(mote.GetComponent<Collider>());
                mote.transform.localScale = Vector3.one * (0.11f + index * 0.025f);
                if (guideMaterial != null) mote.GetComponent<Renderer>().sharedMaterial = guideMaterial;
                _trail[index] = mote.transform;
            }
        }

        private static void CreateGlowPiece(string name, PrimitiveType primitive, Transform parent, Vector3 localPosition, Vector3 localScale, Material material)
        {
            GameObject piece = GameObject.CreatePrimitive(primitive);
            piece.name = name;
            Destroy(piece.GetComponent<Collider>());
            piece.transform.SetParent(parent, false);
            piece.transform.localPosition = localPosition;
            piece.transform.localScale = localScale;
            if (material != null) piece.GetComponent<Renderer>().sharedMaterial = material;
        }

        private void AnimateMarkerAndTrail(Vector3 targetPosition)
        {
            float pulse = 1f + Mathf.Sin(Time.unscaledTime * 4f) * 0.12f;
            _marker.transform.localScale = Vector3.one * pulse;
            for (int index = 0; index < _trail.Length; index++)
            {
                Transform mote = _trail[index];
                float progress = (index + 1f) / (_trail.Length + 1f);
                Vector3 position = Vector3.Lerp(transform.position + Vector3.up * 0.55f, targetPosition + Vector3.up * 0.75f, progress);
                position.y += Mathf.Sin(Time.unscaledTime * 3f + index) * 0.10f;
                mote.position = position;
                mote.gameObject.SetActive(true);
            }
        }

        private void SetTrailActive(bool active)
        {
            for (int index = 0; index < _trail.Length; index++)
            {
                if (_trail[index] != null) _trail[index].gameObject.SetActive(active);
            }
        }
    }
}

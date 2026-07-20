using System;
using Wayroot.Character;
using Wayroot.Combat;
using Wayroot.Creatures;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Art
{
    /// <summary>Small allocation-free motion rig for the runtime-composed Phase 18 primitives. It never changes gameplay roots or colliders.</summary>
    public sealed class ProceduralStylizedAnimator : MonoBehaviour
    {
        public enum MotionStyle { Player, Mossling, Slime, Guardian, Foliage, Water, Landmark }

        [SerializeField] private MotionStyle style;
        [SerializeField] private Transform[] animatedParts = Array.Empty<Transform>();
        [SerializeField] private PrototypePlayerController player = null!;
        [SerializeField] private PrototypeCreatureController mossling = null!;
        [SerializeField] private PrototypeEnemy enemy = null!;
        [SerializeField] private PrototypeGatheringController gathering = null!;

        private Vector3[] _basePositions = Array.Empty<Vector3>();
        private Vector3[] _baseScales = Array.Empty<Vector3>();
        private Quaternion[] _baseRotations = Array.Empty<Quaternion>();
        private float _emphasisUntil;
        private bool _wasDefeated;

        public MotionStyle Style => style;
        public bool IsConfigured => animatedParts.Length > 0 && animatedParts[0] != null;

        public void Configure(MotionStyle motionStyle, Transform[] parts, PrototypePlayerController playerController = null,
            PrototypeCreatureController creatureController = null, PrototypeEnemy prototypeEnemy = null,
            PrototypeGatheringController gatheringController = null)
        {
            style = motionStyle;
            animatedParts = parts;
            player = playerController;
            mossling = creatureController;
            enemy = prototypeEnemy;
            gathering = gatheringController;
            _basePositions = new Vector3[parts.Length];
            _baseScales = new Vector3[parts.Length];
            _baseRotations = new Quaternion[parts.Length];
            for (int index = 0; index < parts.Length; index++)
            {
                _basePositions[index] = parts[index].localPosition;
                _baseScales[index] = parts[index].localScale;
                _baseRotations[index] = parts[index].localRotation;
            }
        }

        public void Emphasize(float seconds = 0.28f) => _emphasisUntil = Mathf.Max(_emphasisUntil, Time.time + seconds);

        private void Update()
        {
            if (!IsConfigured) return;
            float time = Time.time;
            float emphasis = Time.time < _emphasisUntil ? 1f - (( _emphasisUntil - time) / 0.28f) : 0f;
            bool moving = player != null && player.CurrentMove.sqrMagnitude > 0.01f;
            bool defeated = enemy != null && enemy.IsDefeated;
            if (enemy != null && defeated != _wasDefeated)
            {
                Emphasize(defeated ? 0.45f : 0.7f);
                _wasDefeated = defeated;
            }

            for (int index = 0; index < animatedParts.Length; index++)
            {
                Transform part = animatedParts[index];
                if (part == null) continue;
                float phase = time * (style == MotionStyle.Water ? 1.7f : 2.25f) + index * 1.37f;
                float wave = Mathf.Sin(phase);
                part.localPosition = _basePositions[index];
                part.localScale = _baseScales[index];
                part.localRotation = _baseRotations[index];

                switch (style)
                {
                    case MotionStyle.Player:
                        float stride = moving ? Mathf.Sin(time * 10f) : 0f;
                        float breath = moving ? 0.035f * Mathf.Abs(stride) : 0.035f * wave;
                        part.localPosition += new Vector3(index == 1 ? 0.035f * stride : 0f, breath, index == 0 ? -0.035f * stride : 0f);
                        part.localRotation *= Quaternion.Euler(index == 0 ? 3f * stride : 0f, 0f, index == 1 ? 12f * stride : 5f * wave);
                        if (emphasis > 0f) part.localScale *= 1f + 0.16f * Mathf.Sin(emphasis * Mathf.PI);
                        break;
                    case MotionStyle.Mossling:
                        float guide = mossling != null && mossling.IsBefriended ? 1f : 0.45f;
                        part.localPosition += Vector3.up * (0.06f + 0.035f * guide) * wave;
                        part.localRotation *= Quaternion.Euler(index < 2 ? 0f : 8f * wave, 0f, index < 2 ? (index == 0 ? 13f : -13f) * wave : 0f);
                        part.localScale = Vector3.Scale(_baseScales[index], new Vector3(1f + 0.06f * wave, 1f - 0.05f * wave, 1f + 0.06f * wave));
                        break;
                    case MotionStyle.Slime:
                    case MotionStyle.Guardian:
                        float squash = defeated ? -0.25f : 0.11f * wave;
                        part.localPosition += Vector3.up * (0.06f + 0.04f * wave);
                        part.localScale = Vector3.Scale(_baseScales[index], new Vector3(1f - squash * 0.55f, 1f + squash, 1f - squash * 0.55f));
                        if (emphasis > 0f) part.localScale *= 1f + 0.18f * Mathf.Sin(emphasis * Mathf.PI);
                        break;
                    case MotionStyle.Foliage:
                        part.localRotation *= Quaternion.Euler(3f * wave, 0f, 5f * wave);
                        break;
                    case MotionStyle.Water:
                        part.localPosition += Vector3.up * 0.035f * wave;
                        part.localScale = Vector3.Scale(_baseScales[index], new Vector3(1f + 0.025f * wave, 1f, 1f - 0.025f * wave));
                        break;
                    case MotionStyle.Landmark:
                        part.localPosition += Vector3.up * (0.07f + index * 0.015f) * wave;
                        part.localScale *= 1f + 0.07f * wave;
                        break;
                }
            }
        }
    }
}

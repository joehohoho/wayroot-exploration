using System;
using Wayroot.Character;
using Wayroot.Combat;
using Wayroot.Creatures;
using Wayroot.Gathering;
using Wayroot.UI;
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
        private AccessibilityPreferences? _preferences;

        public MotionStyle Style => style;
        public bool IsConfigured => animatedParts.Length > 0 && animatedParts[0] != null;

        public void Configure(MotionStyle motionStyle, Transform[] parts, PrototypePlayerController playerController = null,
            PrototypeCreatureController creatureController = null, PrototypeEnemy prototypeEnemy = null,
            PrototypeGatheringController gatheringController = null, AccessibilityPreferences? accessibilityPreferences = null)
        {
            style = motionStyle;
            animatedParts = parts;
            player = playerController;
            mossling = creatureController;
            enemy = prototypeEnemy;
            gathering = gatheringController;
            _preferences = accessibilityPreferences;
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
                float ambientWave = wave * AccessibilityRules.GetMotionAmplitude(1f, _preferences != null && _preferences.ReducedMotion);
                part.localPosition = _basePositions[index];
                part.localScale = _baseScales[index];
                part.localRotation = _baseRotations[index];

                switch (style)
                {
                    case MotionStyle.Player:
                        float stride = moving ? Mathf.Sin(time * 10f) : 0f;
                        float breath = moving ? 0.035f * Mathf.Abs(stride) : 0.035f * ambientWave;
                        bool dodging = player != null && player.IsDodging;
                        part.localPosition += new Vector3(index == 1 ? 0.035f * stride : 0f, breath, index == 0 ? -0.035f * stride : 0f);
                        part.localRotation *= Quaternion.Euler(index == 0 ? 3f * stride : 0f, 0f, index == 1 ? 12f * stride : 5f * ambientWave);
                        if (index >= 5)
                        {
                            part.localPosition += Vector3.back * (dodging ? 0.70f : 0.25f);
                            part.localScale *= dodging ? 1f : 0.01f;
                        }
                        if (dodging) part.localScale = Vector3.Scale(part.localScale, new Vector3(1.12f, 0.82f, 1.24f));
                        if (emphasis > 0f) part.localScale *= 1f + 0.16f * Mathf.Sin(emphasis * Mathf.PI);
                        break;
                    case MotionStyle.Mossling:
                        float guide = mossling != null && mossling.IsBefriended ? 1f : 0.45f;
                        part.localPosition += Vector3.up * (0.06f + 0.035f * guide) * ambientWave;
                        part.localRotation *= Quaternion.Euler(index < 2 ? 0f : 8f * ambientWave, 0f, index < 2 ? (index == 0 ? 13f : -13f) * ambientWave : 0f);
                        part.localScale = Vector3.Scale(_baseScales[index], new Vector3(1f + 0.06f * ambientWave, 1f - 0.05f * ambientWave, 1f + 0.06f * ambientWave));
                        break;
                    case MotionStyle.Slime:
                    case MotionStyle.Guardian:
                        float squash = defeated ? -0.25f : 0.11f * ambientWave;
                        bool anticipating = enemy != null && enemy.GetComponent<PrototypeEnemyChase>()?.IsAnticipating == true;
                        if (anticipating) squash = 0.23f;
                        part.localPosition += Vector3.up * (0.06f + 0.04f * ambientWave);
                        part.localScale = Vector3.Scale(_baseScales[index], new Vector3(1f - squash * 0.55f, 1f + squash, 1f - squash * 0.55f));
                        if (anticipating) part.localPosition += transform.forward * 0.08f;
                        if (emphasis > 0f) part.localScale *= 1f + 0.18f * Mathf.Sin(emphasis * Mathf.PI);
                        break;
                    case MotionStyle.Foliage:
                        part.localRotation *= Quaternion.Euler(3f * ambientWave, 0f, 5f * ambientWave);
                        break;
                    case MotionStyle.Water:
                        part.localPosition += Vector3.up * 0.035f * ambientWave;
                        part.localScale = Vector3.Scale(_baseScales[index], new Vector3(1f + 0.025f * ambientWave, 1f, 1f - 0.025f * ambientWave));
                        break;
                    case MotionStyle.Landmark:
                        part.localPosition += Vector3.up * (0.07f + index * 0.015f) * ambientWave;
                        part.localScale *= 1f + 0.07f * ambientWave;
                        break;
                }
            }
        }
    }
}

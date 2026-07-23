using UnityEngine;
using Wayroot.UI;

namespace Wayroot.Combat
{
    /// <summary>Runtime-composed, mobile-scale enemy state cues using existing enemy and chase semantics.</summary>
    public sealed class EnemyEncounterPresentation : MonoBehaviour
    {
        private PrototypeEnemy _enemy = null!;
        private PrototypeEnemyChase _chase = null!;
        private bool _guardian;
        private Transform _anticipationGround = null!;
        private Transform _anticipationArc = null!;
        private Transform _hitFlash = null!;
        private Transform _defeatMarker = null!;
        private Transform _respawnMarker = null!;
        private AccessibilityPreferences _preferences = null!;

        public bool IsGuardian => _guardian;
        public bool IsShowingAnticipation => _chase != null && _chase.IsAnticipating;

        public void Configure(PrototypeEnemy enemy, PrototypeEnemyChase chase, bool guardian, AccessibilityPreferences preferences)
        {
            _enemy = enemy;
            _chase = chase;
            _guardian = guardian;
            _preferences = preferences;
            Color cueColor = guardian ? new Color(0.72f, 0.94f, 0.28f) : new Color(1f, 0.48f, 0.34f);
            Color hitColor = guardian ? new Color(1f, 0.76f, 0.30f) : new Color(1f, 0.88f, 0.52f);
            _anticipationGround = CreateVisual($"{enemy.DisplayName} Anticipation Ground Cue", PrimitiveType.Cylinder, new Vector3(0f, -0.92f, 0f), guardian ? new Vector3(1.40f, 0.018f, 1.40f) : new Vector3(1.15f, 0.018f, 1.15f), cueColor);
            _anticipationArc = CreateVisual($"{enemy.DisplayName} Anticipation Arc Cue", PrimitiveType.Sphere, new Vector3(0f, 0.72f, 0.56f), guardian ? new Vector3(0.92f, 0.10f, 0.22f) : new Vector3(0.68f, 0.08f, 0.18f), cueColor);
            _hitFlash = CreateVisual($"{enemy.DisplayName} Hit Flash", PrimitiveType.Sphere, new Vector3(0f, 0.22f, 0f), guardian ? Vector3.one * 1.62f : Vector3.one * 1.38f, hitColor);
            _defeatMarker = CreateVisual($"{enemy.DisplayName} Defeat Marker", PrimitiveType.Cylinder, new Vector3(0f, -0.90f, 0f), guardian ? new Vector3(1.56f, 0.025f, 1.56f) : new Vector3(1.26f, 0.025f, 1.26f), new Color(0.98f, 0.62f, 0.26f));
            _respawnMarker = CreateVisual($"{enemy.DisplayName} Respawn Marker", PrimitiveType.Cylinder, new Vector3(0f, -0.89f, 0f), guardian ? new Vector3(1.72f, 0.02f, 1.72f) : new Vector3(1.42f, 0.02f, 1.42f), cueColor);
            SetAllVisible(false);
        }

        private void Update()
        {
            if (_enemy == null) return;
            float time = Time.time;
            bool anticipation = IsShowingAnticipation;
            bool hit = CombatEncounterPolishRules.IsActive(_enemy.HitElapsed, AccessibilityRules.GetFlashDuration(CombatEncounterPolishRules.EnemyHitFlashSeconds, _preferences.ReducedFlash)) && !_enemy.IsDefeated;
            bool defeat = _enemy.IsDefeated;
            bool respawn = CombatEncounterPolishRules.IsActive(_enemy.RespawnElapsed, CombatEncounterPolishRules.EnemyRespawnCueSeconds);
            _anticipationGround.gameObject.SetActive(anticipation);
            _anticipationArc.gameObject.SetActive(anticipation);
            _hitFlash.gameObject.SetActive(hit);
            _defeatMarker.gameObject.SetActive(defeat);
            _respawnMarker.gameObject.SetActive(respawn);
            if (anticipation)
            {
                float pulse = 1f + 0.12f * Mathf.Sin(time * 18f);
                _anticipationGround.localScale = Vector3.Scale(_guardian ? new Vector3(1.40f, 0.018f, 1.40f) : new Vector3(1.15f, 0.018f, 1.15f), new Vector3(pulse, 1f, pulse));
                _anticipationArc.localPosition = new Vector3(0f, 0.72f, 0.48f + 0.08f * Mathf.Sin(time * 14f));
            }
            if (hit)
            {
                float pulse = 1f + AccessibilityRules.GetMotionAmplitude(0.18f, _preferences.ReducedFlash) * Mathf.Sin(time * 28f);
                _hitFlash.localScale = (_guardian ? Vector3.one * 1.62f : Vector3.one * 1.38f) * pulse;
            }
            if (defeat) _defeatMarker.localScale = (_guardian ? new Vector3(1.56f, 0.025f, 1.56f) : new Vector3(1.26f, 0.025f, 1.26f)) * (1f + 0.08f * Mathf.Sin(time * 5f));
            if (respawn) _respawnMarker.localScale = (_guardian ? new Vector3(1.72f, 0.02f, 1.72f) : new Vector3(1.42f, 0.02f, 1.42f)) * (1f + 0.16f * Mathf.Sin(time * 16f));
        }

        private Transform CreateVisual(string name, PrimitiveType primitive, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject visual = GameObject.CreatePrimitive(primitive);
            visual.name = name;
            visual.transform.SetParent(transform, false);
            visual.transform.localPosition = localPosition;
            visual.transform.localScale = localScale;
            Destroy(visual.GetComponent<Collider>());
            MaterialPropertyBlock properties = new();
            Renderer renderer = visual.GetComponent<Renderer>();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            renderer.SetPropertyBlock(properties);
            return visual.transform;
        }

        private void SetAllVisible(bool visible)
        {
            _anticipationGround.gameObject.SetActive(visible);
            _anticipationArc.gameObject.SetActive(visible);
            _hitFlash.gameObject.SetActive(visible);
            _defeatMarker.gameObject.SetActive(visible);
            _respawnMarker.gameObject.SetActive(visible);
        }
    }
}

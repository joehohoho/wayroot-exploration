using UnityEngine;
using Wayroot.UI;

namespace Wayroot.Combat
{
    /// <summary>Compact presentation-only player contact effects; it never moves the player root or changes attack rules.</summary>
    public sealed class PlayerAttackPresentation : MonoBehaviour
    {
        private Transform _trail = null!;
        private Transform _impact = null!;
        private Transform _contactMarker = null!;
        private AccessibilityPreferences _preferences = null!;
        private float _attackAt = float.NegativeInfinity;
        private Vector3 _attackDirection = Vector3.forward;

        public bool IsShowingTrail => CombatEncounterPolishRules.IsActive(Time.time - _attackAt, CombatEncounterPolishRules.PlayerTrailSeconds);

        public void Configure(AccessibilityPreferences preferences)
        {
            _preferences = preferences;
            _trail = CreateVisual("Player Attack Swing Trail", PrimitiveType.Sphere, new Vector3(0f, 0.42f, 0.75f), new Vector3(0.72f, 0.12f, 0.38f), new Color(1f, 0.76f, 0.28f));
            _impact = CreateVisual("Player Attack Impact Flash", PrimitiveType.Sphere, Vector3.zero, Vector3.one * 0.42f, new Color(1f, 0.92f, 0.58f));
            _contactMarker = CreateVisual("Player Attack Contact Marker", PrimitiveType.Cylinder, Vector3.zero, new Vector3(0.32f, 0.025f, 0.32f), new Color(1f, 0.48f, 0.22f));
            SetVisible(false);
        }

        public void PlayContact(Vector3 targetPosition, bool defeated)
        {
            _attackAt = Time.time;
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f) _attackDirection = direction.normalized;
            _impact.position = targetPosition + Vector3.up * 0.5f;
            _contactMarker.position = targetPosition + Vector3.up * 0.04f;
            float impactSize = defeated ? 0.62f : 0.42f;
            _impact.localScale = Vector3.one * AccessibilityRules.GetMotionAmplitude(impactSize, _preferences.ReducedFlash);
            SetVisible(true);
        }

        private void Update()
        {
            if (_trail == null) return;
            float elapsed = Time.time - _attackAt;
            bool trail = CombatEncounterPolishRules.IsActive(elapsed, CombatEncounterPolishRules.PlayerTrailSeconds);
            bool impact = CombatEncounterPolishRules.IsActive(elapsed, AccessibilityRules.GetFlashDuration(CombatEncounterPolishRules.PlayerImpactSeconds, _preferences.ReducedFlash));
            bool marker = CombatEncounterPolishRules.IsActive(elapsed, CombatEncounterPolishRules.PlayerContactMarkerSeconds);
            _trail.gameObject.SetActive(trail);
            _impact.gameObject.SetActive(impact);
            _contactMarker.gameObject.SetActive(marker);
            if (!trail) return;

            _trail.localPosition = Vector3.up * 0.42f + _attackDirection * (0.65f + elapsed * 1.5f);
            _trail.localRotation = Quaternion.LookRotation(_attackDirection, Vector3.up);
            float pulse = 1f - elapsed / CombatEncounterPolishRules.PlayerTrailSeconds;
            _trail.localScale = new Vector3(0.82f, 0.10f + pulse * 0.10f, 0.42f);
        }

        private Transform CreateVisual(string name, PrimitiveType primitive, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject visual = GameObject.CreatePrimitive(primitive);
            visual.name = name;
            visual.transform.SetParent(transform, false);
            visual.transform.localPosition = localPosition;
            visual.transform.localScale = localScale;
            Destroy(visual.GetComponent<Collider>());
            SetColor(visual.GetComponent<Renderer>(), color);
            return visual.transform;
        }

        private void SetVisible(bool visible)
        {
            _trail.gameObject.SetActive(visible);
            _impact.gameObject.SetActive(visible);
            _contactMarker.gameObject.SetActive(visible);
        }

        private static void SetColor(Renderer renderer, Color color)
        {
            MaterialPropertyBlock properties = new();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            renderer.SetPropertyBlock(properties);
        }
    }
}

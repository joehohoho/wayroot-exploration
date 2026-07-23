using Wayroot.Audio;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Progression
{
    /// <summary>Visual-only Iron Edge landmark, outcome pulse, and equipped-blade read for the existing merchant upgrade.</summary>
    public sealed class MerchantPresentation : MonoBehaviour
    {
        private readonly Color _purchaseColor = new(1f, 0.76f, 0.26f);
        private readonly Color _insufficientColor = new(0.94f, 0.42f, 0.30f);
        private readonly Color _ownedColor = new(0.40f, 0.78f, 0.92f);
        private Transform _station = null!;
        private Transform _player = null!;
        private PrototypeGatheringController _gathering = null!;
        private ProceduralSoundscape _soundscape = null!;
        private PrototypeMerchantController _merchant = null!;
        private Transform _outcomePulse = null!;
        private Transform _equippedBlade = null!;
        private Renderer _lanternRenderer = null!;
        private float _feedbackStartedAt = float.NegativeInfinity;
        private MerchantPurchaseOutcome _lastOutcome;

        public bool IsFeedbackShowing => MerchantPresentationRules.IsFeedbackActive(Time.time - _feedbackStartedAt);
        public MerchantPurchaseOutcome LastOutcome => _lastOutcome;
        public int VisualCount { get; private set; }

        public void Configure(PrototypeMerchantController merchant, PrototypeGatheringController gathering, ProceduralSoundscape soundscape, Transform player)
        {
            _merchant = merchant;
            _gathering = gathering;
            _soundscape = soundscape;
            _station = merchant.transform;
            _player = player;
            transform.localScale = Vector3.one;
            transform.position = _station.position;

            Transform stall = new GameObject("Iron Edge Stall Visual Root").transform;
            stall.SetParent(transform, false);
            CreateVisual("Iron Edge Stall Counter", PrimitiveType.Cube, stall, new Vector3(0f, 0.62f, -0.82f), new Vector3(2.15f, 0.18f, 0.46f), new Color(0.24f, 0.10f, 0.04f));
            CreateVisual("Iron Edge Stall Post Left", PrimitiveType.Cylinder, stall, new Vector3(-0.98f, 1.28f, -0.28f), new Vector3(0.12f, 1.16f, 0.12f), new Color(0.33f, 0.16f, 0.06f));
            CreateVisual("Iron Edge Stall Post Right", PrimitiveType.Cylinder, stall, new Vector3(0.98f, 1.28f, -0.28f), new Vector3(0.12f, 1.16f, 0.12f), new Color(0.33f, 0.16f, 0.06f));
            CreateVisual("Iron Edge Canopy Trim", PrimitiveType.Cube, stall, new Vector3(0f, 2.25f, -0.22f), new Vector3(2.35f, 0.12f, 0.66f), new Color(0.72f, 0.19f, 0.12f));
            CreateVisual("Iron Edge Anvil", PrimitiveType.Cube, stall, new Vector3(-0.46f, 0.94f, -0.65f), new Vector3(0.46f, 0.16f, 0.28f), new Color(0.31f, 0.38f, 0.45f));
            CreateVisual("Iron Edge Display Blade", PrimitiveType.Cube, stall, new Vector3(0.38f, 1.15f, -0.66f), new Vector3(0.12f, 0.78f, 0.10f), _purchaseColor).localRotation = Quaternion.Euler(0f, 0f, -24f);
            _lanternRenderer = CreateVisual("Iron Edge Ember Lantern", PrimitiveType.Sphere, stall, new Vector3(1.18f, 1.72f, -0.24f), new Vector3(0.28f, 0.38f, 0.28f), _purchaseColor).GetComponent<Renderer>();
            _outcomePulse = CreateVisual("Iron Edge Purchase Outcome Ring", PrimitiveType.Cylinder, transform, new Vector3(0f, 0.03f, 0f), new Vector3(1.72f, 0.018f, 1.72f), _purchaseColor);
            _outcomePulse.gameObject.SetActive(false);

            Transform weaponRoot = new GameObject("Iron Edge Player Upgrade Visual").transform;
            weaponRoot.SetParent(transform, false);
            _equippedBlade = CreateVisual("Iron Edge Equipped Blade", PrimitiveType.Cube, weaponRoot, new Vector3(0.38f, 0.48f, 0.36f), new Vector3(0.10f, 0.72f, 0.08f), _purchaseColor);
            CreateVisual("Iron Edge Equipped Guard", PrimitiveType.Cube, weaponRoot, new Vector3(0.38f, 0.16f, 0.36f), new Vector3(0.28f, 0.06f, 0.10f), new Color(0.30f, 0.16f, 0.05f));
            weaponRoot.gameObject.SetActive(false);

            _merchant.PurchaseAttempted += ShowOutcome;
        }

        private void Update()
        {
            if (_station == null) return;
            transform.position = _station.position;
            transform.localScale = Vector3.one;
            Transform weaponRoot = _equippedBlade.parent!;
            weaponRoot.gameObject.SetActive(_gathering.WeaponLevel > 0);
            weaponRoot.position = _player.position;
            weaponRoot.rotation = _player.rotation;

            float elapsed = Time.time - _feedbackStartedAt;
            bool active = MerchantPresentationRules.IsFeedbackActive(elapsed);
            _outcomePulse.gameObject.SetActive(active);
            if (active)
            {
                float pulse = 1f + elapsed * 1.25f;
                _outcomePulse.localScale = new Vector3(1.72f * pulse, 0.018f, 1.72f * pulse);
            }

            float lanternPulse = 0.92f + Mathf.Sin(Time.time * 2.2f) * 0.08f;
            SetPropertyColor(_lanternRenderer, _purchaseColor * lanternPulse);
        }

        private void ShowOutcome(MerchantPurchaseOutcome outcome)
        {
            _lastOutcome = outcome;
            _feedbackStartedAt = Time.time;
            Color color = outcome switch
            {
                MerchantPurchaseOutcome.Purchased => _purchaseColor,
                MerchantPurchaseOutcome.AlreadyOwned => _ownedColor,
                _ => _insufficientColor
            };
            SetColor(_outcomePulse.GetComponent<Renderer>(), color);
            _soundscape.Play(outcome == MerchantPurchaseOutcome.Purchased ? SoundscapeCue.MerchantPurchase : SoundscapeCue.MerchantUnavailable);
        }

        private Transform CreateVisual(string name, PrimitiveType primitive, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject visual = GameObject.CreatePrimitive(primitive);
            visual.name = name;
            visual.transform.SetParent(parent, false);
            visual.transform.localPosition = localPosition;
            visual.transform.localScale = localScale;
            Destroy(visual.GetComponent<Collider>());
            SetColor(visual.GetComponent<Renderer>(), color);
            VisualCount++;
            return visual.transform;
        }

        private static void SetPropertyColor(Renderer renderer, Color color)
        {
            MaterialPropertyBlock properties = new();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            properties.SetColor("_Color", color);
            renderer.SetPropertyBlock(properties);
        }

        private static void SetColor(Renderer renderer, Color color)
        {
            Material? verifiedMaterial = Resources.Load<Material>("ActorSpriteUnlit");
            if (verifiedMaterial != null) renderer.sharedMaterial = verifiedMaterial;
            SetPropertyColor(renderer, color);
        }

        private void OnDestroy()
        {
            if (_merchant != null) _merchant.PurchaseAttempted -= ShowOutcome;
        }
    }
}

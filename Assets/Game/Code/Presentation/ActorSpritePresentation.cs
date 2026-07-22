using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Combat;
using Wayroot.Gathering;
using UnityEngine;

namespace Wayroot.Presentation
{
    /// <summary>Billboarded SpriteRenderer presentation only. Gameplay roots, colliders, and camera targets remain untouched.</summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class ActorSpritePresentation : MonoBehaviour
    {
        private SpriteRenderer _renderer = null!;
        private Material _spriteMaterial = null!;
        private UnityEngine.Camera _camera = null!;
        private PrototypePlayerController? _player;
        private PrototypePlayerHealth? _playerHealth;
        private PrototypeAttackController? _attack;
        private PrototypeGatheringController? _gathering;
        private PrototypeEnemy? _enemy;
        private PrototypeEnemyChase? _chase;
        private bool _guardian;
        private Vector3 _lastRootPosition;
        private float _lastMoveAt = float.NegativeInfinity;
        private readonly Dictionary<PlayerSpriteAction, Sprite> _playerFrames = new();
        private readonly Dictionary<EnemySpriteAction, Sprite> _enemyFrames = new();

        public string CurrentActionName { get; private set; } = string.Empty;
        public bool IsEnemyRig => _enemy != null;

        public void ConfigurePlayer(PrototypePlayerController player, PrototypePlayerHealth health, PrototypeAttackController attack, PrototypeGatheringController gathering, UnityEngine.Camera sceneCamera)
        {
            _renderer = GetComponent<SpriteRenderer>();
            _camera = sceneCamera;
            _player = player;
            _playerHealth = health;
            _attack = attack;
            _gathering = gathering;
            ConfigureRenderer(1.04f);
            Sprite initialSprite = ActorSpriteFrameFactory.CreatePlayer(PlayerSpriteAction.Idle, false);
            _playerFrames.Add(PlayerSpriteAction.Idle, initialSprite);
            SetSprite(initialSprite);
            CurrentActionName = PlayerSpriteAction.Idle.ToString();
            ActorSpriteVisualMasker.LogRendererInventory(transform.parent, _renderer);
        }

        public void ConfigureEnemy(PrototypeEnemy enemy, PrototypeEnemyChase chase, bool guardian, UnityEngine.Camera sceneCamera)
        {
            _renderer = GetComponent<SpriteRenderer>();
            _camera = sceneCamera;
            _enemy = enemy;
            _chase = chase;
            _guardian = guardian;
            ConfigureRenderer(guardian ? 1.30f : 1.10f);
            Sprite initialSprite = ActorSpriteFrameFactory.CreateEnemy(guardian, EnemySpriteAction.Idle, false);
            _enemyFrames.Add(EnemySpriteAction.Idle, initialSprite);
            SetSprite(initialSprite);
            CurrentActionName = EnemySpriteAction.Idle.ToString();
            ActorSpriteVisualMasker.LogRendererInventory(transform.parent, _renderer);
        }

        private void ConfigureRenderer(float size)
        {
            Material template = Resources.Load<Material>("ActorSpriteUnlit");
            if (template == null) throw new System.InvalidOperationException("ActorSpriteUnlit material asset is missing.");
            _spriteMaterial = new Material(template) { name = "Actor Sprite Instance Material" };
            _renderer.sharedMaterial = _spriteMaterial;
            _renderer.color = Color.white;
            _renderer.sortingLayerName = "Default";
            _renderer.sortingOrder = 28;
            transform.localScale = Vector3.one * size;
            _lastRootPosition = transform.parent.position;
            ActorSpriteVisualMasker.HideLegacyActorBodyRenderers(transform.parent, _renderer);
        }


        private void SetSprite(Sprite sprite)
        {
            _renderer.sprite = sprite;
            _spriteMaterial.SetTexture("_BaseMap", sprite.texture);
            _spriteMaterial.SetColor("_BaseColor", Color.white);
        }

        private void LateUpdate()
        {
            if (_renderer == null || _camera == null || transform.parent == null) return;
            transform.rotation = Quaternion.LookRotation(-_camera.transform.forward, _camera.transform.up);
            bool facingLeft = Vector3.Dot(transform.parent.forward, _camera.transform.right) < 0f;
            if (_player != null) UpdatePlayer(facingLeft); else if (_enemy != null) UpdateEnemy(facingLeft);
        }

        private void UpdatePlayer(bool facingLeft)
        {
            PlayerSpriteAction action = ActorSpriteAnimationRules.SelectPlayer(_player!.CurrentMove.magnitude, _attack!.AttackElapsed, _gathering!.GatherElapsed,
                _player.IsDodging ? 0f : float.PositiveInfinity, _playerHealth!.RespawnElapsed);
            if (!_playerFrames.TryGetValue(action, out Sprite sprite))
            {
                sprite = ActorSpriteFrameFactory.CreatePlayer(action, facingLeft);
                _playerFrames.Add(action, sprite);
            }
            SetSprite(sprite);
            CurrentActionName = action.ToString();
        }

        private void UpdateEnemy(bool facingLeft)
        {
            if ((transform.parent.position - _lastRootPosition).sqrMagnitude > 0.00001f) _lastMoveAt = Time.time;
            _lastRootPosition = transform.parent.position;
            float windupElapsed = _chase!.IsAnticipating ? 0f : float.PositiveInfinity;
            EnemySpriteAction action = ActorSpriteAnimationRules.SelectEnemy(_enemy!.IsDefeated, Time.time - _lastMoveAt, _enemy.HitElapsed, windupElapsed, _enemy.RespawnElapsed);
            if (!_enemyFrames.TryGetValue(action, out Sprite sprite))
            {
                sprite = ActorSpriteFrameFactory.CreateEnemy(_guardian, action, facingLeft);
                _enemyFrames.Add(action, sprite);
            }
            SetSprite(sprite);
            _renderer.enabled = transform.parent.gameObject.activeInHierarchy;
            CurrentActionName = action.ToString();
        }

        private void OnDestroy()
        {
            foreach (Sprite sprite in _playerFrames.Values) if (sprite != null) Destroy(sprite.texture);
            foreach (Sprite sprite in _enemyFrames.Values) if (sprite != null) Destroy(sprite.texture);
        }
    }
}

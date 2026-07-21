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
            ConfigureRenderer(1.12f);
        }

        public void ConfigureEnemy(PrototypeEnemy enemy, PrototypeEnemyChase chase, bool guardian, UnityEngine.Camera sceneCamera)
        {
            _renderer = GetComponent<SpriteRenderer>();
            _camera = sceneCamera;
            _enemy = enemy;
            _chase = chase;
            _guardian = guardian;
            ConfigureRenderer(guardian ? 1.48f : 1.20f);
            Sprite initialSprite = ActorSpriteFrameFactory.CreateEnemy(guardian, EnemySpriteAction.Idle, false);
            _enemyFrames.Add(EnemySpriteAction.Idle, initialSprite);
            _renderer.sprite = initialSprite;
            CurrentActionName = EnemySpriteAction.Idle.ToString();
        }

        private void ConfigureRenderer(float size)
        {
            _renderer.sortingOrder = 20;
            transform.localScale = Vector3.one * size;
            _lastRootPosition = transform.parent.position;
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
            _renderer.sprite = sprite;
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
            _renderer.sprite = sprite;
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

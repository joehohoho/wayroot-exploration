using System;
using NUnit.Framework;
using UnityEngine;
using Wayroot.Presentation;

namespace Wayroot.Tests.EditMode
{
    public sealed class ActorSpriteAnimationRulesTests
    {
        [Test]
        public void EveryProceduralSpriteFrame_HasFullyTransparentClearCorners()
        {
            foreach (PlayerSpriteAction action in Enum.GetValues(typeof(PlayerSpriteAction)))
            {
                AssertClearCorners(ActorSpriteFrameFactory.CreatePlayer(action, false));
                AssertClearCorners(ActorSpriteFrameFactory.CreatePlayer(action, true));
            }

            foreach (EnemySpriteAction action in Enum.GetValues(typeof(EnemySpriteAction)))
            {
                AssertClearCorners(ActorSpriteFrameFactory.CreateEnemy(false, action, false));
                AssertClearCorners(ActorSpriteFrameFactory.CreateEnemy(true, action, true));
            }
        }

        [Test]
        public void PlayerSelection_PrioritizesSafeDefeatAndSemanticActionsOverWalking()
        {
            float inactive = float.PositiveInfinity;
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, inactive, inactive, inactive, 0.1f), Is.EqualTo(PlayerSpriteAction.Defeated));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, 0.08f, inactive, inactive, inactive), Is.EqualTo(PlayerSpriteAction.AttackWindup));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, 0.18f, inactive, inactive, inactive), Is.EqualTo(PlayerSpriteAction.AttackSwing));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, 0.34f, inactive, inactive, inactive), Is.EqualTo(PlayerSpriteAction.AttackRecover));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, inactive, 0.08f, inactive, inactive), Is.EqualTo(PlayerSpriteAction.GatherReach));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, inactive, 0.28f, inactive, inactive), Is.EqualTo(PlayerSpriteAction.GatherCollect));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, inactive, inactive, 0.08f, inactive), Is.EqualTo(PlayerSpriteAction.Dodge));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0.4f, inactive, inactive, inactive, inactive), Is.EqualTo(PlayerSpriteAction.Walk));
            Assert.That(ActorSpriteAnimationRules.SelectPlayer(0f, inactive, inactive, inactive, inactive), Is.EqualTo(PlayerSpriteAction.Idle));
        }

        [Test]
        public void EnemySelection_UsesDistinctCombatAndRecoveryFrames()
        {
            float inactive = float.PositiveInfinity;
            Assert.That(ActorSpriteAnimationRules.SelectEnemy(true, inactive, inactive, inactive, inactive), Is.EqualTo(EnemySpriteAction.Defeated));
            Assert.That(ActorSpriteAnimationRules.SelectEnemy(false, inactive, inactive, inactive, 0.1f), Is.EqualTo(EnemySpriteAction.Respawn));
            Assert.That(ActorSpriteAnimationRules.SelectEnemy(false, inactive, 0.1f, inactive, inactive), Is.EqualTo(EnemySpriteAction.Hit));
            Assert.That(ActorSpriteAnimationRules.SelectEnemy(false, inactive, inactive, 0.1f, inactive), Is.EqualTo(EnemySpriteAction.Windup));
            Assert.That(ActorSpriteAnimationRules.SelectEnemy(false, 0.1f, inactive, inactive, inactive), Is.EqualTo(EnemySpriteAction.Chase));
            Assert.That(ActorSpriteAnimationRules.SelectEnemy(false, inactive, inactive, inactive, inactive), Is.EqualTo(EnemySpriteAction.Idle));
        }

        private static void AssertClearCorners(Sprite sprite)
        {
            Texture2D texture = sprite.texture;
            Assert.That(texture.GetPixel(0, 0).a, Is.EqualTo(0f), sprite.name);
            Assert.That(texture.GetPixel(63, 0).a, Is.EqualTo(0f), sprite.name);
            Assert.That(texture.GetPixel(0, 63).a, Is.EqualTo(0f), sprite.name);
            Assert.That(texture.GetPixel(63, 63).a, Is.EqualTo(0f), sprite.name);
            UnityEngine.Object.DestroyImmediate(sprite.texture);
            UnityEngine.Object.DestroyImmediate(sprite);
        }
    }
}

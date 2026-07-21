namespace Wayroot.Presentation
{
    public enum PlayerSpriteAction { Idle, Walk, AttackWindup, AttackSwing, AttackRecover, GatherReach, GatherCollect, Dodge, Defeated, Respawn }
    public enum EnemySpriteAction { Idle, Chase, Windup, Hit, Defeated, Respawn }

    /// <summary>Pure semantic priority contract for the original Phase 23 sprite frames.</summary>
    public static class ActorSpriteAnimationRules
    {
        public const float AttackWindow = 0.42f;
        public const float GatherWindow = 0.40f;
        public const float DodgeWindow = 0.24f;
        public const float DefeatWindow = 0.36f;
        public const float HitWindow = 0.20f;
        public const float RespawnWindow = 0.45f;

        public static PlayerSpriteAction SelectPlayer(float moveMagnitude, float attackElapsed, float gatherElapsed, float dodgeElapsed, float defeatElapsed)
        {
            if (defeatElapsed >= 0f && defeatElapsed < DefeatWindow) return PlayerSpriteAction.Defeated;
            if (dodgeElapsed >= 0f && dodgeElapsed < DodgeWindow) return PlayerSpriteAction.Dodge;
            if (attackElapsed >= 0f && attackElapsed < AttackWindow)
            {
                return attackElapsed < 0.13f ? PlayerSpriteAction.AttackWindup : attackElapsed < 0.27f ? PlayerSpriteAction.AttackSwing : PlayerSpriteAction.AttackRecover;
            }
            if (gatherElapsed >= 0f && gatherElapsed < GatherWindow) return gatherElapsed < 0.20f ? PlayerSpriteAction.GatherReach : PlayerSpriteAction.GatherCollect;
            return moveMagnitude > 0.01f ? PlayerSpriteAction.Walk : PlayerSpriteAction.Idle;
        }

        public static EnemySpriteAction SelectEnemy(bool defeated, float moveElapsed, float hitElapsed, float windupElapsed, float respawnElapsed)
        {
            if (defeated) return EnemySpriteAction.Defeated;
            if (respawnElapsed >= 0f && respawnElapsed < RespawnWindow) return EnemySpriteAction.Respawn;
            if (hitElapsed >= 0f && hitElapsed < HitWindow) return EnemySpriteAction.Hit;
            if (windupElapsed >= 0f && windupElapsed < 0.44f) return EnemySpriteAction.Windup;
            return moveElapsed >= 0f && moveElapsed < 0.18f ? EnemySpriteAction.Chase : EnemySpriteAction.Idle;
        }
    }
}

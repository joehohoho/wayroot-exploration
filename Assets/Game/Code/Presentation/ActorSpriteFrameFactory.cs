using UnityEngine;

namespace Wayroot.Presentation
{
    /// <summary>Creates compact original pixel-painted sprite frames at runtime; no imported art is required.</summary>
    public static class ActorSpriteFrameFactory
    {
        public static Sprite CreatePlayer(PlayerSpriteAction action, bool facingLeft)
        {
            Texture2D texture = NewTexture();
            Color cloak = new(0.08f, 0.28f, 0.67f, 1f);
            Color cloakLight = new(0.20f, 0.55f, 0.98f, 1f);
            Color skin = new(0.98f, 0.68f, 0.46f, 1f);
            Color hair = new(0.20f, 0.09f, 0.05f, 1f);
            Color lantern = new(1f, 0.74f, 0.22f, 1f);
            float bob = action == PlayerSpriteAction.Walk ? 2f : action == PlayerSpriteAction.Idle ? Mathf.Sin((facingLeft ? 1f : 0f)) : 0f;
            if (action == PlayerSpriteAction.Defeated) { DrawOval(texture, 32, 18, 20, 7, cloak); DrawOval(texture, 39, 19, 7, 5, skin); return Finish(texture, "Explorer Defeated"); }
            DrawOval(texture, 32, 26 + bob, 18, 20, cloak);
            DrawOval(texture, 32, 30 + bob, 13, 17, cloakLight);
            DrawOval(texture, 32, 45 + bob, 10, 9, skin);
            DrawOval(texture, 32, 49 + bob, 11, 5, hair);
            DrawRect(texture, 25, 28 + (int)bob, 14, 3, new Color(0.94f, 0.31f, 0.16f, 1f));
            int side = facingLeft ? -1 : 1;
            int armX = 32 + side * 15;
            DrawOval(texture, armX, 29 + bob, 6, 10, cloakLight);
            DrawRect(texture, armX + side * 4, 23 + (int)bob, 4, 7, lantern);
            if (action == PlayerSpriteAction.AttackWindup) DrawRect(texture, 32 - side * 23, 31, 3, 16, new Color(0.78f, 0.86f, 0.96f, 1f));
            if (action == PlayerSpriteAction.AttackSwing) { DrawRect(texture, 32 + side * 20, 30, 3, 18, Color.white); DrawOval(texture, 32 + side * 14, 33, 10, 3, new Color(0.55f, 0.88f, 1f, 0.7f)); }
            if (action == PlayerSpriteAction.GatherReach || action == PlayerSpriteAction.GatherCollect) DrawOval(texture, 32 + side * 18, action == PlayerSpriteAction.GatherCollect ? 21 : 26, 6, 6, lantern);
            if (action == PlayerSpriteAction.Dodge) { DrawOval(texture, 32 - side * 15, 24, 22, 5, new Color(0.45f, 0.82f, 1f, 0.58f)); DrawRect(texture, 32 + side * 17, 28, 3, 10, cloakLight); }
            if (action == PlayerSpriteAction.Respawn) DrawOval(texture, 32, 55, 14, 4, new Color(0.85f, 0.96f, 1f, 0.62f));
            return Finish(texture, "Blue Cloak Explorer");
        }

        public static Sprite CreateEnemy(bool guardian, EnemySpriteAction action, bool facingLeft)
        {
            Texture2D texture = NewTexture();
            Color body = guardian ? new Color(0.14f, 0.43f, 0.20f, 1f) : new Color(0.78f, 0.17f, 0.32f, 1f);
            Color highlight = guardian ? new Color(0.52f, 0.82f, 0.22f, 1f) : new Color(1f, 0.38f, 0.48f, 1f);
            Color eye = guardian ? new Color(1f, 0.68f, 0.20f, 1f) : Color.white;
            float squash = action == EnemySpriteAction.Windup ? -3f : action == EnemySpriteAction.Chase ? 3f : 0f;
            if (action == EnemySpriteAction.Defeated) { DrawOval(texture, 32, 17, guardian ? 24 : 20, 7, body); DrawRect(texture, 20, 18, 24, 2, highlight); return Finish(texture, guardian ? "Guardian Defeated" : "Slime Defeated"); }
            DrawOval(texture, 32, 27, guardian ? 22 + squash : 19 + squash, guardian ? 23 - squash : 17 - squash, body);
            DrawOval(texture, 32, 31, guardian ? 16 : 14, guardian ? 16 : 11, highlight);
            if (guardian)
            {
                for (int index = -2; index <= 2; index++) DrawTriangle(texture, 32 + index * 7, 47, 5, new Color(0.35f, 0.68f, 0.18f, 1f));
                DrawOval(texture, 32, 31, 5, 6, eye);
            }
            else
            {
                DrawOval(texture, 26, 34, 3, 4, eye); DrawOval(texture, 38, 34, 3, 4, eye);
                DrawRect(texture, 29, 26, 7, 2, new Color(0.30f, 0.07f, 0.14f, 1f));
            }
            int side = facingLeft ? -1 : 1;
            if (action == EnemySpriteAction.Windup) DrawTriangle(texture, 32 + side * 18, 31, 9, new Color(1f, 0.75f, 0.22f, 1f));
            if (action == EnemySpriteAction.Hit) DrawRect(texture, 12, 42, 40, 3, Color.white);
            if (action == EnemySpriteAction.Respawn) DrawOval(texture, 32, 54, 16, 4, new Color(0.68f, 1f, 0.72f, 0.65f));
            return Finish(texture, guardian ? "Thorn Guardian" : "Practice Slime");
        }

        private static Texture2D NewTexture()
        {
            Texture2D texture = new(64, 64, TextureFormat.RGBA32, false) { filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Clamp, name = "Original Phase 23 Sprite Frame" };
            Color[] clear = new Color[64 * 64];
            texture.SetPixels(clear);
            return texture;
        }

        private static Sprite Finish(Texture2D texture, string name)
        {
            texture.Apply(false, true);
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, 64f, 64f), new Vector2(0.5f, 0.18f), 32f);
            sprite.name = name;
            return sprite;
        }

        private static void DrawRect(Texture2D texture, int x, int y, int width, int height, Color color)
        {
            for (int px = x; px < x + width; px++) for (int py = y; py < y + height; py++) if (px >= 0 && px < 64 && py >= 0 && py < 64) texture.SetPixel(px, py, color);
        }

        private static void DrawOval(Texture2D texture, int centerX, float centerY, float radiusX, float radiusY, Color color)
        {
            for (int px = Mathf.FloorToInt(centerX - radiusX); px <= Mathf.CeilToInt(centerX + radiusX); px++) for (int py = Mathf.FloorToInt(centerY - radiusY); py <= Mathf.CeilToInt(centerY + radiusY); py++)
            {
                float x = (px - centerX) / radiusX; float y = (py - centerY) / radiusY;
                if (x * x + y * y <= 1f && px >= 0 && px < 64 && py >= 0 && py < 64) texture.SetPixel(px, py, color);
            }
        }

        private static void DrawTriangle(Texture2D texture, int centerX, int baseY, int size, Color color)
        {
            for (int row = 0; row < size; row++) DrawRect(texture, centerX - row, baseY + row, row * 2 + 1, 1, color);
        }
    }
}

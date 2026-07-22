using UnityEngine;

namespace Wayroot.Presentation
{
    /// <summary>Creates compact original sprite frames with transparent backgrounds; no imported art is required.</summary>
    public static class ActorSpriteFrameFactory
    {
        private static readonly Color Ink = new(0.035f, 0.06f, 0.12f, 1f);
        private static readonly Color Shadow = new(0.02f, 0.05f, 0.10f, 0.42f);

        public static Sprite CreatePlayer(PlayerSpriteAction action, bool facingLeft)
        {
            Texture2D texture = NewTexture();
            int side = facingLeft ? -1 : 1;
            float bob = action == PlayerSpriteAction.Walk ? 2f : 0f;
            Color cloak = new(0.06f, 0.20f, 0.58f, 1f);
            Color cloakLight = new(0.18f, 0.52f, 0.95f, 1f);
            Color skin = new(1f, 0.70f, 0.47f, 1f);
            Color hair = new(0.17f, 0.07f, 0.04f, 1f);
            Color lantern = new(1f, 0.70f, 0.16f, 1f);

            if (action == PlayerSpriteAction.Defeated)
            {
                DrawOutlinedOval(texture, 32, 18, 21, 7, cloak, Ink);
                DrawOutlinedOval(texture, 42, 19, 7, 5, skin, Ink);
                return Finish(texture, "Explorer Defeated");
            }

            DrawOval(texture, 32, 16, 19, 4, Shadow);
            DrawOutlinedOval(texture, 32, 29 + bob, 18, 21, cloak, Ink);
            DrawOval(texture, 32 - side * 4, 31 + bob, 11, 15, cloakLight);
            DrawRect(texture, 26, 27 + (int)bob, 12, 3, new Color(0.90f, 0.28f, 0.15f, 1f));
            DrawOutlinedOval(texture, 32, 46 + bob, 10, 10, skin, Ink);
            DrawOval(texture, 32, 50 + bob, 11, 5, hair);
            DrawRect(texture, 27, 49 + (int)bob, 10, 2, hair);
            DrawRect(texture, 32 + side * 3, 44 + (int)bob, 2, 2, Ink);

            int armX = 32 + side * 15;
            DrawOutlinedOval(texture, armX, 30 + bob, 6, 10, cloakLight, Ink);
            DrawRect(texture, armX + side * 4, 23 + (int)bob, 4, 8, Ink);
            DrawRect(texture, armX + side * 4, 24 + (int)bob, 3, 6, lantern);
            DrawRect(texture, armX + side * 4, 30 + (int)bob, 3, 1, new Color(1f, 0.93f, 0.53f, 1f));

            if (action == PlayerSpriteAction.AttackWindup) DrawOutlinedRect(texture, 32 - side * 22, 30, 3, 16, new Color(0.58f, 0.77f, 0.94f, 1f), Ink);
            if (action == PlayerSpriteAction.AttackSwing)
            {
                DrawOutlinedRect(texture, 32 + side * 20, 30, 3, 19, Color.white, Ink);
                DrawOval(texture, 32 + side * 14, 34, 11, 3, new Color(0.46f, 0.86f, 1f, 0.58f));
            }
            if (action == PlayerSpriteAction.GatherReach || action == PlayerSpriteAction.GatherCollect)
                DrawOutlinedOval(texture, 32 + side * 18, action == PlayerSpriteAction.GatherCollect ? 21 : 26, 6, 6, lantern, Ink);
            if (action == PlayerSpriteAction.Dodge)
            {
                DrawOval(texture, 32 - side * 15, 24, 23, 5, new Color(0.38f, 0.76f, 1f, 0.54f));
                DrawOutlinedRect(texture, 32 + side * 17, 28, 3, 10, cloakLight, Ink);
            }
            if (action == PlayerSpriteAction.Respawn) DrawOval(texture, 32, 55, 15, 4, new Color(0.75f, 0.94f, 1f, 0.62f));
            return Finish(texture, "Blue Cloak Explorer");
        }

        public static Sprite CreateEnemy(bool guardian, EnemySpriteAction action, bool facingLeft)
        {
            Texture2D texture = NewTexture();
            int side = facingLeft ? -1 : 1;
            float squash = action == EnemySpriteAction.Windup ? -3f : action == EnemySpriteAction.Chase ? 3f : 0f;
            Color body = guardian ? new Color(0.09f, 0.34f, 0.16f, 1f) : new Color(0.72f, 0.12f, 0.24f, 1f);
            Color highlight = guardian ? new Color(0.48f, 0.78f, 0.18f, 1f) : new Color(1f, 0.32f, 0.42f, 1f);

            if (action == EnemySpriteAction.Defeated)
            {
                DrawOutlinedOval(texture, 32, 17, guardian ? 24 : 20, 7, body, Ink);
                DrawRect(texture, 21, 19, 22, 2, highlight);
                return Finish(texture, guardian ? "Guardian Defeated" : "Slime Defeated");
            }

            DrawOval(texture, 32, 15, guardian ? 24 : 21, 4, Shadow);
            DrawOutlinedOval(texture, 32, 28, guardian ? 22 + squash : 19 + squash, guardian ? 23 - squash : 17 - squash, body, Ink);
            DrawOval(texture, 32 - side * 3, 31, guardian ? 16 : 14, guardian ? 16 : 11, highlight);
            if (guardian)
            {
                for (int index = -2; index <= 2; index++) DrawOutlinedTriangle(texture, 32 + index * 7, 47, 5, new Color(0.30f, 0.66f, 0.16f, 1f), Ink);
                DrawOutlinedOval(texture, 32, 32, 5, 7, new Color(1f, 0.68f, 0.16f, 1f), Ink);
                DrawRect(texture, 31, 33, 2, 2, Ink);
            }
            else
            {
                DrawOutlinedOval(texture, 26, 34, 3, 4, Color.white, Ink);
                DrawOutlinedOval(texture, 38, 34, 3, 4, Color.white, Ink);
                DrawRect(texture, 26, 34, 2, 2, Ink);
                DrawRect(texture, 38, 34, 2, 2, Ink);
                DrawRect(texture, 29, 26, 7, 2, Ink);
            }

            if (action == EnemySpriteAction.Windup) DrawOutlinedTriangle(texture, 32 + side * 19, 31, 9, new Color(1f, 0.72f, 0.18f, 1f), Ink);
            if (action == EnemySpriteAction.Hit) DrawRect(texture, 13, 42, 39, 3, Color.white);
            if (action == EnemySpriteAction.Respawn) DrawOval(texture, 32, 54, 16, 4, new Color(0.60f, 1f, 0.68f, 0.62f));
            return Finish(texture, guardian ? "Thorn Leaf Guardian" : "Expressive Red Slime");
        }

        private static Texture2D NewTexture()
        {
            Texture2D texture = new(64, 64, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp,
                name = "Original Phase 24 Transparent Sprite Frame"
            };
            Color[] clear = new Color[64 * 64];
            for (int index = 0; index < clear.Length; index++) clear[index] = Color.clear;
            texture.SetPixels(clear);
            return texture;
        }

        private static Sprite Finish(Texture2D texture, string name)
        {
            texture.Apply(false, false);
            Sprite sprite = Sprite.Create(texture, new Rect(0f, 0f, 64f, 64f), new Vector2(0.5f, 0.14f), 42f);
            sprite.name = name;
            return sprite;
        }

        private static void DrawOutlinedRect(Texture2D texture, int x, int y, int width, int height, Color fill, Color outline)
        {
            DrawRect(texture, x - 1, y - 1, width + 2, height + 2, outline);
            DrawRect(texture, x, y, width, height, fill);
        }

        private static void DrawOutlinedOval(Texture2D texture, int centerX, float centerY, float radiusX, float radiusY, Color fill, Color outline)
        {
            DrawOval(texture, centerX, centerY, radiusX + 1f, radiusY + 1f, outline);
            DrawOval(texture, centerX, centerY, radiusX, radiusY, fill);
        }

        private static void DrawOutlinedTriangle(Texture2D texture, int centerX, int baseY, int size, Color fill, Color outline)
        {
            DrawTriangle(texture, centerX, baseY - 1, size + 1, outline);
            DrawTriangle(texture, centerX, baseY, size, fill);
        }

        private static void DrawRect(Texture2D texture, int x, int y, int width, int height, Color color)
        {
            for (int px = x; px < x + width; px++) for (int py = y; py < y + height; py++) if (px >= 0 && px < 64 && py >= 0 && py < 64) texture.SetPixel(px, py, color);
        }

        private static void DrawOval(Texture2D texture, int centerX, float centerY, float radiusX, float radiusY, Color color)
        {
            for (int px = Mathf.FloorToInt(centerX - radiusX); px <= Mathf.CeilToInt(centerX + radiusX); px++) for (int py = Mathf.FloorToInt(centerY - radiusY); py <= Mathf.CeilToInt(centerY + radiusY); py++)
            {
                float x = (px - centerX) / radiusX;
                float y = (py - centerY) / radiusY;
                if (x * x + y * y <= 1f && px >= 0 && px < 64 && py >= 0 && py < 64) texture.SetPixel(px, py, color);
            }
        }

        private static void DrawTriangle(Texture2D texture, int centerX, int baseY, int size, Color color)
        {
            for (int row = 0; row < size; row++) DrawRect(texture, centerX - row, baseY + row, row * 2 + 1, 1, color);
        }
    }
}

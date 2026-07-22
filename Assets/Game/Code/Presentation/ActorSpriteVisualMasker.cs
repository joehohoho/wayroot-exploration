using UnityEngine;
using Wayroot.Combat;

namespace Wayroot.Presentation
{
    /// <summary>Keeps gameplay roots intact while removing their legacy primitive body visuals from sprite-presented actors.</summary>
    public static class ActorSpriteVisualMasker
    {
        public static void HideLegacyActorBodyRenderers(Transform actorRoot, SpriteRenderer designatedSpriteRenderer)
        {
            foreach (Renderer renderer in actorRoot.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer == designatedSpriteRenderer || IsActorUtilityRenderer(renderer) || IsFallbackSilhouetteRenderer(renderer)) continue;
                renderer.enabled = false;
            }
        }

        public static int CountVisibleActorBodyRenderers(Transform actorRoot)
        {
            int visibleCount = 0;
            foreach (Renderer renderer in actorRoot.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer.enabled && !IsActorUtilityRenderer(renderer)) visibleCount++;
            }
            return visibleCount;
        }

        /// <summary>Emits development-build evidence of exactly which renderer owns an actor's pixels.</summary>
        public static void LogRendererInventory(Transform actorRoot, SpriteRenderer designatedSpriteRenderer)
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            foreach (Renderer renderer in actorRoot.GetComponentsInChildren<Renderer>(true))
            {
                string material = renderer.sharedMaterial == null
                    ? "<SpriteRenderer default material>"
                    : $"{renderer.sharedMaterial.name} / {renderer.sharedMaterial.shader.name}";
                string sprite = renderer is SpriteRenderer spriteRenderer && spriteRenderer.sprite != null
                    ? $", sprite={spriteRenderer.sprite.name}, texture={spriteRenderer.sprite.texture.name}, cornerAlpha={spriteRenderer.sprite.texture.GetPixel(0, 0).a:F2}"
                    : string.Empty;
                Debug.Log($"Phase24 renderer audit: actor={actorRoot.name}, object={renderer.gameObject.name}, type={renderer.GetType().Name}, enabled={renderer.enabled}, designated={renderer == designatedSpriteRenderer}, material={material}{sprite}", renderer);
            }
#endif
        }

        private static bool IsFallbackSilhouetteRenderer(Renderer renderer)
        {
            string name = renderer.gameObject.name;
            return name is "Player Cloak" or "Player Lantern" or "Player Hair" or "Player Warm Scarf" or "Slime Crown" or "Slime Face";
        }

        private static bool IsActorUtilityRenderer(Renderer renderer)
        {
            return renderer.GetComponent<TextMesh>() != null || renderer.GetComponent<PrototypeWorldHealthBar>() != null;
        }
    }
}

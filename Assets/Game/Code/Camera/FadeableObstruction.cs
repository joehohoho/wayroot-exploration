using UnityEngine;

namespace Wayroot.Camera
{
    [RequireComponent(typeof(Renderer))]
    public sealed class FadeableObstruction : MonoBehaviour
    {
        [SerializeField, Range(0.05f, 1f)] private float minimumAlpha = 0.2f;
        private MaterialPropertyBlock _propertyBlock = null!;
        private Renderer _renderer = null!;
        private Color _baseColor;
        private bool _initialized;

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();
            Material material = _renderer.material;
            _baseColor = material.HasProperty("_BaseColor") ? material.GetColor("_BaseColor") : material.color;
            ConfigureTransparent(material);
            _initialized = true;
        }

        public void SetObstructing(bool value)
        {
            if (!_initialized || _renderer == null)
            {
                return;
            }

            float alpha = value ? minimumAlpha : _baseColor.a;
            Color color = new(_baseColor.r, _baseColor.g, _baseColor.b, alpha);
            _propertyBlock.SetColor("_BaseColor", color);
            _propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private static void ConfigureTransparent(Material material)
        {
            material.SetFloat("_Surface", 1f);
            material.SetOverrideTag("RenderType", "Transparent");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }
    }
}

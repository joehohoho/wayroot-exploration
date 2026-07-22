using UnityEditor;
using UnityEngine;

namespace Wayroot.Editor
{
    public static class ActorSpriteMaterialAsset
    {
        public static void Create()
        {
            const string folder = "Assets/Game/Resources";
            const string path = folder + "/ActorSpriteUnlit.mat";
            if (!AssetDatabase.IsValidFolder(folder)) AssetDatabase.CreateFolder("Assets/Game", "Resources");
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material == null)
            {
                Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
                if (shader == null) throw new System.InvalidOperationException("URP Unlit shader unavailable in editor.");
                material = new Material(shader) { name = "ActorSpriteUnlit" };
                AssetDatabase.CreateAsset(material, path);
            }
            material.SetFloat("_Surface", 1f);
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            EditorUtility.SetDirty(material);
            AssetDatabase.SaveAssets();
        }
    }
}

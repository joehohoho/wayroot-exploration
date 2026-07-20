using System.IO;
using UnityEditor;
using UnityEngine;

namespace Wayroot.Editor
{
    /// <summary>Creates the small, source-controlled URP material library used by the Phase 14 runtime composition.</summary>
    public static class PhaseFourteenMaterialLibrary
    {
        private const string DirectoryPath = "Assets/Game/Art/Resources/Phase14";

        [MenuItem("Wayroot/Phase 14/Create Material Library")]
        public static void CreateMaterialLibrary()
        {
            Directory.CreateDirectory(DirectoryPath);
            Create("WarmGround", new Color(0.31f, 0.50f, 0.25f));
            Create("Meadow", new Color(0.45f, 0.68f, 0.30f));
            Create("Path", new Color(0.74f, 0.51f, 0.27f));
            Create("Water", new Color(0.17f, 0.55f, 0.62f));
            Create("Bark", new Color(0.30f, 0.16f, 0.08f));
            Create("Leaf", new Color(0.12f, 0.42f, 0.19f));
            Create("Moss", new Color(0.31f, 0.66f, 0.35f));
            Create("Cream", new Color(1f, 0.82f, 0.50f));
            Create("Coral", new Color(0.92f, 0.33f, 0.36f));
            Create("Sky", new Color(0.31f, 0.60f, 0.83f));
            Create("WayrootGlow", new Color(0.28f, 0.94f, 0.60f), 0.25f);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void Create(string name, Color color, float emission = 0f)
        {
            string path = $"{DirectoryPath}/{name}.mat";
            Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (material == null)
            {
                Shader shader = Shader.Find("Universal Render Pipeline/Lit");
                if (shader == null) throw new System.InvalidOperationException("URP/Lit shader was not found.");
                material = new Material(shader);
                AssetDatabase.CreateAsset(material, path);
            }

            material.SetColor("_BaseColor", color);
            material.SetFloat("_Smoothness", 0.12f);
            if (emission > 0f)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", color * emission);
            }
        }
    }
}

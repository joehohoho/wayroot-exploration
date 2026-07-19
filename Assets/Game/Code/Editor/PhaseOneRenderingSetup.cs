#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Wayroot.Editor
{
    /// <summary>Repairs the required URP asset assignment for a source-created project.</summary>
    public static class PhaseOneRenderingSetup
    {
        private const string PipelineAssetPath = "Assets/Game/Settings/WayrootPrototypeRenderPipeline.asset";

        [MenuItem("Wayroot/Repair Phase 1 Rendering")]
        public static void Repair()
        {
            UniversalRenderPipelineAsset pipeline = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(PipelineAssetPath);
            if (pipeline == null)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(PipelineAssetPath)!);
                pipeline = UniversalRenderPipelineAsset.Create();
                AssetDatabase.CreateAsset(pipeline, PipelineAssetPath);
                AssetDatabase.AddObjectToAsset(pipeline.rendererDataList[0], pipeline);
            }

            GraphicsSettings.defaultRenderPipeline = pipeline;
            QualitySettings.renderPipeline = pipeline;
            EditorUtility.SetDirty(pipeline);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Wayroot Phase 1 rendering is configured with the URP pipeline asset.");
        }
    }
}
#endif

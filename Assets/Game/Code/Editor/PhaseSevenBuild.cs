using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Wayroot.Editor
{
    public static class PhaseSevenBuild
    {
        public static void BuildWindowsReviewPlayer()
        {
            string outputPath = Path.Combine("Builds", "Phase7Review", "WayrootPhase7.exe");
            string[] scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray();
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            BuildReport report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.StandaloneWindows64, BuildOptions.Development);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new System.InvalidOperationException($"Phase 7 Windows review build failed: {report.summary.result}");
            }
        }
    }
}

using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Wayroot.Editor
{
    /// <summary>Builds the ignored Windows player used for Phase 34 route and presentation review.</summary>
    public static class PhaseThirtyFourBuild
    {
        public static void BuildWindowsReviewPlayer()
        {
            string outputPath = Path.Combine("Builds", "Phase34ReleaseReadinessReview", "WayrootPhase34.exe");
            string[] scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray();
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            BuildReport report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.StandaloneWindows64, BuildOptions.Development);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new System.InvalidOperationException($"Phase 34 Windows release-readiness review build failed: {report.summary.result}");
            }
        }
    }
}

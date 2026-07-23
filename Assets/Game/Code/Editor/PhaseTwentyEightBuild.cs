using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;

namespace Wayroot.Editor
{
    public static class PhaseTwentyEightBuild
    {
        public static void BuildWindowsReviewPlayer()
        {
            string outputPath = Path.Combine("Builds", "Phase28BloomwellFinaleReview", "WayrootPhase28.exe");
            string[] scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray();
            BuildReport report = BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.StandaloneWindows64, BuildOptions.Development);
            if (report.summary.result != BuildResult.Succeeded)
            {
                throw new System.InvalidOperationException($"Phase 28 Windows Bloomwell finale review build failed: {report.summary.result}");
            }
        }
    }
}

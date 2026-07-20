using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Wayroot.Tests.PlayMode
{
    public sealed class SunmeadowVisualRegionPlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_ComposesSunmeadowVisualLandmarksAroundTheExistingLoops()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(GameObject.Find("Sunmeadow Clearing"), Is.Not.Null);
            Assert.That(GameObject.Find("Sunmeadow Creek"), Is.Not.Null);
            Assert.That(GameObject.Find("Sunmeadow Footpath"), Is.Not.Null);
            Assert.That(GameObject.Find("Sunmeadow North Grove"), Is.Not.Null);
            Assert.That(GameObject.Find("Sunmeadow South Rock Garden"), Is.Not.Null);
            Assert.That(GameObject.Find("Sunmeadow Wildflower Meadow"), Is.Not.Null);
            Assert.That(RenderSettings.ambientMode, Is.EqualTo(UnityEngine.Rendering.AmbientMode.Trilight));
        }
    }
}

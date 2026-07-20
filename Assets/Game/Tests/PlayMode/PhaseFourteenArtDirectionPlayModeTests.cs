using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Wayroot.Tests.PlayMode
{
    public sealed class PhaseFourteenArtDirectionPlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_ComposesWarmStylizedReadableSilhouettes()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(GameObject.Find("Prototype Player/Player Cloak"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype Player/Player Lantern"), Is.Not.Null);
            Assert.That(GameObject.Find("Friendly Mossling (hold E)/Mossling Face"), Is.Not.Null);
            Assert.That(GameObject.Find("Practice Slime (hold SPACE)/Slime Crown"), Is.Not.Null);
            Assert.That(GameObject.Find("Sunmeadow Creek/River Lily 1"), Is.Not.Null);
            Assert.That(GameObject.Find("Iron Edge Merchant Station (hold E)/Merchant Awning"), Is.Not.Null);
            Assert.That(GameObject.Find("Dormant Wayroot (hold E)/Wayroot Petal Crown"), Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator BootstrapScene_UsesScreenHorizontalMobileReadableWorldLabels()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            TextMesh label = GameObject.Find("World Label: WILDFLOWER PETAL").GetComponent<TextMesh>();
            Assert.That(label.characterSize, Is.LessThanOrEqualTo(0.075f));
            Assert.That(label.fontSize, Is.LessThanOrEqualTo(44));
            Assert.That(label.text, Is.EqualTo("WILDFLOWER\nPETAL"));
        }
    }
}

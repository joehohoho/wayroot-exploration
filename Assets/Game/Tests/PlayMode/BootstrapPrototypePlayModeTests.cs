using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Wayroot.Tests.PlayMode
{
    public sealed class BootstrapPrototypePlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_CreatesTheControlledPrototypeComposition()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(GameObject.Find("Prototype Player"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype Camera"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype HUD"), Is.Not.Null);
            Assert.That(GameObject.Find("Fadeable Test Tree"), Is.Not.Null);
            Assert.That(GameObject.Find("Iron Edge Merchant Station (hold E)"), Is.Not.Null);
        }
    }
}

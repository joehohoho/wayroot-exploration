using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Wayroot.Tests.PlayMode
{
    public sealed class MobilePolishPlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_ComposesMobileControlsFeedbackAndAllCriticalWorldIdentifiers()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(GameObject.Find("Wayroot Title Card"), Is.Not.Null);
            Assert.That(GameObject.Find("Movement Joystick"), Is.Not.Null);
            Assert.That(GameObject.Find("Gather Button"), Is.Not.Null);
            Assert.That(GameObject.Find("Attack Button"), Is.Not.Null);
            Assert.That(GameObject.Find("Action Feedback Card"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: WILDFLOWER PETAL"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: YOUNG TREE TIMBER"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: STONE OUTCROP STONE"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: MERCHANT IRON EDGE"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: SHELTER BUILD PLOT"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: SHELTER BUILD PLOT").GetComponent<TextMesh>(), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: MOSSling COMPANION"), Is.Not.Null);
            Assert.That(GameObject.Find("World Label: SLIME HOLD ATTACK"), Is.Not.Null);
        }
    }
}

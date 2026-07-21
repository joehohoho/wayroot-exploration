using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.UI;

namespace Wayroot.Tests.PlayMode
{
    public sealed class AlphaPresentationPlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_ComposesCompactHorizontalMarkersAndSeparatedHudCards()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(GameObject.Find("Development Overlay"), Is.Null);
            Assert.That(GameObject.Find("Combat Status Card"), Is.Not.Null);
            Assert.That(GameObject.Find("Resource Progression Card"), Is.Not.Null);
            Assert.That(GameObject.Find("Journey Guidance Card"), Is.Not.Null);
            Assert.That(GameObject.Find("Contextual Action Prompt"), Is.Not.Null);

            WorldIdentifier[] markers = Object.FindObjectsByType<WorldIdentifier>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            Assert.That(markers.Length, Is.GreaterThanOrEqualTo(7));
            foreach (WorldIdentifier marker in markers)
            {
                TextMesh text = marker.GetComponent<TextMesh>();
                Assert.That(text.text, Does.Not.Contain("\n"));
                Assert.That(text.characterSize, Is.LessThanOrEqualTo(0.035f));
                Assert.That(Vector3.Dot(marker.transform.up, UnityEngine.Camera.main.transform.up), Is.GreaterThan(0.99f));
            }

            RectTransform combat = GameObject.Find("Combat Status Card").GetComponent<RectTransform>();
            RectTransform resources = GameObject.Find("Resource Progression Card").GetComponent<RectTransform>();
            Assert.That(combat.anchoredPosition.x, Is.LessThan(100f));
            Assert.That(resources.anchoredPosition.x, Is.LessThan(0f));
        }
    }
}

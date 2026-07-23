using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Art;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class PhaseThirtyOneAmbiencePlayModeTests
    {
        [UnityTest]
        public IEnumerator Bootstrap_ComposesUnscaledVisualOnlyEnvironmentalAmbience()
        {
            PrototypeGatheringSaveService.Reset();
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject ambienceObject = GameObject.Find("Phase 31 Environmental Ambience");
            EnvironmentalAmbiencePresentation ambience = ambienceObject.GetComponent<EnvironmentalAmbiencePresentation>();
            Assert.That(ambience, Is.Not.Null);
            Assert.That(ambienceObject.transform.lossyScale, Is.EqualTo(Vector3.one));
            Assert.That(ambience.VisualCount, Is.GreaterThanOrEqualTo(7));
            Assert.That(GameObject.Find("Phase 31 Creek Ripple"), Is.Not.Null);
            Assert.That(GameObject.Find("Phase 31 Meadow Sway Leaf"), Is.Not.Null);
            Assert.That(GameObject.Find("Phase 31 Ambient Mote"), Is.Not.Null);
            Assert.That(ambienceObject.GetComponentsInChildren<Collider>(true), Is.Empty);
            Assert.That(GameObject.Find("Prototype Player").transform.position, Is.EqualTo(new Vector3(0f, 1f, 0f)));

            PrototypeGatheringSaveService.Reset();
        }
    }
}

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Building;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class ShelterCozyArtPlayModeTests
    {
        [UnityTest]
        public IEnumerator FreshRoute_ComposesAnUnbuiltBlueprintWithoutTheCompletedHome()
        {
            PrototypeGatheringSaveService.Reset();
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            Assert.That(GameObject.Find("Shelter Build Plot (hold E)"), Is.Not.Null);
            Assert.That(GameObject.Find("Unbuilt Shelter Blueprint Frame"), Is.Not.Null);
            Assert.That(GameObject.Find("Unbuilt Shelter Material Markers"), Is.Not.Null);
            Assert.That(GameObject.Find("Built Shelter"), Is.Null);
            Assert.That(GameObject.Find("Shelter Cozy Presentation").GetComponent<ShelterCozyPresentation>(), Is.Not.Null);

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator BuiltRoute_ComposesCozyHomeDressingAndCompactRestFeedbackWithoutColliders()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { shelterBuilt = true, activeShelterReturnPoint = true });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject shelter = GameObject.Find("Built Shelter");
            ShelterCozyPresentation presentation = GameObject.Find("Shelter Cozy Presentation").GetComponent<ShelterCozyPresentation>();
            Assert.That(shelter.activeSelf, Is.True);
            Assert.That(GameObject.Find("Shelter Lantern Glow"), Is.Not.Null);
            Assert.That(GameObject.Find("Shelter Hearth Glow"), Is.Not.Null);
            Assert.That(GameObject.Find("Shelter Garden Patch"), Is.Not.Null);
            Assert.That(GameObject.Find("Shelter Welcome Path"), Is.Not.Null);
            Assert.That(GameObject.Find("Shelter Lantern Glow").GetComponent<Collider>(), Is.Null);
            Assert.That(GameObject.Find("Shelter Garden Patch").GetComponentsInChildren<Collider>(true), Is.Empty);

            presentation.PlayRestFeedback();
            yield return null;
            Assert.That(presentation.IsRestPulseShowing, Is.True);
            yield return new WaitForSeconds(ShelterCozyArtRules.RestPulseSeconds + 0.08f);
            Assert.That(presentation.IsRestPulseShowing, Is.False);

            PrototypeGatheringSaveService.Reset();
        }
    }
}

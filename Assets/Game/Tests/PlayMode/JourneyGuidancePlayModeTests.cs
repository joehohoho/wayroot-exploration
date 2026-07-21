using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Gathering;
using Wayroot.Guidance;

namespace Wayroot.Tests.PlayMode
{
    public sealed class JourneyGuidancePlayModeTests
    {
        [UnityTest]
        public IEnumerator SaveDerivedJourney_SelectsExistingMilestonesAcrossRestart()
        {
            yield return LoadWith(new PrototypeGatheringSave());
            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Resource));

            yield return LoadWith(new PrototypeGatheringSave { petals = 1, slimeCores = 1 });
            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Merchant));

            yield return LoadWith(new PrototypeGatheringSave { weaponLevel = 1 });
            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Shelter));

            yield return LoadWith(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true });
            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Wayroot));

            yield return LoadWith(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, wayrootRestored = true });
            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Guardian));

            yield return LoadWith(new PrototypeGatheringSave { weaponLevel = 1, shelterBuilt = true, wayrootRestored = true, moonlitGladeUnlocked = true });
            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Bloomwell));

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator BloomwellCompletion_DisablesPointerAndResetRestartsTheJourney()
        {
            yield return LoadWith(new PrototypeGatheringSave { bloomwellRestored = true });
            JourneyGuidanceController completeGuide = Guide();
            Assert.That(completeGuide.CurrentState.Target, Is.EqualTo(JourneyTarget.None));
            Assert.That(GameObject.Find("Journey Firefly Pointer"), Is.Null);

            PrototypeGatheringSaveService.Reset();
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            Assert.That(Guide().CurrentState.Target, Is.EqualTo(JourneyTarget.Resource));
            Assert.That(Guide().CurrentState.HasPointer, Is.True);
            PrototypeGatheringSaveService.Reset();
        }

        private static IEnumerator LoadWith(PrototypeGatheringSave save)
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(save);
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;
        }

        private static JourneyGuidanceController Guide() => GameObject.Find("Journey Guidance Card").GetComponent<JourneyGuidanceController>();
    }
}

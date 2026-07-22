using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class PhaseTwentyFiveCompositionPlayModeTests
    {
        [UnityTest]
        public IEnumerator FullyOpenedRoute_ComposesTheFourDistinctExistingDestinationDressingsWithoutColliders()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave
            {
                wayrootRestored = true,
                moonlitGladeUnlocked = true,
                bloomwellRestored = true
            });

            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            AssertVisualOnly("Phase 25 Sunmeadow Warm Edge Dressing");
            AssertVisualOnly("Phase 25 Restored Grove Shelter Dressing");
            AssertVisualOnly("Phase 25 Moonlit Canopy Dressing");
            AssertVisualOnly("Phase 25 Bloomwell Landmark Silhouette");
            Assert.That(GameObject.Find("Bloomwell Crescent Silhouette"), Is.Not.Null);
            Assert.That(GameObject.Find("Thorn Guardian (hold ATTACK)/Thorn Guardian Sprite Rig"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype Player/Blue Cloak Explorer Sprite Rig"), Is.Not.Null);

            PrototypeGatheringSaveService.Reset();
        }

        private static void AssertVisualOnly(string name)
        {
            GameObject dressing = GameObject.Find(name);
            Assert.That(dressing, Is.Not.Null, $"Missing Phase 25 composition root: {name}");
            Assert.That(dressing.GetComponentsInChildren<Collider>(true), Is.Empty, $"{name} must not block the existing route or interactions.");
        }
    }
}

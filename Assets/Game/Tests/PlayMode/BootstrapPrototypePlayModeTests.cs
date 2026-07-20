using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Gathering;

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
            Assert.That(GameObject.Find("Shelter Build Plot (hold E)"), Is.Not.Null);
            Assert.That(GameObject.Find("Friendly Mossling (hold E)"), Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator ShelterReturnPoint_PersistsAcrossRestartRespawnsAtShelterAndResetClearsIt()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { shelterBuilt = true, activeShelterReturnPoint = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject player = GameObject.Find("Prototype Player");
            GameObject shelter = GameObject.Find("Built Shelter");
            global::Wayroot.Combat.PrototypePlayerHealth health = player.GetComponent<global::Wayroot.Combat.PrototypePlayerHealth>();
            Assert.That(shelter.activeSelf, Is.True);
            Assert.That(health.HasActiveShelterReturnPoint, Is.True);

            Vector3 expectedReturnPoint = shelter.transform.position + new Vector3(0f, 1f, -1.7f);
            health.TakeDamage(10);
            Assert.That(Vector3.Distance(player.transform.position, expectedReturnPoint), Is.LessThan(0.001f));
            Assert.That(health.Health, Is.EqualTo(10));
            Assert.That(GameObject.Find("World Label: SHELTER BUILD PLOT").GetComponent<TextMesh>().text, Is.EqualTo("SHELTER\nACTIVE HOME"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().activeShelterReturnPoint, Is.False);
        }

        [UnityTest]
        public IEnumerator DefaultRespawn_RemainsAtInitialSpawnWithoutAnActiveShelterReturnPoint()
        {
            PrototypeGatheringSaveService.Reset();
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject player = GameObject.Find("Prototype Player");
            global::Wayroot.Combat.PrototypePlayerHealth health = player.GetComponent<global::Wayroot.Combat.PrototypePlayerHealth>();
            health.TakeDamage(10);

            Assert.That(health.HasActiveShelterReturnPoint, Is.False);
            Assert.That(Vector3.Distance(player.transform.position, new Vector3(0f, 1f, 0f)), Is.LessThan(0.001f));
        }

        [UnityTest]
        public IEnumerator WayrootRestoration_PersistsItsClearingBloomAcrossSceneRestartAndResetClearsIt()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject wayroot = GameObject.Find("Dormant Wayroot (hold E)");
            GameObject bloom = GameObject.Find("Restored Wayroot Bloom");
            Assert.That(wayroot, Is.Not.Null);
            Assert.That(bloom, Is.Not.Null);
            Assert.That(bloom.activeSelf, Is.True);
            Assert.That(GameObject.Find("Wayroot World Label").GetComponent<TextMesh>().text, Is.EqualTo("WAYROOT\nRESTORED"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().wayrootRestored, Is.False);
        }

        [UnityTest]
        public IEnumerator RenewalDeadline_RestoresTheSavedNodeAndResetClearsRenewalState()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave
            {
                wayrootRestored = true,
                renewalNodes = new() { new RenewalNodeSave { nodeId = "wildflower-01", renewalDeadlineUtcTicks = System.DateTime.UtcNow.AddSeconds(-1).Ticks } }
            });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GatheringNode flower = GameObject.Find("Wildflower (hold E)").GetComponent<GatheringNode>();
            Assert.That(flower.IsAvailable, Is.True);
            Assert.That(GameObject.Find("World Label: WILDFLOWER PETAL").GetComponent<TextMesh>().text, Is.EqualTo("WILDFLOWER\nPETAL"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().renewalNodes, Is.Empty);
        }

        [UnityTest]
        public IEnumerator BefriendedCreature_RestoresAtTheShelterAndResetClearsItsState()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { creatureBefriended = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject creature = GameObject.Find("Friendly Mossling (hold E)");
            Assert.That(creature, Is.Not.Null);
            Assert.That(Vector3.Distance(creature.transform.position, new Vector3(-4.1f, 0.6f, -4.1f)), Is.LessThan(0.001f));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().creatureBefriended, Is.False);
        }
    }
}

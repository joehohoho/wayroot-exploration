using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Exploration;
using Wayroot.Gathering;
using Wayroot.Inventory;

namespace Wayroot.Tests.PlayMode
{
    public sealed class MoonlitGladePlayModeTests
    {
        [UnityTest]
        public IEnumerator Glade_IsSealedBeforeGuardianVictoryAndDoesNotRegisterItsNodes()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            MoonlitGladeController glade = GameObject.Find("Moonlit Glade Controller").GetComponent<MoonlitGladeController>();
            PrototypeGatheringController gathering = GameObject.Find("Prototype Gathering").GetComponent<PrototypeGatheringController>();
            Assert.That(glade.IsOpen, Is.False);
            Assert.That(GameObject.Find("Moonlit Glade Sealed Path"), Is.Not.Null);
            Assert.That(GameObject.Find("Moonlit Glade"), Is.Null);
            Assert.That(gathering.Nodes, Has.Count.EqualTo(3));

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator GuardianVictory_OpensAndRecordsTheMoonlitPathOnce()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject.Find("Thorn Guardian (hold ATTACK)").GetComponent<global::Wayroot.Combat.PrototypeEnemy>().TakeDamage(99);
            yield return null;

            Assert.That(GameObject.Find("Moonlit Glade Controller").GetComponent<MoonlitGladeController>().IsOpen, Is.True);
            Assert.That(PrototypeGatheringSaveService.Load().moonlitGladeUnlocked, Is.True);
            Assert.That(GameObject.Find("Moonlit Glade"), Is.Not.Null);
            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator GladeUnlock_PersistsItsExistingResourceNodesAcrossRestartAndResetSealsIt()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave
            {
                wayrootRestored = true,
                moonlitGladeUnlocked = true,
                renewalNodes = new() { new RenewalNodeSave { nodeId = "moonlit-wildflower-01", renewalDeadlineUtcTicks = System.DateTime.UtcNow.AddSeconds(-1).Ticks } }
            });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            MoonlitGladeController glade = GameObject.Find("Moonlit Glade Controller").GetComponent<MoonlitGladeController>();
            PrototypeGatheringController gathering = GameObject.Find("Prototype Gathering").GetComponent<PrototypeGatheringController>();
            Assert.That(glade.IsOpen, Is.True);
            Assert.That(GameObject.Find("Moonlit Glade"), Is.Not.Null);
            Assert.That(GameObject.Find("Moonlit Bloomwell Discovery"), Is.Not.Null);
            Assert.That(gathering.Nodes, Has.Count.EqualTo(6));
            Assert.That(GameObject.Find("Moonlit Wild Petal (hold E)").GetComponent<GatheringNode>().Resource, Is.EqualTo(ResourceType.WildPetal));
            Assert.That(GameObject.Find("Moonlit Sapling (hold E)").GetComponent<GatheringNode>().Resource, Is.EqualTo(ResourceType.Timber));
            Assert.That(GameObject.Find("Moonlit Stone (hold E)").GetComponent<GatheringNode>().Resource, Is.EqualTo(ResourceType.Stone));
            Assert.That(GameObject.Find("Moonlit Wild Petal (hold E)").GetComponent<GatheringNode>().IsAvailable, Is.True);

            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;
            Assert.That(GameObject.Find("Moonlit Glade Controller").GetComponent<MoonlitGladeController>().IsOpen, Is.True);

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().moonlitGladeUnlocked, Is.False);
        }
    }
}

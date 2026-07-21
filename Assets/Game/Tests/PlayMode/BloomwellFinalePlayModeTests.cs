using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Exploration;
using Wayroot.Gathering;
using Wayroot.Input;

namespace Wayroot.Tests.PlayMode
{
    public sealed class BloomwellFinalePlayModeTests
    {
        [UnityTest]
        public IEnumerator FullExistingLoop_RestoresWayrootDefeatsGuardianAndActivatesBloomwellPersistently()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave
            {
                petals = 5,
                timber = 5,
                stone = 5,
                slimeCores = 2,
                weaponLevel = 1,
                shelterBuilt = true
            });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            PrototypeInputReader input = GameObject.Find("Prototype Input").GetComponent<PrototypeInputReader>();
            GameObject player = GameObject.Find("Prototype Player");
            player.transform.position = GameObject.Find("Dormant Wayroot (hold E)").transform.position;
            input.SetVirtualInteract(true);
            yield return null;
            input.SetVirtualInteract(false);
            yield return null;

            Assert.That(PrototypeGatheringSaveService.Load().wayrootRestored, Is.True);
            GameObject.Find("Thorn Guardian (hold ATTACK)").GetComponent<global::Wayroot.Combat.PrototypeEnemy>().TakeDamage(99);
            yield return null;
            Assert.That(GameObject.Find("Moonlit Glade Controller").GetComponent<MoonlitGladeController>().IsOpen, Is.True);

            GameObject bloomwellObject = GameObject.Find("Moonlit Bloomwell Discovery");
            player.transform.position = bloomwellObject.transform.position;
            input.SetVirtualInteract(true);
            yield return null;
            input.SetVirtualInteract(false);
            yield return null;

            PrototypeGatheringController gathering = GameObject.Find("Prototype Gathering").GetComponent<PrototypeGatheringController>();
            Assert.That(gathering.BloomwellRestored, Is.True);
            Assert.That(PrototypeGatheringSaveService.Load().bloomwellRestored, Is.True);
            Assert.That(GameObject.Find("Bloomwell Restored Finale Bloom").activeSelf, Is.True);
            Assert.That(GameObject.Find("Sunmeadow Bloomwell Finale Motif").activeSelf, Is.True);
            Assert.That(GameObject.Find("Friendly Mossling (hold E)").GetComponent<global::Wayroot.Creatures.PrototypeCreatureController>().IsCelebratingFinale, Is.True);

            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;
            Assert.That(GameObject.Find("Moonlit Bloomwell Discovery").GetComponent<BloomwellController>().Status, Does.Contain("RESTORED"));
            Assert.That(GameObject.Find("Bloomwell Restored Finale Bloom").activeSelf, Is.True);
            Assert.That(GameObject.Find("Sunmeadow Bloomwell Finale Motif").activeSelf, Is.True);

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().bloomwellRestored, Is.False);
        }

        [UnityTest]
        public IEnumerator BloomwellIncompleteState_NamesMissingExistingRequirements()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true, moonlitGladeUnlocked = true });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            PrototypeGatheringController gathering = GameObject.Find("Prototype Gathering").GetComponent<PrototypeGatheringController>();
            Assert.That(gathering.TryRestoreBloomwell(out string status), Is.False);
            Assert.That(status, Does.Contain("PETAL"));
            Assert.That(status, Does.Contain("TIMBER"));
            Assert.That(status, Does.Contain("STONE"));
            Assert.That(status, Does.Contain("CORE"));
            Assert.That(GameObject.Find("Bloomwell Finale Presentation").GetComponent<BloomwellFinalePresentation>().IsApplied, Is.False);

            PrototypeGatheringSaveService.Reset();
        }
    }
}

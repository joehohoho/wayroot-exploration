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

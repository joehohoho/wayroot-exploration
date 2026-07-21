using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Character;
using Wayroot.Combat;
using Wayroot.Input;
using Wayroot.Presentation;

namespace Wayroot.Tests.PlayMode
{
    public sealed class ActorSpritePresentationPlayModeTests
    {
        [UnityTest]
        public IEnumerator Bootstrap_ComposesOriginalBillboardSpriteRigsWithoutPrimaryPrimitiveActors()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;
            yield return null;

            ActorSpritePresentation playerRig = GameObject.Find("Blue Cloak Explorer Sprite Rig").GetComponent<ActorSpritePresentation>();
            ActorSpritePresentation slimeRig = GameObject.Find("Practice Slime Sprite Rig").GetComponent<ActorSpritePresentation>();
            ActorSpritePresentation guardianRig = FindRig("Thorn Guardian Sprite Rig");
            Assert.That(playerRig, Is.Not.Null);
            Assert.That(slimeRig, Is.Not.Null);
            Assert.That(guardianRig, Is.Not.Null);
            Assert.That(playerRig.GetComponent<SpriteRenderer>().sprite, Is.Not.Null);
            Assert.That(slimeRig.GetComponent<SpriteRenderer>().sprite, Is.Not.Null);
            Assert.That(guardianRig.GetComponent<SpriteRenderer>().sprite, Is.Not.Null);
            Assert.That(slimeRig.IsEnemyRig, Is.True);
            Assert.That(guardianRig.IsEnemyRig, Is.True);
            Assert.That(GameObject.Find("Prototype Player").GetComponent<Renderer>().enabled, Is.False);
            Assert.That(GameObject.Find("Practice Slime (hold SPACE)").GetComponent<Renderer>().enabled, Is.False);
        }

        [UnityTest]
        public IEnumerator SpriteRig_RespondsToSemanticDodgeAndEnemyHitActions()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            PrototypePlayerController player = GameObject.Find("Prototype Player").GetComponent<PrototypePlayerController>();
            GameObject.Find("Prototype Input").GetComponent<PrototypeInputReader>().RequestVirtualDodge();
            yield return null;
            Assert.That(player.IsDodging, Is.True);
            yield return null;
            Assert.That(GameObject.Find("Blue Cloak Explorer Sprite Rig").GetComponent<ActorSpritePresentation>().CurrentActionName, Is.EqualTo("Dodge"));

            PrototypeEnemy slime = GameObject.Find("Practice Slime (hold SPACE)").GetComponent<PrototypeEnemy>();
            slime.TakeDamage(1);
            yield return null;
            Assert.That(GameObject.Find("Practice Slime Sprite Rig").GetComponent<ActorSpritePresentation>().CurrentActionName, Is.EqualTo("Hit"));
        }
        private static ActorSpritePresentation FindRig(string name)
        {
            foreach (ActorSpritePresentation rig in Object.FindObjectsByType<ActorSpritePresentation>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (rig.gameObject.name == name) return rig;
            }
            return null;
        }
    }
}

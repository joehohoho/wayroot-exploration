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
            AssertRigPolish(playerRig, GameObject.Find("Prototype Player").transform);
            AssertRigPolish(slimeRig, GameObject.Find("Practice Slime (hold SPACE)").transform);
            AssertRigPolish(guardianRig, guardianRig.transform.parent);
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
        private static void AssertRigPolish(ActorSpritePresentation rig, Transform actorRoot)
        {
            SpriteRenderer renderer = rig.GetComponent<SpriteRenderer>();
            Assert.That(renderer.enabled, Is.True);
            Assert.That(renderer.sharedMaterial, Is.Not.Null, "Actor sprites require the source-controlled transparent URP material in player builds.");
            Assert.That(renderer.sharedMaterial.mainTexture, Is.Not.Null);
            Assert.That(renderer.sprite.texture.GetPixel(0, 0).a, Is.EqualTo(0f), "The sprite's clear pixels must remain transparent.");
            Assert.That(renderer.color.a, Is.EqualTo(1f));
            Assert.That(rig.transform.localScale.x, Is.GreaterThan(0.8f).And.LessThan(1.4f));
            Assert.That(renderer.sortingOrder, Is.GreaterThanOrEqualTo(28));
            Assert.That(ActorSpriteVisualMasker.CountVisibleActorBodyRenderers(actorRoot), Is.GreaterThanOrEqualTo(1), "Actor keeps a readable fallback silhouette while the sprite presentation is active.");
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

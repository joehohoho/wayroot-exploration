using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Art;
using Wayroot.Combat;

namespace Wayroot.Tests.PlayMode
{
    public sealed class PhaseEighteenArtAnimationPlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_ComposesProceduralMotionWithoutMovingGameplayRoots()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject player = GameObject.Find("Prototype Player");
            GameObject mossling = GameObject.Find("Friendly Mossling (hold E)");
            GameObject slime = GameObject.Find("Practice Slime (hold SPACE)");
            GameObject creek = GameObject.Find("Sunmeadow Creek");
            Assert.That(player.GetComponent<ProceduralStylizedAnimator>().Style, Is.EqualTo(ProceduralStylizedAnimator.MotionStyle.Player));
            Assert.That(mossling.GetComponent<ProceduralStylizedAnimator>().Style, Is.EqualTo(ProceduralStylizedAnimator.MotionStyle.Mossling));
            Assert.That(slime.GetComponent<ProceduralStylizedAnimator>().Style, Is.EqualTo(ProceduralStylizedAnimator.MotionStyle.Slime));
            Assert.That(creek.GetComponent<ProceduralStylizedAnimator>().Style, Is.EqualTo(ProceduralStylizedAnimator.MotionStyle.Water));
            Assert.That(GameObject.Find("Prototype Player/Player Lantern Glow"), Is.Not.Null);
            Assert.That(GameObject.Find("Practice Slime (hold SPACE)/Slime Animated Shell"), Is.Not.Null);

            Vector3 playerRoot = player.transform.position;
            Vector3 slimeScale = slime.transform.localScale;
            Vector3 cloakBefore = player.transform.Find("Player Cloak").localPosition;
            yield return new WaitForSeconds(0.18f);

            Assert.That(player.transform.position, Is.EqualTo(playerRoot));
            Assert.That(slime.transform.localScale, Is.EqualTo(slimeScale));
            Assert.That(player.transform.Find("Player Cloak").localPosition, Is.Not.EqualTo(cloakBefore));
        }

        [UnityTest]
        public IEnumerator BootstrapScene_RestoreSafeCompositionKeepsAnimatedEnemyColliderAndWorldLabel()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject slime = GameObject.Find("Practice Slime (hold SPACE)");
            PrototypeEnemy enemy = slime.GetComponent<PrototypeEnemy>();
            Collider collider = slime.GetComponent<Collider>();
            Assert.That(collider.enabled, Is.True);
            enemy.TakeDamage(1);
            yield return null;

            Assert.That(collider.enabled, Is.True);
            Assert.That(GameObject.Find("World Label: WILDFLOWER PETAL").GetComponent<TextMesh>().text, Is.EqualTo("WILDFLOWER\nPETAL"));
            Assert.That(GameObject.Find("Prototype Player").GetComponents<ProceduralStylizedAnimator>(), Has.Length.EqualTo(1));
        }
    }
}

using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Combat;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class CombatEncounterPolishPlayModeTests
    {
        [UnityTest]
        public IEnumerator OpenGrove_ComposesCompactVisualOnlyCombatPolishForBothEnemies()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            AssertPresentation("Practice Slime (hold SPACE)", false);
            AssertPresentation("Thorn Guardian (hold ATTACK)", true);
            Assert.That(GameObject.Find("Phase 26 Thorn Guardian Arena Focus"), Is.Not.Null);
            Assert.That(GameObject.Find("Guardian Arena Boundary North"), Is.Not.Null);
            Assert.That(GameObject.Find("Guardian Threat Focus"), Is.Not.Null);
            Assert.That(GameObject.Find("Phase 26 Thorn Guardian Arena Focus").GetComponentsInChildren<Collider>(true), Is.Empty);

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator ContactPresentation_PulsesWithoutMovingGameplayRootsOrAddingColliders()
        {
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject playerObject = GameObject.Find("Prototype Player");
            GameObject slimeObject = GameObject.Find("Practice Slime (hold SPACE)");
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 slimePosition = slimeObject.transform.position;
            PlayerAttackPresentation playerPresentation = playerObject.GetComponent<PlayerAttackPresentation>();
            Transform contactMarker = playerObject.transform.Find("Player Attack Contact Marker");
            PrototypeEnemy slime = slimeObject.GetComponent<PrototypeEnemy>();
            slimeObject.GetComponent<PrototypeEnemyChase>().enabled = false;

            playerPresentation.PlayContact(slimePosition, false);
            slime.TakeDamage(1);
            yield return null;

            Assert.That(playerPresentation.IsShowingTrail, Is.True);
            Assert.That(GameObject.Find("Player Attack Swing Trail").activeSelf, Is.True);
            Assert.That(GameObject.Find("Player Attack Impact Flash").activeSelf, Is.True);
            Assert.That(GameObject.Find("SLIME Hit Flash").activeSelf, Is.True);
            Assert.That(playerObject.transform.position, Is.EqualTo(playerPosition));
            Assert.That(slimeObject.transform.position, Is.EqualTo(slimePosition));
            Assert.That(GameObject.Find("Player Attack Swing Trail").GetComponent<Collider>(), Is.Null);

            yield return new WaitForSeconds(CombatEncounterPolishRules.PlayerContactMarkerSeconds + 0.08f);
            Assert.That(contactMarker.gameObject.activeSelf, Is.False);
        }

        private static void AssertPresentation(string enemyName, bool guardian)
        {
            GameObject enemyObject = GameObject.Find(enemyName);
            EnemyEncounterPresentation presentation = enemyObject.GetComponent<EnemyEncounterPresentation>();
            Assert.That(presentation, Is.Not.Null);
            Assert.That(presentation.IsGuardian, Is.EqualTo(guardian));
            Assert.That(enemyObject.transform.Find($"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Anticipation Ground Cue"), Is.Not.Null);
            Assert.That(enemyObject.transform.Find($"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Anticipation Arc Cue"), Is.Not.Null);
            Assert.That(enemyObject.transform.Find($"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Hit Flash"), Is.Not.Null);
            Assert.That(enemyObject.transform.Find($"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Defeat Marker"), Is.Not.Null);
            Assert.That(enemyObject.transform.Find($"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Respawn Marker"), Is.Not.Null);
            AssertCueVisualOnly(enemyObject, $"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Anticipation Ground Cue");
            AssertCueVisualOnly(enemyObject, $"{enemyObject.GetComponent<PrototypeEnemy>().DisplayName} Hit Flash");
        }

        private static void AssertCueVisualOnly(GameObject enemyObject, string cueName)
        {
            Transform cue = enemyObject.transform.Find(cueName);
            Assert.That(cue.GetComponent<Collider>(), Is.Null, $"{cueName} must not affect combat collisions.");
        }
    }
}

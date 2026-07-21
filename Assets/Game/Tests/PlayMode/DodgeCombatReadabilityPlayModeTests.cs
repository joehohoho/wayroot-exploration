using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Character;
using Wayroot.Combat;
using Wayroot.Input;

namespace Wayroot.Tests.PlayMode
{
    public sealed class DodgeCombatReadabilityPlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_ComposesOneSemanticDodgeActionAndSafeAreaButton()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            PrototypeInputReader input = GameObject.Find("Prototype Input").GetComponent<PrototypeInputReader>();
            Assert.That(input.HasDodgeBinding("<Keyboard>/leftShift"), Is.True);
            Assert.That(input.HasDodgeBinding("<Gamepad>/buttonEast"), Is.True);
            Assert.That(GameObject.Find("Dodge Button"), Is.Not.Null);
            Assert.That(GameObject.Find("Dodge Label").GetComponent<UnityEngine.UI.Text>().text, Does.StartWith("DODGE"));

            input.RequestVirtualDodge();
            Assert.That(input.ConsumeDodgeRequested(), Is.True);
            Assert.That(input.ConsumeDodgeRequested(), Is.False);
        }

        [UnityTest]
        public IEnumerator Dodge_UsesVirtualSemanticInputAndProtectsOnlyItsBriefWindow()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject playerObject = GameObject.Find("Prototype Player");
            PrototypePlayerController player = playerObject.GetComponent<PrototypePlayerController>();
            PrototypePlayerHealth health = playerObject.GetComponent<PrototypePlayerHealth>();
            PrototypeInputReader input = GameObject.Find("Prototype Input").GetComponent<PrototypeInputReader>();
            Vector3 before = playerObject.transform.position;

            input.RequestVirtualDodge();
            yield return null;

            Assert.That(player.IsDodging, Is.True);
            Assert.That(Vector3.Distance(playerObject.transform.position, before), Is.GreaterThan(0.01f));
            Assert.That(health.IsDodgeInvulnerable, Is.True);
            health.TakeDamage(1);
            Assert.That(health.Health, Is.EqualTo(10));

            yield return new WaitForSeconds(DodgeRules.ImmunitySeconds + 0.05f);
            health.TakeDamage(1);
            Assert.That(health.Health, Is.EqualTo(9));
        }

        [UnityTest]
        public IEnumerator SlimeAndGuardian_ExposeContactAnticipationBeforeDamage()
        {
            global::Wayroot.Gathering.PrototypeGatheringSaveService.Reset();
            global::Wayroot.Gathering.PrototypeGatheringSaveService.Save(new global::Wayroot.Gathering.PrototypeGatheringSave { wayrootRestored = true });
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(PrototypeEnemyChase.ContactAnticipationSeconds, Is.GreaterThan(0f));
            Assert.That(GameObject.Find("Practice Slime (hold SPACE)").transform.Find("Slime Attack Pulse"), Is.Not.Null);
            bool hasGuardianAnticipationVisual = false;
            foreach (global::Wayroot.Art.ProceduralStylizedAnimator animator in Object.FindObjectsByType<global::Wayroot.Art.ProceduralStylizedAnimator>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                hasGuardianAnticipationVisual |= animator.Style == global::Wayroot.Art.ProceduralStylizedAnimator.MotionStyle.Guardian;
            }
            Assert.That(hasGuardianAnticipationVisual, Is.True);

            global::Wayroot.Gathering.PrototypeGatheringSaveService.Reset();
        }
    }
}

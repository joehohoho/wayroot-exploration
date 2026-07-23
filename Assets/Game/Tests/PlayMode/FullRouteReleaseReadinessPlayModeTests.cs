using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Art;
using Wayroot.Gathering;
using Wayroot.Guidance;
using Wayroot.Input;
using Wayroot.UI;

namespace Wayroot.Tests.PlayMode
{
    /// <summary>Exercises the published route as real in-scene interactions from a fresh reset through Bloomwell completion.</summary>
    public sealed class FullRouteReleaseReadinessPlayModeTests
    {
        [UnityTest]
        public IEnumerator FreshRoute_ConnectsMerchantShelterWayrootGuardianGladeBloomwellAndReadinessSafeguards()
        {
            PrototypeGatheringSaveService.Reset();
            AccessibilityPreferences preferences = null!;
            try
            {
                // These are the existing route's finite costs: merchant (1P/1C), shelter (3T/3S),
                // Wayroot (3P/3T/3S/1C), then Bloomwell (2P/2T/2S/1C) after the Guardian reward.
                PrototypeGatheringSaveService.Save(new PrototypeGatheringSave
                {
                    petals = 6,
                    timber = 8,
                    stone = 8,
                    slimeCores = 2
                });

                yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
                yield return null;

                PrototypeGatheringController gathering = GameObject.Find("Prototype Gathering").GetComponent<PrototypeGatheringController>();
                preferences = GameObject.Find("Accessibility Preferences").GetComponent<AccessibilityPreferences>();

                Assert.That(GameObject.Find("Journey Guidance Card").GetComponent<JourneyGuidanceController>().CurrentState.Target, Is.EqualTo(JourneyTarget.Merchant));
                Assert.That(gathering.SoundEnabled, Is.True);
                Assert.That(preferences.ReducedFlash, Is.False);
                Assert.That(preferences.ReducedMotion, Is.False);

                gathering.BefriendCreature();
                Assert.That(gathering.CreatureBefriended, Is.True);

                Assert.That(gathering.TryPurchaseWeaponUpgrade(out _), Is.True);
                Assert.That(gathering.WeaponLevel, Is.EqualTo(1));

                Assert.That(gathering.TryBuildShelter(out _), Is.True);
                Assert.That(gathering.ShelterBuilt, Is.True);
                Assert.That(gathering.TryRestAtShelter(out _), Is.True);
                Assert.That(gathering.HasActiveShelterReturnPoint, Is.True);

                Assert.That(gathering.TryRestoreWayroot(out _), Is.True);
                yield return null;
                Assert.That(gathering.WayrootRestored, Is.True);
                Assert.That(GameObject.Find("Restored Grove Controller").GetComponent<global::Wayroot.Combat.RestoredGroveController>().IsOpen, Is.True);
                JourneyGuidanceController guide = GameObject.Find("Journey Guidance Card").GetComponent<JourneyGuidanceController>();
                guide.RefreshNow();
                Assert.That(guide.CurrentState.Target, Is.EqualTo(JourneyTarget.Guardian));

                GameObject guardianObject = GameObject.Find("Thorn Guardian (hold ATTACK)");
                guardianObject.GetComponent<global::Wayroot.Combat.PrototypeEnemy>().TakeDamage(99);
                yield return null;

                Assert.That(gathering.MoonlitGladeUnlocked, Is.True);
                Assert.That(GameObject.Find("Moonlit Glade Controller").GetComponent<global::Wayroot.Exploration.MoonlitGladeController>().IsOpen, Is.True);
                Assert.That(gathering.GetCount(global::Wayroot.Inventory.ResourceType.SlimeCore), Is.EqualTo(1));

                Assert.That(gathering.TryRestoreBloomwell(out string bloomwellStatus), Is.True, bloomwellStatus);
                Assert.That(gathering.BloomwellRestored, Is.True);
                guide = GameObject.Find("Journey Guidance Card").GetComponent<JourneyGuidanceController>();
                guide.RefreshNow();
                Assert.That(guide.CurrentState.Target, Is.EqualTo(JourneyTarget.None));
                Assert.That(GameObject.Find("Journey Firefly Pointer"), Is.Null);

                MobilePresentationSafeguard safeguard = GameObject.Find("Mobile Presentation Safeguard").GetComponent<MobilePresentationSafeguard>();
                for (int index = 0; index < MobilePresentationSafeguardRules.SlowFrameSamplesToReduce; index++)
                {
                    safeguard.ReportFrameDuration(MobilePresentationSafeguardRules.SlowFrameThresholdSeconds + 0.01f);
                }

                Assert.That(safeguard.State, Is.EqualTo(MobilePresentationSafeguardState.Reduced));
                Assert.That(GameObject.Find("Prototype Player").activeInHierarchy, Is.True);
                Assert.That(GameObject.Find("Prototype HUD").activeInHierarchy, Is.True);
                Assert.That(GameObject.Find("Movement Joystick").activeInHierarchy, Is.True);
                Assert.That(GameObject.Find("Attack Button").activeInHierarchy, Is.True);
                Assert.That(GameObject.Find("Dodge Button").activeInHierarchy, Is.True);
            }
            finally
            {
                preferences?.ResetDefaults();
                PrototypeGatheringSaveService.Reset();
            }
        }

        private static IEnumerator InteractAt(GameObject player, PrototypeInputReader input, Transform destination)
        {
            player.transform.position = destination.position;
            // Let every range-gated controller observe the relocated player before and during the virtual press.
            yield return null;
            yield return null;
            input.SetVirtualInteract(true);
            yield return null;
            yield return null;
            input.SetVirtualInteract(false);
            yield return null;
            yield return null;
        }
    }
}

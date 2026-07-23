using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Art;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class MobilePresentationSafeguardPlayModeTests
    {
        [UnityTest]
        public IEnumerator Bootstrap_SafeguardReducesDecorativeAmbienceWithoutHidingCoreRouteOrTouchUi()
        {
            PrototypeGatheringSaveService.Reset();
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            EnvironmentalAmbiencePresentation ambience = GameObject.Find("Phase 31 Environmental Ambience").GetComponent<EnvironmentalAmbiencePresentation>();
            MobilePresentationSafeguard safeguard = GameObject.Find("Mobile Presentation Safeguard").GetComponent<MobilePresentationSafeguard>();
            Assert.That(ambience.OptionalEffectsEnabled, Is.True);
            Assert.That(GameObject.Find("Movement Joystick").activeInHierarchy, Is.True);
            Assert.That(GameObject.Find("Prototype Player").activeInHierarchy, Is.True);
            Assert.That(GameObject.Find("Practice Slime (hold SPACE)").activeInHierarchy, Is.True);

            for (int index = 0; index < MobilePresentationSafeguardRules.SlowFrameSamplesToReduce; index++)
            {
                safeguard.ReportFrameDuration(MobilePresentationSafeguardRules.SlowFrameThresholdSeconds + 0.01f);
            }

            Assert.That(safeguard.State, Is.EqualTo(MobilePresentationSafeguardState.Reduced));
            Assert.That(ambience.OptionalEffectsEnabled, Is.False);
            Assert.That(GameObject.Find("Movement Joystick").activeInHierarchy, Is.True);
            Assert.That(GameObject.Find("Prototype Player").activeInHierarchy, Is.True);
            Assert.That(GameObject.Find("Practice Slime (hold SPACE)").activeInHierarchy, Is.True);
            PrototypeGatheringSaveService.Reset();
        }
    }
}

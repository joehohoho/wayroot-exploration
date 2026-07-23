using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Gathering;
using Wayroot.UI;

namespace Wayroot.Tests.PlayMode
{
    public sealed class AccessibilityPreferencesPlayModeTests
    {
        [UnityTest]
        public IEnumerator Bootstrap_ComposesPersistentComfortControlsAndResetDefaults()
        {
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            AccessibilityPreferences preferences = GameObject.Find("Accessibility Preferences").GetComponent<AccessibilityPreferences>();
            Assert.That(preferences, Is.Not.Null);
            Assert.That(GameObject.Find("Reduced Flash Toggle Button"), Is.Not.Null);
            Assert.That(GameObject.Find("Reduced Motion Toggle Button"), Is.Not.Null);
            Assert.That(GameObject.Find("Phase 31 Environmental Ambience"), Is.Not.Null);

            preferences.SetReducedFlash(true);
            preferences.SetReducedMotion(true);
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            preferences = GameObject.Find("Accessibility Preferences").GetComponent<AccessibilityPreferences>();
            Assert.That(preferences.ReducedFlash, Is.True);
            Assert.That(preferences.ReducedMotion, Is.True);

            preferences.ResetDefaults();
            Assert.That(preferences.ReducedFlash, Is.EqualTo(AccessibilityRules.DefaultReducedFlash));
            Assert.That(preferences.ReducedMotion, Is.EqualTo(AccessibilityRules.DefaultReducedMotion));
            PrototypeGatheringSaveService.Reset();
        }
    }
}

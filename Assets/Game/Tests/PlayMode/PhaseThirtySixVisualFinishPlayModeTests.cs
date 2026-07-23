using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Art;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class PhaseThirtySixVisualFinishPlayModeTests
    {
        [UnityTest]
        public IEnumerator Bootstrap_ComposesBoundedNonCollidingPublicPlaytestFinishAcrossRoute()
        {
            PrototypeGatheringSaveService.Reset();
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject finishObject = GameObject.Find("Phase 36 Public Playtest Visual Finish");
            PhaseThirtySixVisualFinish finish = finishObject.GetComponent<PhaseThirtySixVisualFinish>();
            Assert.That(finish, Is.Not.Null);
            Assert.That(finish.VisualCount, Is.GreaterThanOrEqualTo(PhaseThirtySixVisualFinishRules.MinimumVisualCount));
            Assert.That(finish.HasOnlyVisualColliders, Is.True);
            Assert.That(GameObject.Find("Sunmeadow Finish Dressing"), Is.Not.Null);
            Assert.That(GameObject.Find("Shelter Finish Dressing"), Is.Not.Null);
            Assert.That(GameObject.Find("Grove Finish Dressing"), Is.Not.Null);
            Assert.That(GameObject.Find("Moonlit Glade Finish Dressing"), Is.Not.Null);
            Assert.That(GameObject.Find("Bloomwell Finish Dressing"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype Player").transform.position, Is.EqualTo(new Vector3(0f, 1f, 0f)));

            PrototypeGatheringSaveService.Reset();
        }
    }
}

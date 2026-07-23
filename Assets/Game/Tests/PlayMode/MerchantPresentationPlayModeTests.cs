using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Gathering;
using Wayroot.Progression;

namespace Wayroot.Tests.PlayMode
{
    public sealed class MerchantPresentationPlayModeTests
    {
        [UnityTest]
        public IEnumerator Bootstrap_ComposesUnscaledVisualOnlyMerchantLandmarkAndUnequippedBlade()
        {
            PrototypeGatheringSaveService.Reset();
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject presentationObject = GameObject.Find("Iron Edge Merchant Presentation");
            MerchantPresentation presentation = presentationObject.GetComponent<MerchantPresentation>();
            Assert.That(presentation, Is.Not.Null);
            Assert.That(presentationObject.transform.lossyScale, Is.EqualTo(Vector3.one));
            Assert.That(presentation.VisualCount, Is.GreaterThanOrEqualTo(10));
            Assert.That(GameObject.Find("Iron Edge Stall Visual Root"), Is.Not.Null);
            Assert.That(GameObject.Find("Iron Edge Ember Lantern"), Is.Not.Null);
            Assert.That(GameObject.Find("Iron Edge Merchant Presentation").GetComponentsInChildren<Collider>(true), Is.Empty);
            Assert.That(presentationObject.transform.Find("Iron Edge Player Upgrade Visual").gameObject.activeSelf, Is.False);

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator SavedIronEdge_ComposesVisiblePlayerWeaponWithoutChangingPlayerRoot()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { weaponLevel = 1 });
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return null;

            GameObject player = GameObject.Find("Prototype Player");
            Vector3 playerPosition = player.transform.position;
            GameObject bladeRoot = GameObject.Find("Iron Edge Player Upgrade Visual");
            Assert.That(bladeRoot.activeSelf, Is.True);
            Assert.That(bladeRoot.transform.lossyScale, Is.EqualTo(Vector3.one));
            Assert.That(bladeRoot.GetComponentsInChildren<Collider>(true), Is.Empty);
            Assert.That(player.transform.position, Is.EqualTo(playerPosition));

            PrototypeGatheringSaveService.Reset();
        }
    }
}

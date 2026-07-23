using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Wayroot.Audio;
using Wayroot.Gathering;

namespace Wayroot.Tests.PlayMode
{
    public sealed class BootstrapPrototypePlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_CreatesTheControlledPrototypeComposition()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            Assert.That(GameObject.Find("Prototype Player"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype Camera"), Is.Not.Null);
            Assert.That(GameObject.Find("Prototype HUD"), Is.Not.Null);
            Assert.That(GameObject.Find("Iron Edge Merchant Station (hold E)"), Is.Not.Null);
            Assert.That(GameObject.Find("Shelter Build Plot (hold E)"), Is.Not.Null);
            Assert.That(GameObject.Find("Friendly Mossling (hold E)"), Is.Not.Null);
        }

        [UnityTest]
        public IEnumerator ShelterReturnPoint_PersistsAcrossRestartRespawnsAtShelterAndResetClearsIt()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { shelterBuilt = true, activeShelterReturnPoint = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject player = GameObject.Find("Prototype Player");
            GameObject shelter = GameObject.Find("Built Shelter");
            global::Wayroot.Combat.PrototypePlayerHealth health = player.GetComponent<global::Wayroot.Combat.PrototypePlayerHealth>();
            Assert.That(shelter.activeSelf, Is.True);
            Assert.That(health.HasActiveShelterReturnPoint, Is.True);

            Vector3 expectedReturnPoint = shelter.transform.position + new Vector3(0f, 1f, -1.7f);
            health.TakeDamage(10);
            Assert.That(Vector3.Distance(player.transform.position, expectedReturnPoint), Is.LessThan(0.001f));
            Assert.That(health.Health, Is.EqualTo(10));
            Assert.That(GameObject.Find("World Label: SHELTER BUILD PLOT").GetComponent<TextMesh>().text, Is.EqualTo("SHELTER  •  ACTIVE HOME"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().activeShelterReturnPoint, Is.False);
        }

        [UnityTest]
        public IEnumerator DefaultRespawn_RemainsAtInitialSpawnWithoutAnActiveShelterReturnPoint()
        {
            PrototypeGatheringSaveService.Reset();
            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject player = GameObject.Find("Prototype Player");
            global::Wayroot.Combat.PrototypePlayerHealth health = player.GetComponent<global::Wayroot.Combat.PrototypePlayerHealth>();
            health.TakeDamage(10);

            Assert.That(health.HasActiveShelterReturnPoint, Is.False);
            Assert.That(Vector3.Distance(player.transform.position, new Vector3(0f, 1f, 0f)), Is.LessThan(0.001f));
        }

        [UnityTest]
        public IEnumerator WayrootRestoration_PersistsItsClearingBloomAcrossSceneRestartAndResetClearsIt()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject wayroot = GameObject.Find("Dormant Wayroot (hold E)");
            GameObject bloom = GameObject.Find("Restored Wayroot Bloom");
            Assert.That(wayroot, Is.Not.Null);
            Assert.That(bloom, Is.Not.Null);
            Assert.That(bloom.activeSelf, Is.True);
            Assert.That(GameObject.Find("Wayroot World Label").GetComponent<TextMesh>().text, Is.EqualTo("WAYROOT  •  RESTORED"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().wayrootRestored, Is.False);
        }

        [UnityTest]
        public IEnumerator RestoredGrove_IsUnavailableBeforeWayrootAndComposesThornGuardianAfterRestoration()
        {
            PrototypeGatheringSaveService.Reset();
            AsyncOperation lockedOperation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return lockedOperation;
            yield return null;

            global::Wayroot.Combat.RestoredGroveController lockedGrove = GameObject.Find("Restored Grove Controller").GetComponent<global::Wayroot.Combat.RestoredGroveController>();
            Assert.That(lockedGrove.IsOpen, Is.False);
            Assert.That(GameObject.Find("Thorn Guardian (hold ATTACK)"), Is.Null);

            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { wayrootRestored = true });
            AsyncOperation restoredOperation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return restoredOperation;
            yield return null;

            global::Wayroot.Combat.RestoredGroveController restoredGrove = GameObject.Find("Restored Grove Controller").GetComponent<global::Wayroot.Combat.RestoredGroveController>();
            global::Wayroot.Combat.PrototypeEnemy guardian = GameObject.Find("Thorn Guardian (hold ATTACK)").GetComponent<global::Wayroot.Combat.PrototypeEnemy>();
            Assert.That(restoredGrove.IsOpen, Is.True);
            Assert.That(GameObject.Find("Restored Grove Edge"), Is.Not.Null);
            Assert.That(guardian.DisplayName, Is.EqualTo("THORN GUARDIAN"));
            Assert.That(guardian.MaxHealth, Is.EqualTo(8));

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator RenewalDeadline_RestoresTheSavedNodeAndResetClearsRenewalState()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave
            {
                wayrootRestored = true,
                renewalNodes = new() { new RenewalNodeSave { nodeId = "wildflower-01", renewalDeadlineUtcTicks = System.DateTime.UtcNow.AddSeconds(-1).Ticks } }
            });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GatheringNode flower = GameObject.Find("Wildflower (hold E)").GetComponent<GatheringNode>();
            Assert.That(flower.IsAvailable, Is.True);
            Assert.That(GameObject.Find("World Label: WILDFLOWER PETAL").GetComponent<TextMesh>().text, Is.EqualTo("WILDFLOWER  •  PETAL"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().renewalNodes, Is.Empty);
        }

        [UnityTest]
        public IEnumerator BefriendedCreature_RestoresAtTheShelterAndResetClearsItsState()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { creatureBefriended = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject creature = GameObject.Find("Friendly Mossling (hold E)");
            Assert.That(creature, Is.Not.Null);
            Assert.That(Vector3.Distance(creature.transform.position, new Vector3(-4.1f, 0.6f, -4.1f)), Is.LessThan(0.001f));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().creatureBefriended, Is.False);
        }

        [UnityTest]
        public IEnumerator BefriendedMossling_GuidesAvailableResourcesThenShowsRenewalStatusWithoutADepletedMarker()
        {
            PrototypeGatheringSaveService.Reset();
            PrototypeGatheringSaveService.Save(new PrototypeGatheringSave { creatureBefriended = true });

            AsyncOperation operation = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return operation;
            yield return null;

            GameObject creature = GameObject.Find("Friendly Mossling (hold E)");
            global::Wayroot.Creatures.MosslingResourceGuide guide = creature.GetComponent<global::Wayroot.Creatures.MosslingResourceGuide>();
            GameObject marker = GameObject.Find("Mossling Guide Available Marker");
            global::Wayroot.Creatures.MosslingPresencePresentation presence = creature.GetComponent<global::Wayroot.Creatures.MosslingPresencePresentation>();
            GameObject presenceRoot = GameObject.Find("Mossling Presence Visual Root");
            Assert.That(presence, Is.Not.Null);
            Assert.That(presenceRoot, Is.Not.Null);
            Assert.That(presenceRoot.transform.parent, Is.EqualTo(creature.transform));
            Assert.That(presenceRoot.transform.lossyScale.x, Is.EqualTo(1f).Within(0.01f));
            Assert.That(GameObject.Find("Mossling Guide Pointer").GetComponent<Collider>(), Is.Null);
            Assert.That(GameObject.Find("Mossling Guide Pointer").GetComponent<Renderer>().sharedMaterial.name, Does.Contain("ActorSpriteUnlit"));
            Assert.That(guide.Selection.Kind, Is.EqualTo(global::Wayroot.Creatures.MosslingGuideKind.Available));
            Assert.That(marker.activeSelf, Is.True);
            Assert.That(guide.Status, Does.Contain("GUIDE"));

            GatheringNode flower = GameObject.Find("Wildflower (hold E)").GetComponent<GatheringNode>();
            Assert.That(flower.TryGather(), Is.True);
            yield return null;

            Assert.That(guide.Selection.Kind, Is.EqualTo(global::Wayroot.Creatures.MosslingGuideKind.Available));
            Assert.That(guide.Selection.NodeName, Is.Not.EqualTo("WILDFLOWER"));
            Assert.That(marker.activeSelf, Is.True);

            long deadline = System.DateTime.UtcNow.AddSeconds(12).Ticks;
            foreach (GatheringNode node in GameObject.FindObjectsByType<GatheringNode>(FindObjectsSortMode.None))
            {
                node.StartRenewal(deadline);
            }

            yield return null;

            Assert.That(guide.Selection.Kind, Is.EqualTo(global::Wayroot.Creatures.MosslingGuideKind.Renewing));
            Assert.That(marker.activeSelf, Is.False);
            Assert.That(guide.Status, Does.Contain("renews"));

            PrototypeGatheringSaveService.Reset();
        }

        [UnityTest]
        public IEnumerator SoundToggle_PersistsAcrossRestartAndResetRestoresEnabledDefault()
        {
            PrototypeGatheringSaveService.Reset();
            AsyncOperation firstLoad = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return firstLoad;
            yield return null;

            ProceduralSoundscape soundscape = GameObject.Find("Procedural Cozy Soundscape").GetComponent<ProceduralSoundscape>();
            Assert.That(soundscape.IsSoundEnabled, Is.True);
            soundscape.SetSoundEnabled(false);
            Assert.That(PrototypeGatheringSaveService.Load().soundEnabled, Is.False);

            AsyncOperation restart = SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);
            yield return restart;
            yield return null;

            soundscape = GameObject.Find("Procedural Cozy Soundscape").GetComponent<ProceduralSoundscape>();
            Assert.That(soundscape.IsSoundEnabled, Is.False);
            Assert.That(GameObject.Find("Sound Toggle Label").GetComponent<UnityEngine.UI.Text>().text, Is.EqualTo("SOUND OFF"));

            PrototypeGatheringSaveService.Reset();
            Assert.That(PrototypeGatheringSaveService.Load().soundEnabled, Is.True);
        }
    }
}

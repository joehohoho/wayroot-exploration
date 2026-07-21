using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Wayroot.Camera;
using Wayroot.Character;
using Wayroot.Input;
using Wayroot.Inventory;
using Wayroot.Gathering;
using Wayroot.Building;
using Wayroot.Combat;
using Wayroot.Creatures;
using Wayroot.Progression;
using Wayroot.UI;
using Wayroot.Wayroot;
using Wayroot.Audio;
using Wayroot.Exploration;
using Wayroot.Art;
using Wayroot.Guidance;

namespace Wayroot.Core
{
    /// <summary>Composes the controlled Phase 7 Sunmeadow prototype scene and its retained gameplay loops.</summary>
    public sealed class GameBootstrap : MonoBehaviour
    {
        private static readonly Color GroundColor = new(0.31f, 0.50f, 0.25f, 1f);
        private static readonly Color MeadowColor = new(0.45f, 0.68f, 0.30f, 1f);
        private static readonly Color PathColor = new(0.74f, 0.51f, 0.27f, 1f);
        private static readonly Color WaterColor = new(0.17f, 0.55f, 0.62f, 1f);
        private static readonly Color TrunkColor = new(0.30f, 0.16f, 0.08f, 1f);
        private static readonly Color LeafColor = new(0.12f, 0.42f, 0.19f, 1f);
        private static readonly Color PlayerColor = new(0.31f, 0.60f, 0.83f, 1f);
        private static readonly Color TreeColor = new(0.30f, 0.16f, 0.08f, 1f);

        private void Awake()
        {
            Application.targetFrameRate = 60;
            BuildPrototype();
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            Debug.Log($"{ProjectIdentity.ProductName}: Phase 14 warm stylized world loaded.", this);
#endif
        }

        private void BuildPrototype()
        {
            CreateLight();
            CreateGround();
            PrototypeInputReader input = new GameObject("Prototype Input").AddComponent<PrototypeInputReader>();
            PrototypePlayerController player = CreatePlayer(input);
            PrototypePlayerHealth playerHealth = player.gameObject.AddComponent<PrototypePlayerHealth>();
            player.Configure(input, playerHealth);
            TopDownCameraController cameraController = CreateCamera(player.transform, input, out UnityEngine.Camera sceneCamera);
            CreateObstruction(sceneCamera, player.transform);
            PrototypeGatheringController gathering = CreateGathering(input, player, sceneCamera);
            PrototypeMerchantController merchant = CreateMerchant(input, player, gathering, sceneCamera);
            PrototypeBuildController build = CreateBuildPlot(input, player, playerHealth, gathering, sceneCamera);
            PrototypeWayrootController wayroot = CreateWayroot(input, player, gathering, sceneCamera);
            PrototypeCreatureController creature = CreateCreature(input, player, gathering, sceneCamera);
            PrototypeEnemy enemy = CreateCombat(input, player, playerHealth, sceneCamera, out PrototypeAttackController attack);
            RestoredGroveController grove = CreateRestoredGrove(input, player, playerHealth, gathering, sceneCamera, attack, enemy, out PrototypeEnemy guardian, out MoonlitGladeController moonlitGlade, out BloomwellController bloomwell, out GameObject bloomwellRestoredVisual);
            PauseController pause = new GameObject("Pause Controller").AddComponent<PauseController>();
            pause.Configure(player, cameraController);
            ProceduralSoundscape soundscape = new GameObject("Procedural Cozy Soundscape").AddComponent<ProceduralSoundscape>();
            soundscape.Configure(gathering);
            CreatePhaseEighteenArtMotion(player, creature, enemy, grove, gathering);
            GameObject sunmeadowFinaleMotif = CreateSunmeadowFinaleMotif();
            BloomwellFinalePresentation finalePresentation = new GameObject("Bloomwell Finale Presentation").AddComponent<BloomwellFinalePresentation>();
            finalePresentation.Configure(gathering, creature, bloomwell, bloomwellRestoredVisual, sunmeadowFinaleMotif, GameObject.Find("Sunmeadow Sun").GetComponent<Light>());
            ActionFeedbackHud feedback = CreateRuntimeUi(input, player, playerHealth, enemy, grove, cameraController, pause, gathering, merchant, build, wayroot, creature, bloomwell, soundscape, sceneCamera, gathering.Nodes[0].transform, merchant.transform, build.transform, wayroot.transform, guardian.transform, bloomwell.transform);
            gathering.SetFeedback(feedback);
            gathering.SetSoundscape(soundscape);
            merchant.SetFeedback(feedback);
            build.SetFeedback(feedback);
            wayroot.SetFeedback(feedback);
            creature.SetFeedback(feedback);
            attack.SetFeedback(feedback);
            attack.SetSoundscape(soundscape);
            enemy.SetFeedback(feedback);
            grove.SetFeedback(feedback);
            moonlitGlade.SetFeedback(feedback);
            bloomwell.SetFeedback(feedback);
            bloomwell.SetSoundscape(soundscape);
            playerHealth.SetFeedback(feedback);
            playerHealth.SetSoundscape(soundscape);
            player.SetSoundscape(soundscape);
            pause.SetFeedback(feedback);
            build.SetSoundscape(soundscape);
            wayroot.SetSoundscape(soundscape);
            creature.SetSoundscape(soundscape);
            enemy.GetComponent<PrototypeEnemyChase>().SetSoundscape(soundscape);
            guardian.GetComponent<PrototypeEnemyChase>().SetSoundscape(soundscape);
        }

        private static void CreateLight()
        {
            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(0.49f, 0.70f, 0.84f);
            RenderSettings.ambientEquatorColor = new Color(0.71f, 0.76f, 0.49f);
            RenderSettings.ambientGroundColor = new Color(0.20f, 0.28f, 0.16f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.72f, 0.82f, 0.72f);
            RenderSettings.fogDensity = 0.009f;
            GameObject lightObject = new("Sunmeadow Sun");
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.color = new Color(1f, 0.82f, 0.60f);
            light.intensity = 1.25f;
            light.shadows = LightShadows.Soft;
            lightObject.transform.rotation = Quaternion.Euler(48f, -28f, 0f);
        }

        private static void CreateGround()
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Sunmeadow Clearing";
            ground.transform.localScale = new Vector3(4f, 1f, 4f);
            SetMaterialColor(ground.GetComponent<Renderer>(), GroundColor);
            CreateVisualRegion();
        }

        private static void CreateVisualRegion()
        {
            GameObject meadow = CreateVisualPrimitive("Sunmeadow Wildflower Meadow", PrimitiveType.Plane, new Vector3(-3.5f, 0.012f, 3.4f), new Vector3(1.15f, 1f, 0.9f), MeadowColor);
            meadow.transform.rotation = Quaternion.Euler(0f, -14f, 0f);
            CreateVisualPrimitive("Sunmeadow Footpath", PrimitiveType.Cube, new Vector3(-1.5f, 0.035f, -2f), new Vector3(2.1f, 0.07f, 13.5f), PathColor).transform.rotation = Quaternion.Euler(0f, 37f, 0f);
            GameObject creek = CreateVisualPrimitive("Sunmeadow Creek", PrimitiveType.Cube, new Vector3(6.6f, 0.025f, 0.8f), new Vector3(2.1f, 0.05f, 18f), WaterColor);
            creek.transform.rotation = Quaternion.Euler(0f, -11f, 0f);
            CreateVisualPrimitive("River Lily 1", PrimitiveType.Sphere, creek.transform.position + new Vector3(-0.38f, 0.09f, -2.4f), new Vector3(0.48f, 0.05f, 0.38f), new Color(0.96f, 0.65f, 0.75f)).transform.SetParent(creek.transform, true);
            CreateVisualPrimitive("River Lily 2", PrimitiveType.Sphere, creek.transform.position + new Vector3(0.25f, 0.09f, 1.2f), new Vector3(0.36f, 0.05f, 0.30f), new Color(1f, 0.82f, 0.50f)).transform.SetParent(creek.transform, true);
            CreateVisualPrimitive("Sunmeadow Creek Shore", PrimitiveType.Cube, new Vector3(5.35f, 0.02f, 0.8f), new Vector3(0.35f, 0.04f, 18.3f), new Color(0.82f, 0.72f, 0.45f)).transform.rotation = Quaternion.Euler(0f, -11f, 0f);

            Transform northGrove = new GameObject("Sunmeadow North Grove").transform;
            CreateTree(northGrove, new Vector3(-7.6f, 0f, 7.5f), 1.15f);
            CreateTree(northGrove, new Vector3(-4.8f, 0f, 7.8f), 0.85f);
            CreateTree(northGrove, new Vector3(0.2f, 0f, 8.2f), 1.3f);
            CreateTree(northGrove, new Vector3(4.1f, 0f, 7.2f), 0.95f);
            CreateTree(northGrove, new Vector3(8.4f, 0f, 6.4f), 1.25f);

            Transform rockGarden = new GameObject("Sunmeadow South Rock Garden").transform;
            CreateRock(rockGarden, new Vector3(-8.1f, 0.45f, -7.4f), new Vector3(1.25f, 0.9f, 0.95f));
            CreateRock(rockGarden, new Vector3(-6.6f, 0.3f, -7.6f), new Vector3(0.75f, 0.6f, 0.7f));
            CreateRock(rockGarden, new Vector3(-7.2f, 0.22f, -6.25f), new Vector3(0.55f, 0.45f, 0.52f));
            CreateTree(rockGarden, new Vector3(8.9f, 0f, -6.8f), 1.2f);
            CreateTree(rockGarden, new Vector3(6.8f, 0f, -7.7f), 0.8f);

            Transform flowerCluster = new GameObject("Sunmeadow Flower Clusters").transform;
            CreateFlowerCluster(flowerCluster, new Vector3(-5.3f, 0f, 4.2f), new Color(0.98f, 0.42f, 0.55f));
            CreateFlowerCluster(flowerCluster, new Vector3(-2.5f, 0f, 4.6f), new Color(1f, 0.82f, 0.24f));
            CreateFlowerCluster(flowerCluster, new Vector3(1.2f, 0f, 5.7f), new Color(0.72f, 0.45f, 0.94f));
            CreateFlowerCluster(flowerCluster, new Vector3(2.5f, 0f, -5.7f), new Color(0.98f, 0.42f, 0.55f));
            CreateFlowerCluster(flowerCluster, new Vector3(4.4f, 0f, 3.8f), new Color(1f, 0.82f, 0.24f));
        }

        private static GameObject CreateVisualPrimitive(string name, PrimitiveType primitive, Vector3 position, Vector3 scale, Color color)
        {
            GameObject visual = GameObject.CreatePrimitive(primitive);
            visual.name = name;
            visual.transform.position = position;
            visual.transform.localScale = scale;
            Destroy(visual.GetComponent<Collider>());
            SetMaterialColor(visual.GetComponent<Renderer>(), color);
            return visual;
        }

        private static void CreateTree(Transform parent, Vector3 position, float size)
        {
            GameObject tree = new("Sunmeadow Tree");
            tree.transform.SetParent(parent);
            tree.transform.position = position;
            CreateVisualPrimitive("Tree Trunk", PrimitiveType.Cylinder, position + new Vector3(0f, size, 0f), new Vector3(0.28f * size, size, 0.28f * size), TrunkColor).transform.SetParent(tree.transform, true);
            CreateVisualPrimitive("Tree Canopy Low", PrimitiveType.Sphere, position + new Vector3(-0.18f * size, size * 2.05f, 0f), new Vector3(1.08f * size, 0.92f * size, 1.08f * size), LeafColor).transform.SetParent(tree.transform, true);
            CreateVisualPrimitive("Tree Canopy High", PrimitiveType.Sphere, position + new Vector3(0.35f * size, size * 2.45f, 0.08f), new Vector3(0.85f * size, 0.72f * size, 0.85f * size), new Color(0.22f, 0.55f, 0.25f)).transform.SetParent(tree.transform, true);
        }

        private static void CreateRock(Transform parent, Vector3 position, Vector3 scale)
        {
            CreateVisualPrimitive("Sunmeadow Rock", PrimitiveType.Sphere, position, scale, new Color(0.33f, 0.40f, 0.43f)).transform.SetParent(parent, true);
        }

        private static void CreateFlowerCluster(Transform parent, Vector3 center, Color petalColor)
        {
            for (int index = 0; index < 3; index++)
            {
                float angle = index * 2.1f;
                Vector3 position = center + new Vector3(Mathf.Cos(angle) * 0.45f, 0f, Mathf.Sin(angle) * 0.45f);
                CreateVisualPrimitive("Sunmeadow Flower Stem", PrimitiveType.Cylinder, position + new Vector3(0f, 0.22f, 0f), new Vector3(0.05f, 0.22f, 0.05f), new Color(0.16f, 0.42f, 0.17f)).transform.SetParent(parent, true);
                CreateVisualPrimitive("Sunmeadow Flower", PrimitiveType.Sphere, position + new Vector3(0f, 0.5f, 0f), new Vector3(0.3f, 0.15f, 0.3f), petalColor).transform.SetParent(parent, true);
            }
        }

        private static PrototypePlayerController CreatePlayer(PrototypeInputReader input)
        {
            GameObject playerObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerObject.name = "Prototype Player";
            playerObject.transform.position = new Vector3(0f, 1f, 0f);
            Destroy(playerObject.GetComponent<Collider>());
            playerObject.AddComponent<CharacterController>();
            SetMaterialColor(playerObject.GetComponent<Renderer>(), PlayerColor);
            CreateVisualPrimitive("Player Cloak", PrimitiveType.Sphere, playerObject.transform.position + new Vector3(0f, -0.1f, 0.05f), new Vector3(0.92f, 0.76f, 0.78f), new Color(0.22f, 0.36f, 0.72f)).transform.SetParent(playerObject.transform, true);
            CreateVisualPrimitive("Player Lantern", PrimitiveType.Sphere, playerObject.transform.position + new Vector3(0.48f, 0.35f, 0.2f), new Vector3(0.18f, 0.24f, 0.18f), new Color(1f, 0.78f, 0.30f)).transform.SetParent(playerObject.transform, true);
            CreateVisualPrimitive("Player Hair", PrimitiveType.Sphere, playerObject.transform.position + new Vector3(0f, 0.72f, 0.04f), new Vector3(0.58f, 0.26f, 0.52f), new Color(0.25f, 0.12f, 0.08f)).transform.SetParent(playerObject.transform, true);
            PrototypePlayerController player = playerObject.AddComponent<PrototypePlayerController>();
            return player;
        }

        private static TopDownCameraController CreateCamera(Transform target, PrototypeInputReader input, out UnityEngine.Camera sceneCamera)
        {
            GameObject cameraObject = new("Prototype Camera");
            cameraObject.tag = "MainCamera";
            sceneCamera = cameraObject.AddComponent<UnityEngine.Camera>();
            sceneCamera.backgroundColor = new Color(0.49f, 0.73f, 0.86f);
            sceneCamera.clearFlags = CameraClearFlags.Skybox;
            cameraObject.AddComponent<AudioListener>();
            TopDownCameraController controller = cameraObject.AddComponent<TopDownCameraController>();
            controller.Configure(target, input);
            return controller;
        }

        private static void CreateObstruction(UnityEngine.Camera sourceCamera, Transform target)
        {
            GameObject tree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            tree.name = "Fadeable Test Tree";
            tree.transform.SetPositionAndRotation(new Vector3(0f, 2f, -3f), Quaternion.identity);
            tree.transform.localScale = new Vector3(1.2f, 2f, 1.2f);
            SetMaterialColor(tree.GetComponent<Renderer>(), TreeColor);
            tree.AddComponent<FadeableObstruction>();
            CameraObstructionFader fader = sourceCamera.gameObject.AddComponent<CameraObstructionFader>();
            fader.Configure(sourceCamera, target);
        }

        private static PrototypeMerchantController CreateMerchant(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, UnityEngine.Camera sceneCamera)
        {
            GameObject station = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            station.name = "Iron Edge Merchant Station (hold E)";
            station.transform.position = new Vector3(2f, 0.75f, -3f);
            station.transform.localScale = new Vector3(1.4f, 0.75f, 1.4f);
            SetMaterialColor(station.GetComponent<Renderer>(), new Color(0.9f, 0.66f, 0.18f));
            CreateVisualPrimitive("Merchant Awning", PrimitiveType.Sphere, station.transform.position + new Vector3(0f, 1.45f, 0f), new Vector3(1.75f, 0.34f, 1.2f), new Color(0.86f, 0.30f, 0.22f)).transform.SetParent(station.transform, true);
            CreateVisualPrimitive("Merchant Lantern", PrimitiveType.Sphere, station.transform.position + new Vector3(0.78f, 1.12f, 0.1f), new Vector3(0.18f, 0.28f, 0.18f), new Color(1f, 0.78f, 0.30f)).transform.SetParent(station.transform, true);

            GameObject sign = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sign.name = "Iron Edge Merchant Sign";
            sign.transform.SetParent(station.transform, false);
            sign.transform.localScale = new Vector3(1.2f, 1.1f, 0.15f);
            sign.transform.localPosition = new Vector3(0f, 1.25f, 0f);
            SetMaterialColor(sign.GetComponent<Renderer>(), new Color(0.2f, 0.12f, 0.04f));

            PrototypeMerchantController merchant = station.AddComponent<PrototypeMerchantController>();
            merchant.Configure(input, player, gathering);
            CreateWorldIdentifier("MERCHANT\nIRON EDGE", station.transform, new Vector3(0f, 2.25f, 0f), sceneCamera, new Color(1f, 0.85f, 0.35f));
            return merchant;
        }

        private static PrototypeBuildController CreateBuildPlot(PrototypeInputReader input, PrototypePlayerController player, PrototypePlayerHealth playerHealth, PrototypeGatheringController gathering, UnityEngine.Camera sceneCamera)
        {
            GameObject plot = GameObject.CreatePrimitive(PrimitiveType.Cube);
            plot.name = "Shelter Build Plot (hold E)";
            plot.transform.position = new Vector3(-6f, 0.1f, -5f);
            plot.transform.localScale = new Vector3(3.8f, 0.2f, 3.8f);
            Renderer plotRenderer = plot.GetComponent<Renderer>();
            SetMaterialColor(plotRenderer, new Color(0.77f, 0.58f, 0.22f));

            GameObject sign = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sign.name = "Shelter Blueprint Sign";
            sign.transform.SetParent(plot.transform, false);
            sign.transform.localScale = new Vector3(1.7f, 1.1f, 0.15f);
            sign.transform.localPosition = new Vector3(0f, 1.15f, 0f);
            SetMaterialColor(sign.GetComponent<Renderer>(), new Color(0.12f, 0.24f, 0.42f));

            GameObject shelter = new("Built Shelter");
            shelter.transform.position = plot.transform.position;
            CreateShelterPiece("Shelter Back Wall", shelter.transform, new Vector3(0f, 1.15f, 0.85f), new Vector3(2.5f, 2.1f, 0.18f), new Color(0.48f, 0.29f, 0.12f));
            CreateShelterPiece("Shelter Left Wall", shelter.transform, new Vector3(-1.15f, 1.15f, 0f), new Vector3(0.18f, 2.1f, 1.8f), new Color(0.56f, 0.34f, 0.14f));
            CreateShelterPiece("Shelter Right Wall", shelter.transform, new Vector3(1.15f, 1.15f, 0f), new Vector3(0.18f, 2.1f, 1.8f), new Color(0.56f, 0.34f, 0.14f));
            GameObject roof = GameObject.CreatePrimitive(PrimitiveType.Cube);
            roof.name = "Shelter Roof";
            roof.transform.SetParent(shelter.transform, false);
            roof.transform.localPosition = new Vector3(0f, 2.45f, 0f);
            roof.transform.localScale = new Vector3(3.1f, 0.25f, 2.5f);
            roof.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            SetMaterialColor(roof.GetComponent<Renderer>(), new Color(0.72f, 0.18f, 0.13f));
            shelter.SetActive(false);

            PrototypeBuildController build = plot.AddComponent<PrototypeBuildController>();
            Vector3 shelterReturnPoint = shelter.transform.position + new Vector3(0f, 1f, -1.7f);
            TextMesh shelterLabel = CreateWorldIdentifier("SHELTER\nBUILD PLOT", shelter.transform, new Vector3(0f, 3.25f, 0f), sceneCamera, new Color(0.62f, 1f, 0.66f));
            build.Configure(input, player, gathering, playerHealth, shelter, plotRenderer, shelterLabel, shelterReturnPoint);
            return build;
        }

        private static PrototypeWayrootController CreateWayroot(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, UnityEngine.Camera sceneCamera)
        {
            GameObject wayroot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            wayroot.name = "Dormant Wayroot (hold E)";
            wayroot.transform.position = new Vector3(-0.8f, 0.9f, -5.2f);
            wayroot.transform.localScale = new Vector3(1.05f, 0.9f, 1.05f);
            Renderer dormantRenderer = wayroot.GetComponent<Renderer>();
            SetMaterialColor(dormantRenderer, new Color(0.24f, 0.16f, 0.38f));
            CreateVisualPrimitive("Wayroot Petal Crown", PrimitiveType.Sphere, wayroot.transform.position + new Vector3(0f, 1.05f, 0f), new Vector3(1.35f, 0.32f, 1.35f), new Color(0.45f, 0.72f, 0.45f)).transform.SetParent(wayroot.transform, true);
            CreateVisualPrimitive("Wayroot Heart", PrimitiveType.Sphere, wayroot.transform.position + new Vector3(0f, 1.32f, 0f), new Vector3(0.42f, 0.62f, 0.42f), new Color(0.28f, 0.94f, 0.60f)).transform.SetParent(wayroot.transform, true);

            GameObject restoredVisual = new("Restored Wayroot Bloom");
            restoredVisual.transform.position = wayroot.transform.position;
            CreateWayrootPiece("Wayroot Bloom Core", restoredVisual.transform, new Vector3(0f, 1.15f, 0f), new Vector3(0.7f, 1.0f, 0.7f), new Color(0.25f, 1f, 0.68f));
            CreateWayrootPiece("Wayroot Bloom Left", restoredVisual.transform, new Vector3(-0.85f, 0.55f, 0f), new Vector3(0.45f, 0.5f, 0.45f), new Color(0.52f, 0.96f, 0.54f));
            CreateWayrootPiece("Wayroot Bloom Right", restoredVisual.transform, new Vector3(0.85f, 0.55f, 0f), new Vector3(0.45f, 0.5f, 0.45f), new Color(0.52f, 0.96f, 0.54f));
            restoredVisual.SetActive(false);

            TextMesh label = CreateWorldIdentifier("WAYROOT", wayroot.transform, new Vector3(0f, 2.15f, 0f), sceneCamera, new Color(0.76f, 0.65f, 1f));
            label.gameObject.name = "Wayroot World Label";

            PrototypeWayrootController controller = wayroot.AddComponent<PrototypeWayrootController>();
            controller.Configure(input, player, gathering, restoredVisual, dormantRenderer, label);
            return controller;
        }

        private static void CreateWayrootPiece(string name, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject piece = CreateVisualPrimitive(name, PrimitiveType.Sphere, parent.position + localPosition, localScale, color);
            piece.transform.SetParent(parent, true);
        }

        private static PrototypeCreatureController CreateCreature(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, UnityEngine.Camera sceneCamera)
        {
            GameObject creature = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            creature.name = "Friendly Mossling (hold E)";
            creature.transform.position = new Vector3(-5.5f, 0.6f, 3.5f);
            creature.transform.localScale = new Vector3(1.2f, 1.1f, 1.2f);
            Destroy(creature.GetComponent<Collider>());
            SetMaterialColor(creature.GetComponent<Renderer>(), new Color(0.34f, 0.8f, 0.54f));
            CreateCreatureFeature("Mossling Face", creature.transform, new Vector3(0f, 0.12f, 0.58f), new Vector3(0.58f, 0.36f, 0.12f), new Color(0.84f, 0.94f, 0.56f));
            CreateCreatureFeature("Mossling Nose", creature.transform, new Vector3(0f, 0.13f, 0.7f), new Vector3(0.12f, 0.12f, 0.08f), new Color(0.24f, 0.16f, 0.10f));
            CreateCreatureFeature("Mossling Left Ear", creature.transform, new Vector3(-0.32f, 0.68f, 0f), new Vector3(0.22f, 0.52f, 0.22f), new Color(0.94f, 0.82f, 0.3f));
            CreateCreatureFeature("Mossling Right Ear", creature.transform, new Vector3(0.32f, 0.68f, 0f), new Vector3(0.22f, 0.52f, 0.22f), new Color(0.94f, 0.82f, 0.3f));
            CreateCreatureFeature("Mossling Tail", creature.transform, new Vector3(0f, 0.05f, -0.62f), new Vector3(0.3f, 0.3f, 0.5f), new Color(0.18f, 0.5f, 0.28f));
            PrototypeCreatureController controller = creature.AddComponent<PrototypeCreatureController>();
            controller.Configure(input, player, gathering, new Vector3(-4.1f, 0.6f, -4.1f));
            MosslingResourceGuide guide = creature.AddComponent<MosslingResourceGuide>();
            guide.Configure(player, gathering);
            controller.SetResourceGuide(guide);
            CreateWorldIdentifier("MOSSling\nCOMPANION", creature.transform, new Vector3(0f, 1.55f, 0f), sceneCamera, new Color(0.8f, 1f, 0.72f));
            return controller;
        }

        private static void CreateCreatureFeature(string name, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject feature = GameObject.CreatePrimitive(PrimitiveType.Cube);
            feature.name = name;
            feature.transform.SetParent(parent, false);
            feature.transform.localPosition = localPosition;
            feature.transform.localScale = localScale;
            Destroy(feature.GetComponent<Collider>());
            SetMaterialColor(feature.GetComponent<Renderer>(), color);
        }

        private static void CreateShelterPiece(string name, Transform parent, Vector3 localPosition, Vector3 localScale, Color color)
        {
            GameObject piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
            piece.name = name;
            piece.transform.SetParent(parent, false);
            piece.transform.localPosition = localPosition;
            piece.transform.localScale = localScale;
            SetMaterialColor(piece.GetComponent<Renderer>(), color);
        }

        private static PrototypeEnemy CreateCombat(PrototypeInputReader input, PrototypePlayerController player, PrototypePlayerHealth playerHealth, UnityEngine.Camera sceneCamera, out PrototypeAttackController attack)
        {
            GameObject enemyObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            enemyObject.name = "Practice Slime (hold SPACE)";
            enemyObject.transform.position = new Vector3(5f, 1f, -1f);
            enemyObject.transform.localScale = new Vector3(1.25f, 0.9f, 1.25f);
            SetMaterialColor(enemyObject.GetComponent<Renderer>(), new Color(0.85f, 0.25f, 0.3f));
            CreateVisualPrimitive("Slime Crown", PrimitiveType.Sphere, enemyObject.transform.position + new Vector3(0f, 0.72f, 0f), new Vector3(0.72f, 0.16f, 0.72f), new Color(1f, 0.70f, 0.28f)).transform.SetParent(enemyObject.transform, true);
            CreateVisualPrimitive("Slime Face", PrimitiveType.Sphere, enemyObject.transform.position + new Vector3(0f, 0f, 0.62f), new Vector3(0.58f, 0.28f, 0.10f), new Color(1f, 0.68f, 0.68f)).transform.SetParent(enemyObject.transform, true);
            PrototypeEnemy enemy = enemyObject.AddComponent<PrototypeEnemy>();
            EnemyCombatProfile slimeProfile = new("SLIME", ThornGuardianRules.PracticeSlimeHealth, ThornGuardianRules.PracticeSlimeContactDamage, 5f, 1.5f, 6f);
            enemy.Configure(enemyObject.GetComponent<Renderer>(), slimeProfile);
            GameObject healthBar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            healthBar.name = "Slime Health Bar";
            healthBar.transform.SetParent(enemyObject.transform, false);
            healthBar.transform.localScale = new Vector3(1f, 0.12f, 0.12f);
            healthBar.transform.localPosition = new Vector3(0f, 1.8f, 0f);
            SetMaterialColor(healthBar.GetComponent<Renderer>(), new Color(0.2f, 0.9f, 0.25f));
            healthBar.AddComponent<PrototypeWorldHealthBar>().Configure(enemy, healthBar.transform);
            enemyObject.AddComponent<PrototypeEnemyChase>().Configure(player.transform, playerHealth, slimeProfile);
            attack = new GameObject("Prototype Attack").AddComponent<PrototypeAttackController>();
            CreateWorldIdentifier("SLIME\nHOLD ATTACK", enemyObject.transform, new Vector3(0f, 2.2f, 0f), sceneCamera, new Color(1f, 0.64f, 0.64f));
            return enemy;
        }

        private static RestoredGroveController CreateRestoredGrove(PrototypeInputReader input, PrototypePlayerController player, PrototypePlayerHealth playerHealth, PrototypeGatheringController gathering, UnityEngine.Camera sceneCamera, PrototypeAttackController attack, PrototypeEnemy slime, out PrototypeEnemy guardian, out MoonlitGladeController moonlitGlade, out BloomwellController bloomwell, out GameObject bloomwellRestoredVisual)
        {
            GameObject grove = new("Restored Grove Edge");
            grove.transform.position = new Vector3(-6.7f, 0f, 1.2f);
            CreateVisualPrimitive("Restored Grove Clearing", PrimitiveType.Cylinder, grove.transform.position + new Vector3(0f, 0.04f, 0f), new Vector3(2.8f, 0.04f, 2.8f), new Color(0.36f, 0.72f, 0.34f)).transform.SetParent(grove.transform, true);
            CreateVisualPrimitive("Restored Grove Thorn Arch", PrimitiveType.Cube, grove.transform.position + new Vector3(0f, 1.3f, 1.8f), new Vector3(3.4f, 2.5f, 0.35f), new Color(0.20f, 0.38f, 0.14f)).transform.SetParent(grove.transform, true);
            CreateVisualPrimitive("Restored Grove Bloom", PrimitiveType.Sphere, grove.transform.position + new Vector3(-1.45f, 0.7f, -0.8f), new Vector3(0.7f, 1.4f, 0.7f), new Color(0.7f, 0.94f, 0.30f)).transform.SetParent(grove.transform, true);

            GameObject guardianObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            guardianObject.name = "Thorn Guardian (hold ATTACK)";
            guardianObject.transform.SetParent(grove.transform, true);
            guardianObject.transform.position = grove.transform.position + new Vector3(0f, 1f, -0.35f);
            guardianObject.transform.localScale = new Vector3(1.35f, 1.15f, 1.35f);
            SetMaterialColor(guardianObject.GetComponent<Renderer>(), new Color(0.24f, 0.48f, 0.15f));
            CreateVisualPrimitive("Thorn Guardian Crown", PrimitiveType.Sphere, guardianObject.transform.position + new Vector3(0f, 0.95f, 0f), new Vector3(1.25f, 0.18f, 1.25f), new Color(0.66f, 0.82f, 0.24f)).transform.SetParent(guardianObject.transform, true);
            CreateVisualPrimitive("Thorn Guardian Heart", PrimitiveType.Sphere, guardianObject.transform.position + new Vector3(0f, 0.12f, 0.72f), new Vector3(0.34f, 0.42f, 0.12f), new Color(0.96f, 0.56f, 0.26f)).transform.SetParent(guardianObject.transform, true);
            EnemyCombatProfile guardianProfile = ThornGuardianRules.Profile;
            guardian = guardianObject.AddComponent<PrototypeEnemy>();
            guardian.Configure(guardianObject.GetComponent<Renderer>(), guardianProfile);
            guardianObject.AddComponent<PrototypeEnemyChase>().Configure(player.transform, playerHealth, guardianProfile);
            GameObject healthBar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            healthBar.name = "Thorn Guardian Health Bar";
            healthBar.transform.SetParent(guardianObject.transform, false);
            healthBar.transform.localScale = new Vector3(1.25f, 0.12f, 0.12f);
            healthBar.transform.localPosition = new Vector3(0f, 1.95f, 0f);
            SetMaterialColor(healthBar.GetComponent<Renderer>(), new Color(0.68f, 0.9f, 0.22f));
            healthBar.AddComponent<PrototypeWorldHealthBar>().Configure(guardian, healthBar.transform);
            CreateWorldIdentifier("RESTORED GROVE\nTHORN GUARDIAN", guardianObject.transform, new Vector3(0f, 2.45f, 0f), sceneCamera, new Color(0.82f, 1f, 0.48f));

            attack.Configure(input, player, gathering, slime, guardian);
            moonlitGlade = CreateMoonlitGlade(grove.transform, input, player, gathering, guardian, sceneCamera, out bloomwell, out bloomwellRestoredVisual);
            grove.SetActive(false);
            RestoredGroveController controller = new GameObject("Restored Grove Controller").AddComponent<RestoredGroveController>();
            controller.Configure(gathering, grove, guardian);
            return controller;
        }

        private static void CreatePhaseEighteenArtMotion(PrototypePlayerController player, PrototypeCreatureController mossling, PrototypeEnemy slime, RestoredGroveController grove, PrototypeGatheringController gathering)
        {
            Transform playerRoot = player.transform;
            CreateVisualPrimitive("Player Warm Scarf", PrimitiveType.Sphere, playerRoot.position + new Vector3(0f, 0.28f, -0.34f), new Vector3(0.78f, 0.16f, 0.24f), new Color(0.94f, 0.34f, 0.22f)).transform.SetParent(playerRoot, true);
            CreateVisualPrimitive("Player Lantern Glow", PrimitiveType.Sphere, playerRoot.position + new Vector3(0.48f, 0.35f, 0.2f), Vector3.one * 0.32f, new Color(1f, 0.76f, 0.30f)).transform.SetParent(playerRoot, true);
            CreateVisualPrimitive("Player Dodge Trail", PrimitiveType.Sphere, playerRoot.position + new Vector3(0f, 0.18f, -0.54f), new Vector3(0.74f, 0.18f, 0.90f), new Color(0.58f, 0.84f, 1f)).transform.SetParent(playerRoot, true);
            CreateVisualPrimitive("Player Dodge Trail Glow", PrimitiveType.Sphere, playerRoot.position + new Vector3(0f, 0.28f, -0.82f), new Vector3(0.32f, 0.10f, 0.52f), new Color(0.96f, 0.80f, 0.38f)).transform.SetParent(playerRoot, true);
            ConfigureMotion(playerRoot.gameObject, ProceduralStylizedAnimator.MotionStyle.Player, player, null, null, gathering,
                playerRoot.Find("Player Cloak"), playerRoot.Find("Player Lantern"), playerRoot.Find("Player Hair"), playerRoot.Find("Player Warm Scarf"), playerRoot.Find("Player Lantern Glow"), playerRoot.Find("Player Dodge Trail"), playerRoot.Find("Player Dodge Trail Glow"));

            Transform mosslingRoot = mossling.transform;
            CreateVisualPrimitive("Mossling Leaf Cap", PrimitiveType.Sphere, mosslingRoot.position + new Vector3(0f, 0.7f, 0f), new Vector3(0.8f, 0.18f, 0.7f), new Color(0.16f, 0.50f, 0.28f)).transform.SetParent(mosslingRoot, true);
            CreateVisualPrimitive("Mossling Glow Cheek", PrimitiveType.Sphere, mosslingRoot.position + new Vector3(0.28f, 0.17f, 0.64f), Vector3.one * 0.13f, new Color(1f, 0.78f, 0.32f)).transform.SetParent(mosslingRoot, true);
            ConfigureMotion(mosslingRoot.gameObject, ProceduralStylizedAnimator.MotionStyle.Mossling, null, mossling, null, gathering,
                mosslingRoot.Find("Mossling Left Ear"), mosslingRoot.Find("Mossling Right Ear"), mosslingRoot.Find("Mossling Tail"), mosslingRoot.Find("Mossling Leaf Cap"), mosslingRoot.Find("Mossling Glow Cheek"));

            CreateAnimatedEnemyShell(slime.transform, "Slime", new Color(0.96f, 0.32f, 0.38f), ProceduralStylizedAnimator.MotionStyle.Slime, slime, gathering);
            PrototypeEnemy guardian = grove.Guardian;
            if (guardian != null) CreateAnimatedEnemyShell(guardian.transform, "Guardian", new Color(0.42f, 0.80f, 0.20f), ProceduralStylizedAnimator.MotionStyle.Guardian, guardian, gathering);

            GameObject creek = GameObject.Find("Sunmeadow Creek");
            if (creek != null)
            {
                CreateVisualPrimitive("Creek Ripple A", PrimitiveType.Sphere, creek.transform.position + new Vector3(-0.35f, 0.08f, -3.2f), new Vector3(0.78f, 0.025f, 0.44f), new Color(0.42f, 0.82f, 0.92f)).transform.SetParent(creek.transform, true);
                CreateVisualPrimitive("Creek Ripple B", PrimitiveType.Sphere, creek.transform.position + new Vector3(0.32f, 0.08f, 2.5f), new Vector3(0.65f, 0.025f, 0.38f), new Color(0.58f, 0.88f, 0.94f)).transform.SetParent(creek.transform, true);
                ConfigureMotion(creek, ProceduralStylizedAnimator.MotionStyle.Water, null, null, null, gathering, creek.transform.Find("River Lily 1"), creek.transform.Find("River Lily 2"), creek.transform.Find("Creek Ripple A"), creek.transform.Find("Creek Ripple B"));
            }

            foreach (Transform canopy in GameObject.FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (canopy.name == "Tree Canopy Low" || canopy.name == "Tree Canopy High" || canopy.name == "Sunmeadow Flower")
                {
                    ConfigureMotion(canopy.gameObject, ProceduralStylizedAnimator.MotionStyle.Foliage, null, null, null, gathering, canopy);
                }
            }

            GameObject wayroot = GameObject.Find("Dormant Wayroot (hold E)");
            if (wayroot != null) ConfigureMotion(wayroot, ProceduralStylizedAnimator.MotionStyle.Landmark, null, null, null, gathering, wayroot.transform.Find("Wayroot Heart"), wayroot.transform.Find("Wayroot Petal Crown"));
            GameObject bloomwell = GameObject.Find("Moonlit Bloomwell Discovery");
            if (bloomwell != null) ConfigureMotion(bloomwell, ProceduralStylizedAnimator.MotionStyle.Landmark, null, null, null, gathering, bloomwell.transform.Find("Bloomwell Moon"), bloomwell.transform.Find("Bloomwell Mote One"), bloomwell.transform.Find("Bloomwell Mote Two"), bloomwell.transform.Find("Bloomwell Mote Three"));
        }

        private static void CreateAnimatedEnemyShell(Transform enemyRoot, string name, Color color, ProceduralStylizedAnimator.MotionStyle style, PrototypeEnemy enemy, PrototypeGatheringController gathering)
        {
            CreateVisualPrimitive($"{name} Animated Shell", PrimitiveType.Sphere, enemyRoot.position + new Vector3(0f, -0.08f, -0.05f), new Vector3(1.12f, 0.84f, 1.12f), color).transform.SetParent(enemyRoot, true);
            CreateVisualPrimitive($"{name} Attack Pulse", PrimitiveType.Sphere, enemyRoot.position + new Vector3(0f, 0.22f, 0.66f), new Vector3(0.28f, 0.18f, 0.10f), new Color(1f, 0.76f, 0.32f)).transform.SetParent(enemyRoot, true);
            ConfigureMotion(enemyRoot.gameObject, style, null, null, enemy, gathering, enemyRoot.Find($"{name} Animated Shell"), enemyRoot.Find($"{name} Attack Pulse"));
        }

        private static void ConfigureMotion(GameObject owner, ProceduralStylizedAnimator.MotionStyle style, PrototypePlayerController player, PrototypeCreatureController mossling, PrototypeEnemy enemy, PrototypeGatheringController gathering, params Transform[] candidates)
        {
            int count = 0;
            for (int index = 0; index < candidates.Length; index++) if (candidates[index] != null) count++;
            Transform[] parts = new Transform[count];
            int write = 0;
            for (int index = 0; index < candidates.Length; index++) if (candidates[index] != null) parts[write++] = candidates[index];
            if (parts.Length == 0) return;
            owner.AddComponent<ProceduralStylizedAnimator>().Configure(style, parts, player, mossling, enemy, gathering);
        }

        private static MoonlitGladeController CreateMoonlitGlade(Transform grove, PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, PrototypeEnemy guardian, UnityEngine.Camera sceneCamera, out BloomwellController bloomwell, out GameObject bloomwellRestoredVisual)
        {
            GameObject entrance = new("Moonlit Glade Passage");
            entrance.transform.SetParent(grove, true);
            GameObject sealedPath = new("Moonlit Glade Sealed Path");
            sealedPath.transform.SetParent(entrance.transform, false);
            sealedPath.transform.position = new Vector3(-7.35f, 0.45f, 3.65f);
            CreateVisualPrimitive("Moonlit Thorn Seal", PrimitiveType.Cylinder, sealedPath.transform.position, new Vector3(1.1f, 0.45f, 1.1f), new Color(0.24f, 0.12f, 0.35f)).transform.SetParent(sealedPath.transform, true);
            CreateVisualPrimitive("Moonlit Thorn Seal Glow", PrimitiveType.Sphere, sealedPath.transform.position + new Vector3(0f, 0.62f, 0f), new Vector3(0.55f, 0.55f, 0.55f), new Color(0.62f, 0.34f, 0.86f)).transform.SetParent(sealedPath.transform, true);
            CreateWorldIdentifier("MOONLIT GLADE\nSEALED PATH", sealedPath.transform, new Vector3(0f, 1.45f, 0f), sceneCamera, new Color(0.82f, 0.64f, 1f));

            GameObject glade = new("Moonlit Glade");
            glade.transform.SetParent(entrance.transform, true);
            CreateVisualPrimitive("Moonlit Violet Path", PrimitiveType.Cube, new Vector3(-7.7f, 0.045f, 4.15f), new Vector3(1.45f, 0.07f, 3.1f), new Color(0.34f, 0.25f, 0.56f)).transform.SetParent(glade.transform, true);
            CreateVisualPrimitive("Moonlit Glade Clearing", PrimitiveType.Cylinder, new Vector3(-8.15f, 0.04f, 5.9f), new Vector3(2.45f, 0.04f, 2.2f), new Color(0.22f, 0.34f, 0.48f)).transform.SetParent(glade.transform, true);
            bloomwell = CreateMoonlitLandmark(glade.transform, new Vector3(-8.15f, 0f, 6.15f), sceneCamera, input, player, gathering, out bloomwellRestoredVisual);

            GatheringNode petal = CreateGatheringNode("moonlit-wildflower-01", "Moonlit Wild Petal (hold E)", PrimitiveType.Sphere, new Vector3(-9.65f, 0.5f, 5.25f), new Color(0.82f, 0.52f, 1f), ResourceType.WildPetal, 1);
            GatheringNode timber = CreateGatheringNode("moonlit-sapling-01", "Moonlit Sapling (hold E)", PrimitiveType.Cylinder, new Vector3(-6.6f, 1f, 5.55f), new Color(0.30f, 0.42f, 0.68f), ResourceType.Timber, 3);
            GatheringNode stone = CreateGatheringNode("moonlit-stone-01", "Moonlit Stone (hold E)", PrimitiveType.Cube, new Vector3(-8.9f, 0.65f, 7.45f), new Color(0.47f, 0.48f, 0.75f), ResourceType.Stone, 3);
            petal.transform.SetParent(glade.transform, true);
            timber.transform.SetParent(glade.transform, true);
            stone.transform.SetParent(glade.transform, true);
            petal.SetWorldLabel(CreateWorldIdentifier("MOON PETAL\nWILD PETAL", petal.transform, new Vector3(0f, 1.05f, 0f), sceneCamera, new Color(0.92f, 0.76f, 1f)), "MOON PETAL", "WILD PETAL");
            timber.SetWorldLabel(CreateWorldIdentifier("MOON SAPLING\nTIMBER", timber.transform, new Vector3(0f, 2.35f, 0f), sceneCamera, new Color(0.72f, 0.82f, 1f)), "MOON SAPLING", "TIMBER");
            stone.SetWorldLabel(CreateWorldIdentifier("MOON STONE\nSTONE", stone.transform, new Vector3(0f, 1.45f, 0f), sceneCamera, new Color(0.82f, 0.86f, 1f)), "MOON STONE", "STONE");
            glade.SetActive(false);

            MoonlitGladeController controller = new GameObject("Moonlit Glade Controller").AddComponent<MoonlitGladeController>();
            controller.Configure(gathering, guardian, sealedPath, glade, new[] { petal, timber, stone });
            return controller;
        }

        private static BloomwellController CreateMoonlitLandmark(Transform parent, Vector3 position, UnityEngine.Camera sceneCamera, PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering, out GameObject restoredVisual)
        {
            GameObject landmark = new("Moonlit Bloomwell Discovery");
            landmark.transform.SetParent(parent, true);
            landmark.transform.position = position;
            CreateVisualPrimitive("Bloomwell Basin", PrimitiveType.Cylinder, position + new Vector3(0f, 0.18f, 0f), new Vector3(0.9f, 0.18f, 0.9f), new Color(0.30f, 0.25f, 0.52f)).transform.SetParent(landmark.transform, true);
            CreateVisualPrimitive("Bloomwell Moon", PrimitiveType.Sphere, position + new Vector3(0f, 0.92f, 0f), new Vector3(0.55f, 0.75f, 0.55f), new Color(0.72f, 0.72f, 1f)).transform.SetParent(landmark.transform, true);
            CreateVisualPrimitive("Bloomwell Petal", PrimitiveType.Sphere, position + new Vector3(0.65f, 0.46f, 0f), new Vector3(0.28f, 0.14f, 0.55f), new Color(0.92f, 0.56f, 1f)).transform.SetParent(landmark.transform, true);
            Light glow = landmark.AddComponent<Light>();
            glow.type = LightType.Point;
            glow.color = new Color(0.62f, 0.48f, 1f);
            glow.intensity = 1.45f;
            glow.range = 4.2f;
            CreateVisualPrimitive("Bloomwell Mote One", PrimitiveType.Sphere, position + new Vector3(-0.42f, 1.22f, 0.15f), Vector3.one * 0.10f, new Color(0.82f, 0.72f, 1f)).transform.SetParent(landmark.transform, true);
            CreateVisualPrimitive("Bloomwell Mote Two", PrimitiveType.Sphere, position + new Vector3(0.35f, 1.42f, -0.18f), Vector3.one * 0.07f, new Color(0.92f, 0.62f, 1f)).transform.SetParent(landmark.transform, true);
            CreateVisualPrimitive("Bloomwell Mote Three", PrimitiveType.Sphere, position + new Vector3(0.08f, 1.65f, 0.32f), Vector3.one * 0.06f, new Color(0.70f, 0.82f, 1f)).transform.SetParent(landmark.transform, true);
            restoredVisual = new GameObject("Bloomwell Restored Finale Bloom");
            restoredVisual.transform.SetParent(landmark.transform, true);
            CreateVisualPrimitive("Bloomwell Restored Heart", PrimitiveType.Sphere, position + new Vector3(0f, 1.22f, 0f), new Vector3(0.78f, 1.05f, 0.78f), new Color(0.48f, 1f, 0.82f)).transform.SetParent(restoredVisual.transform, true);
            CreateVisualPrimitive("Bloomwell Restored Petal Left", PrimitiveType.Sphere, position + new Vector3(-0.7f, 0.72f, 0f), new Vector3(0.34f, 0.18f, 0.66f), new Color(1f, 0.72f, 0.96f)).transform.SetParent(restoredVisual.transform, true);
            CreateVisualPrimitive("Bloomwell Restored Petal Right", PrimitiveType.Sphere, position + new Vector3(0.7f, 0.72f, 0f), new Vector3(0.34f, 0.18f, 0.66f), new Color(0.72f, 0.86f, 1f)).transform.SetParent(restoredVisual.transform, true);
            Light finaleGlow = restoredVisual.AddComponent<Light>();
            finaleGlow.type = LightType.Point;
            finaleGlow.color = new Color(0.56f, 1f, 0.86f);
            finaleGlow.intensity = 2.25f;
            finaleGlow.range = 5.5f;
            restoredVisual.SetActive(false);
            TextMesh label = CreateWorldIdentifier("BLOOMWELL\nHOLD GATHER", landmark.transform, new Vector3(0f, 1.92f, 0f), sceneCamera, new Color(0.88f, 0.76f, 1f));
            BloomwellController controller = landmark.AddComponent<BloomwellController>();
            controller.Configure(input, player, gathering, restoredVisual, landmark.transform.Find("Bloomwell Basin").GetComponent<Renderer>(), label);
            return controller;
        }

        private static PrototypeGatheringController CreateGathering(PrototypeInputReader input, PrototypePlayerController player, UnityEngine.Camera sceneCamera)
        {
            InventoryState inventory = new();
            GatheringNode flower = CreateGatheringNode("wildflower-01", "Wildflower (hold E)", PrimitiveType.Sphere, new Vector3(-2f, 0.5f, 2f), new Color(0.95f, 0.35f, 0.65f), ResourceType.WildPetal, 1);
            GatheringNode tree = CreateGatheringNode("young-tree-01", "Young Tree (hold E)", PrimitiveType.Cylinder, new Vector3(3f, 1f, 2f), new Color(0.32f, 0.6f, 0.2f), ResourceType.Timber, 3);
            GatheringNode rock = CreateGatheringNode("stone-outcrop-01", "Stone Outcrop (hold E)", PrimitiveType.Cube, new Vector3(-3f, 0.65f, -2f), new Color(0.45f, 0.48f, 0.55f), ResourceType.Stone, 3);
            PrototypeGatheringController controller = new GameObject("Prototype Gathering").AddComponent<PrototypeGatheringController>();
            controller.Configure(input, player, inventory, new[] { flower, tree, rock });
            flower.SetWorldLabel(CreateWorldIdentifier("WILDFLOWER\nPETAL", flower.transform, new Vector3(0f, 1.05f, 0f), sceneCamera, new Color(1f, 0.76f, 0.88f)), "WILDFLOWER", "PETAL");
            tree.SetWorldLabel(CreateWorldIdentifier("YOUNG TREE\nTIMBER", tree.transform, new Vector3(0f, 2.35f, 0f), sceneCamera, new Color(0.78f, 1f, 0.68f)), "YOUNG TREE", "TIMBER");
            rock.SetWorldLabel(CreateWorldIdentifier("STONE OUTCROP\nSTONE", rock.transform, new Vector3(0f, 1.45f, 0f), sceneCamera, new Color(0.88f, 0.92f, 1f)), "STONE OUTCROP", "STONE");
            return controller;
        }

        private static TextMesh CreateWorldIdentifier(string value, Transform target, Vector3 offset, UnityEngine.Camera sceneCamera, Color color)
        {
            string compactValue = value.Split('\n')[0].ToUpperInvariant();
            GameObject label = new($"World Label: {value.Replace("\n", " ")}");
            TextMesh text = label.AddComponent<TextMesh>();
            text.text = compactValue;
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.fontSize = 32;
            text.characterSize = 0.035f;
            text.anchor = TextAnchor.MiddleCenter;
            text.alignment = TextAlignment.Center;
            text.color = color;
            label.AddComponent<WorldIdentifier>().Configure(target, offset, sceneCamera);
            return text;
        }

        private static GatheringNode CreateGatheringNode(string id, string name, PrimitiveType primitive, Vector3 position, Color color, ResourceType resource, int steps)
        {
            GameObject nodeObject = GameObject.CreatePrimitive(primitive);
            nodeObject.name = name;
            nodeObject.transform.position = position;
            SetMaterialColor(nodeObject.GetComponent<Renderer>(), color);
            GatheringNode node = nodeObject.AddComponent<GatheringNode>();
            node.Configure(id, resource, steps, nodeObject.GetComponent<Renderer>());
            return node;
        }

        private static GameObject CreateSunmeadowFinaleMotif()
        {
            GameObject motif = new("Sunmeadow Bloomwell Finale Motif");
            CreateVisualPrimitive("Sunmeadow Finale Bloom One", PrimitiveType.Sphere, new Vector3(-1.75f, 0.42f, -4.1f), new Vector3(0.52f, 0.22f, 0.52f), new Color(0.76f, 0.56f, 1f)).transform.SetParent(motif.transform, true);
            CreateVisualPrimitive("Sunmeadow Finale Bloom Two", PrimitiveType.Sphere, new Vector3(-0.7f, 0.38f, -4.45f), new Vector3(0.42f, 0.18f, 0.42f), new Color(0.48f, 0.92f, 0.84f)).transform.SetParent(motif.transform, true);
            CreateVisualPrimitive("Sunmeadow Finale Bloom Three", PrimitiveType.Sphere, new Vector3(0.35f, 0.40f, -4.0f), new Vector3(0.48f, 0.20f, 0.48f), new Color(1f, 0.72f, 0.92f)).transform.SetParent(motif.transform, true);
            Light glow = motif.AddComponent<Light>();
            glow.type = LightType.Point;
            glow.color = new Color(0.66f, 0.76f, 1f);
            glow.intensity = 1.4f;
            glow.range = 4f;
            motif.SetActive(false);
            return motif;
        }

        private static ActionFeedbackHud CreateRuntimeUi(PrototypeInputReader input, PrototypePlayerController player, PrototypePlayerHealth playerHealth, PrototypeEnemy enemy, RestoredGroveController grove, TopDownCameraController cameraController, PauseController pause, PrototypeGatheringController gathering, PrototypeMerchantController merchant, PrototypeBuildController build, PrototypeWayrootController wayroot, PrototypeCreatureController creature, BloomwellController bloomwell, ProceduralSoundscape soundscape, UnityEngine.Camera sceneCamera, Transform resourceTarget, Transform merchantTarget, Transform shelterTarget, Transform wayrootTarget, Transform guardianTarget, Transform bloomwellTarget)
        {
            Canvas canvas = new GameObject("Prototype HUD").AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvas.gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            canvas.gameObject.AddComponent<GraphicRaycaster>();
            CreateEventSystem();

            RectTransform safeArea = CreatePanel("Safe Area", canvas.transform, new Color(0f, 0f, 0f, 0f));
            Stretch(safeArea);
            RectTransform titleCard = CreatePanel("Wayroot Title Card", safeArea, new Color(0.05f, 0.13f, 0.14f, 0.90f));
            titleCard.sizeDelta = new Vector2(350f, 52f);
            titleCard.anchorMin = titleCard.anchorMax = new Vector2(0.5f, 1f);
            titleCard.pivot = new Vector2(0.5f, 1f);
            titleCard.anchoredPosition = new Vector2(0f, -18f);
            CreateText("Wayroot Title", titleCard, "WAYROOT  •  SUNMEADOW", 18, TextAnchor.MiddleCenter);
            RectTransform joystickArea = CreatePanel("Movement Joystick", safeArea, new Color(0f, 0f, 0f, 0.28f));
            joystickArea.sizeDelta = new Vector2(230f, 230f);
            joystickArea.anchoredPosition = new Vector2(56f, 56f);
            CreateText("Move Label", joystickArea, "MOVE", 20, TextAnchor.UpperCenter).rectTransform.anchoredPosition = new Vector2(0f, -12f);
            RectTransform joystickHandle = CreatePanel("Joystick Handle", joystickArea, new Color(0.8f, 0.9f, 1f, 0.75f));
            joystickHandle.sizeDelta = new Vector2(100f, 100f);
            joystickHandle.anchorMin = joystickHandle.anchorMax = joystickHandle.pivot = new Vector2(0.5f, 0.5f);
            joystickArea.gameObject.AddComponent<VirtualJoystick>().Configure(input, joystickHandle);

            RectTransform pauseButton = CreatePanel("Pause Button", safeArea, new Color(0.16f, 0.22f, 0.34f, 0.85f));
            pauseButton.sizeDelta = new Vector2(180f, 82f);
            pauseButton.anchorMin = pauseButton.anchorMax = new Vector2(0f, 1f);
            pauseButton.pivot = new Vector2(0f, 1f);
            pauseButton.anchoredPosition = new Vector2(24f, -178f);
            Button button = pauseButton.gameObject.AddComponent<Button>();
            button.onClick.AddListener(() => { soundscape.Play(SoundscapeCue.Ui); pause.Toggle(); });
            CreateText("Pause Label", pauseButton, "PAUSE", 32, TextAnchor.MiddleCenter);

            RectTransform soundButton = CreatePanel("Sound Toggle Button", safeArea, new Color(0.16f, 0.36f, 0.30f, 0.88f));
            soundButton.sizeDelta = new Vector2(180f, 62f);
            soundButton.anchorMin = soundButton.anchorMax = new Vector2(0f, 1f);
            soundButton.pivot = new Vector2(0f, 1f);
            soundButton.anchoredPosition = new Vector2(24f, -272f);
            Text soundLabel = CreateText("Sound Toggle Label", soundButton, string.Empty, 20, TextAnchor.MiddleCenter);
            soundButton.gameObject.AddComponent<Button>();
            soundButton.gameObject.AddComponent<SoundToggleButton>().Configure(soundLabel, soundscape);

            RectTransform resetButton = CreatePanel("Reset Prototype Button", safeArea, new Color(0.42f, 0.18f, 0.18f, 0.9f));
            resetButton.sizeDelta = new Vector2(180f, 70f);
            resetButton.anchorMin = resetButton.anchorMax = new Vector2(1f, 1f);
            resetButton.pivot = new Vector2(1f, 1f);
            resetButton.anchoredPosition = new Vector2(-24f, -278f);
            resetButton.gameObject.AddComponent<Button>().onClick.AddListener(() => { soundscape.Play(SoundscapeCue.Ui); gathering.ResetPrototype(); });
            CreateText("Reset Label", resetButton, "RESET", 22, TextAnchor.MiddleCenter);

            RectTransform gatherButton = CreatePanel("Gather Button", safeArea, new Color(0.2f, 0.5f, 0.25f, 0.9f));
            gatherButton.sizeDelta = new Vector2(180f, 105f);
            CreateText("Gather Label", gatherButton, "HOLD\nGATHER", 26, TextAnchor.MiddleCenter);
            gatherButton.gameObject.AddComponent<VirtualActionButton>().Configure(input);

            RectTransform attackButton = CreatePanel("Attack Button", safeArea, new Color(0.72f, 0.2f, 0.2f, 0.92f));
            attackButton.sizeDelta = new Vector2(180f, 105f);
            attackButton.anchorMin = attackButton.anchorMax = new Vector2(1f, 0f);
            attackButton.pivot = new Vector2(1f, 0f);
            attackButton.anchoredPosition = new Vector2(-260f, 56f);
            CreateText("Attack Label", attackButton, "HOLD\nATTACK", 26, TextAnchor.MiddleCenter);
            attackButton.gameObject.AddComponent<VirtualAttackButton>().Configure(input);

            RectTransform dodgeButton = CreatePanel("Dodge Button", safeArea, new Color(0.22f, 0.40f, 0.72f, 0.94f));
            dodgeButton.sizeDelta = new Vector2(180f, 86f);
            dodgeButton.anchorMin = dodgeButton.anchorMax = new Vector2(1f, 0f);
            dodgeButton.pivot = new Vector2(1f, 0f);
            dodgeButton.anchoredPosition = new Vector2(-56f, 186f);
            Text dodgeLabel = CreateText("Dodge Label", dodgeButton, string.Empty, 22, TextAnchor.MiddleCenter);
            dodgeButton.gameObject.AddComponent<VirtualDodgeButton>().Configure(input);
            dodgeButton.gameObject.AddComponent<DodgeCooldownHud>().Configure(dodgeLabel, player);

            // Three non-overlapping top cards reserve the center of the play field and omit development telemetry.
            RectTransform combatCard = CreatePanel("Combat Status Card", safeArea, new Color(0.08f, 0.16f, 0.18f, 0.90f));
            combatCard.sizeDelta = new Vector2(310f, 86f);
            combatCard.anchorMin = combatCard.anchorMax = new Vector2(0f, 1f);
            combatCard.pivot = new Vector2(0f, 1f);
            combatCard.anchoredPosition = new Vector2(24f, -18f);
            Text combatText = CreateText("Combat Status", combatCard, string.Empty, 18, TextAnchor.MiddleLeft);
            combatText.rectTransform.offsetMin = new Vector2(18f, 0f);
            combatText.rectTransform.offsetMax = new Vector2(-12f, 0f);
            combatText.gameObject.AddComponent<CombatHud>().Configure(combatText, playerHealth, enemy, grove);

            RectTransform resourceCard = CreatePanel("Resource Progression Card", safeArea, new Color(0.10f, 0.16f, 0.13f, 0.90f));
            resourceCard.sizeDelta = new Vector2(330f, 110f);
            resourceCard.anchorMin = resourceCard.anchorMax = new Vector2(1f, 1f);
            resourceCard.pivot = new Vector2(1f, 1f);
            resourceCard.anchoredPosition = new Vector2(-24f, -18f);
            Text inventoryText = CreateText("Gathering Inventory", resourceCard, string.Empty, 16, TextAnchor.MiddleLeft);
            inventoryText.rectTransform.offsetMin = new Vector2(18f, 0f);
            inventoryText.rectTransform.offsetMax = new Vector2(-12f, 0f);
            inventoryText.gameObject.AddComponent<GatheringHud>().Configure(inventoryText, gathering, merchant, build, wayroot, creature, bloomwell);

            RectTransform journeyCard = CreatePanel("Journey Guidance Card", safeArea, new Color(0.10f, 0.19f, 0.28f, 0.91f));
            journeyCard.sizeDelta = new Vector2(460f, 52f);
            journeyCard.anchorMin = journeyCard.anchorMax = new Vector2(0.5f, 1f);
            journeyCard.pivot = new Vector2(0.5f, 1f);
            journeyCard.anchoredPosition = new Vector2(0f, -82f);
            Text journeyText = CreateText("Journey Guidance", journeyCard, string.Empty, 16, TextAnchor.MiddleCenter);
            journeyText.color = new Color(1f, 0.88f, 0.60f);

            RectTransform journeyPointer = CreatePanel("Journey Firefly Pointer", safeArea, new Color(0.70f, 0.92f, 0.58f, 0.92f));
            journeyPointer.sizeDelta = new Vector2(44f, 44f);
            Text pointerGlyph = CreateText("Journey Firefly Glyph", journeyPointer, "✦", 28, TextAnchor.MiddleCenter);
            pointerGlyph.color = new Color(0.18f, 0.30f, 0.16f);
            journeyCard.gameObject.AddComponent<JourneyGuidanceController>().Configure(player, sceneCamera, journeyText, journeyPointer, resourceTarget, merchantTarget, shelterTarget, wayrootTarget, guardianTarget, bloomwellTarget);

            RectTransform promptCard = CreatePanel("Contextual Action Prompt", safeArea, new Color(0.05f, 0.11f, 0.13f, 0.90f));
            promptCard.sizeDelta = new Vector2(460f, 54f);
            promptCard.anchorMin = promptCard.anchorMax = new Vector2(0.5f, 0f);
            promptCard.pivot = new Vector2(0.5f, 0f);
            promptCard.anchoredPosition = new Vector2(0f, 202f);
            Text promptText = CreateText("Contextual Prompt Text", promptCard, string.Empty, 17, TextAnchor.MiddleCenter);
            promptText.gameObject.AddComponent<ContextualPromptHud>().Configure(promptText, gathering, merchant, build, wayroot, creature, bloomwell);

            RectTransform feedbackCard = CreatePanel("Action Feedback Card", safeArea, new Color(0.04f, 0.10f, 0.14f, 0.9f));
            feedbackCard.sizeDelta = new Vector2(540f, 62f);
            feedbackCard.anchorMin = feedbackCard.anchorMax = new Vector2(0.5f, 0f);
            feedbackCard.pivot = new Vector2(0.5f, 0f);
            feedbackCard.anchoredPosition = new Vector2(0f, 270f);
            Text feedbackText = CreateText("Action Feedback", feedbackCard, string.Empty, 24, TextAnchor.MiddleCenter);
            ActionFeedbackHud feedback = feedbackText.gameObject.AddComponent<ActionFeedbackHud>();
            feedback.Configure(feedbackText);

            SafeAreaLayout layout = safeArea.gameObject.AddComponent<SafeAreaLayout>();
            layout.Configure(safeArea, joystickArea, gatherButton);
            return feedback;
        }

        private static void CreateEventSystem()
        {
            if (EventSystem.current != null)
            {
                return;
            }

            GameObject eventSystem = new("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<InputSystemUIInputModule>();
        }

        private static RectTransform CreatePanel(string name, Transform parent, Color color)
        {
            GameObject panel = new(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            panel.transform.SetParent(parent, false);
            Image image = panel.GetComponent<Image>();
            image.color = color;
            return panel.GetComponent<RectTransform>();
        }

        private static Text CreateText(string name, Transform parent, string value, int fontSize, TextAnchor alignment)
        {
            GameObject textObject = new(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            textObject.transform.SetParent(parent, false);
            Text text = textObject.GetComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.text = value;
            text.fontSize = fontSize;
            text.alignment = alignment;
            text.color = Color.white;
            RectTransform rectTransform = text.rectTransform;
            Stretch(rectTransform);
            return text;
        }

        private static void Stretch(RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        private static void SetMaterialColor(Renderer renderer, Color color)
        {
            Material sourceMaterial = Resources.Load<Material>(color.b > color.r && color.b > color.g ? "Phase14/Water" : color.g > color.r ? "Phase14/Moss" : color.r > 0.8f ? "Phase14/Coral" : "Phase14/WarmGround");
            if (sourceMaterial != null) renderer.sharedMaterial = sourceMaterial;
            MaterialPropertyBlock properties = new();
            renderer.GetPropertyBlock(properties);
            properties.SetColor("_BaseColor", color);
            renderer.SetPropertyBlock(properties);
        }
    }
}

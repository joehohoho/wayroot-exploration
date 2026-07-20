using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
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

namespace Wayroot.Core
{
    /// <summary>Composes the intentionally small, disposable Phase 1 prototype scene.</summary>
    public sealed class GameBootstrap : MonoBehaviour
    {
        private static readonly Color GroundColor = new(0.31f, 0.56f, 0.28f, 1f);
        private static readonly Color PlayerColor = new(0.24f, 0.62f, 0.94f, 1f);
        private static readonly Color TreeColor = new(0.28f, 0.17f, 0.08f, 1f);

        private void Awake()
        {
            Application.targetFrameRate = 60;
            BuildPrototype();
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            Debug.Log($"{ProjectIdentity.ProductName}: Phase 1 prototype loaded.", this);
#endif
        }

        private void BuildPrototype()
        {
            CreateLight();
            CreateGround();
            PrototypeInputReader input = new GameObject("Prototype Input").AddComponent<PrototypeInputReader>();
            PrototypePlayerController player = CreatePlayer(input);
            PrototypePlayerHealth playerHealth = player.gameObject.AddComponent<PrototypePlayerHealth>();
            TopDownCameraController cameraController = CreateCamera(player.transform, input, out UnityEngine.Camera sceneCamera);
            CreateObstruction(sceneCamera, player.transform);
            PrototypeGatheringController gathering = CreateGathering(input, player);
            PrototypeMerchantController merchant = CreateMerchant(input, player, gathering);
            PrototypeBuildController build = CreateBuildPlot(input, player, gathering);
            PrototypeCreatureController creature = CreateCreature(input, player, gathering);
            PrototypeEnemy enemy = CreateCombat(input, player, playerHealth, gathering);
            PauseController pause = new GameObject("Pause Controller").AddComponent<PauseController>();
            pause.Configure(player, cameraController);
            CreateRuntimeUi(input, player, playerHealth, enemy, cameraController, pause, gathering, merchant, build, creature);
        }

        private static void CreateLight()
        {
            GameObject lightObject = new("Sunmeadow Sun");
            Light light = lightObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.2f;
            lightObject.transform.rotation = Quaternion.Euler(50f, -35f, 0f);
        }

        private static void CreateGround()
        {
            GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.name = "Sunmeadow Test Ground";
            ground.transform.localScale = new Vector3(4f, 1f, 4f);
            SetMaterialColor(ground.GetComponent<Renderer>(), GroundColor);
        }

        private static PrototypePlayerController CreatePlayer(PrototypeInputReader input)
        {
            GameObject playerObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerObject.name = "Prototype Player";
            playerObject.transform.position = new Vector3(0f, 1f, 0f);
            Destroy(playerObject.GetComponent<Collider>());
            playerObject.AddComponent<CharacterController>();
            SetMaterialColor(playerObject.GetComponent<Renderer>(), PlayerColor);
            PrototypePlayerController player = playerObject.AddComponent<PrototypePlayerController>();
            player.Configure(input);
            return player;
        }

        private static TopDownCameraController CreateCamera(Transform target, PrototypeInputReader input, out UnityEngine.Camera sceneCamera)
        {
            GameObject cameraObject = new("Prototype Camera");
            cameraObject.tag = "MainCamera";
            sceneCamera = cameraObject.AddComponent<UnityEngine.Camera>();
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

        private static PrototypeMerchantController CreateMerchant(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering)
        {
            GameObject station = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            station.name = "Iron Edge Merchant Station (hold E)";
            station.transform.position = new Vector3(2f, 0.75f, -3f);
            station.transform.localScale = new Vector3(1.4f, 0.75f, 1.4f);
            SetMaterialColor(station.GetComponent<Renderer>(), new Color(0.9f, 0.66f, 0.18f));

            GameObject sign = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sign.name = "Iron Edge Merchant Sign";
            sign.transform.SetParent(station.transform, false);
            sign.transform.localScale = new Vector3(1.2f, 1.1f, 0.15f);
            sign.transform.localPosition = new Vector3(0f, 1.25f, 0f);
            SetMaterialColor(sign.GetComponent<Renderer>(), new Color(0.2f, 0.12f, 0.04f));

            PrototypeMerchantController merchant = station.AddComponent<PrototypeMerchantController>();
            merchant.Configure(input, player, gathering);
            return merchant;
        }

        private static PrototypeBuildController CreateBuildPlot(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering)
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
            build.Configure(input, player, gathering, shelter, plotRenderer);
            return build;
        }

        private static PrototypeCreatureController CreateCreature(PrototypeInputReader input, PrototypePlayerController player, PrototypeGatheringController gathering)
        {
            GameObject creature = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            creature.name = "Friendly Mossling (hold E)";
            creature.transform.position = new Vector3(-5.5f, 0.6f, 3.5f);
            creature.transform.localScale = new Vector3(1.2f, 1.1f, 1.2f);
            Destroy(creature.GetComponent<Collider>());
            SetMaterialColor(creature.GetComponent<Renderer>(), new Color(0.34f, 0.8f, 0.54f));
            CreateCreatureFeature("Mossling Left Ear", creature.transform, new Vector3(-0.32f, 0.68f, 0f), new Vector3(0.22f, 0.52f, 0.22f), new Color(0.94f, 0.82f, 0.3f));
            CreateCreatureFeature("Mossling Right Ear", creature.transform, new Vector3(0.32f, 0.68f, 0f), new Vector3(0.22f, 0.52f, 0.22f), new Color(0.94f, 0.82f, 0.3f));
            CreateCreatureFeature("Mossling Tail", creature.transform, new Vector3(0f, 0.05f, -0.62f), new Vector3(0.3f, 0.3f, 0.5f), new Color(0.18f, 0.5f, 0.28f));
            PrototypeCreatureController controller = creature.AddComponent<PrototypeCreatureController>();
            controller.Configure(input, player, gathering, new Vector3(-4.1f, 0.6f, -4.1f));
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

        private static PrototypeEnemy CreateCombat(PrototypeInputReader input, PrototypePlayerController player, PrototypePlayerHealth playerHealth, PrototypeGatheringController gathering)
        {
            GameObject enemyObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            enemyObject.name = "Practice Slime (hold SPACE)";
            enemyObject.transform.position = new Vector3(5f, 1f, -1f);
            SetMaterialColor(enemyObject.GetComponent<Renderer>(), new Color(0.85f, 0.25f, 0.3f));
            PrototypeEnemy enemy = enemyObject.AddComponent<PrototypeEnemy>();
            enemy.Configure(enemyObject.GetComponent<Renderer>());
            GameObject healthBar = GameObject.CreatePrimitive(PrimitiveType.Cube);
            healthBar.name = "Slime Health Bar";
            healthBar.transform.SetParent(enemyObject.transform, false);
            healthBar.transform.localScale = new Vector3(1f, 0.12f, 0.12f);
            healthBar.transform.localPosition = new Vector3(0f, 1.8f, 0f);
            SetMaterialColor(healthBar.GetComponent<Renderer>(), new Color(0.2f, 0.9f, 0.25f));
            healthBar.AddComponent<PrototypeWorldHealthBar>().Configure(enemy, healthBar.transform);
            enemyObject.AddComponent<PrototypeEnemyChase>().Configure(player.transform, playerHealth);
            new GameObject("Prototype Attack").AddComponent<PrototypeAttackController>().Configure(input, player, enemy, gathering);
            return enemy;
        }

        private static PrototypeGatheringController CreateGathering(PrototypeInputReader input, PrototypePlayerController player)
        {
            InventoryState inventory = new();
            GatheringNode flower = CreateGatheringNode("wildflower-01", "Wildflower (hold E)", PrimitiveType.Sphere, new Vector3(-2f, 0.5f, 2f), new Color(0.95f, 0.35f, 0.65f), ResourceType.WildPetal, 1);
            GatheringNode tree = CreateGatheringNode("young-tree-01", "Young Tree (hold E)", PrimitiveType.Cylinder, new Vector3(3f, 1f, 2f), new Color(0.32f, 0.6f, 0.2f), ResourceType.Timber, 3);
            GatheringNode rock = CreateGatheringNode("stone-outcrop-01", "Stone Outcrop (hold E)", PrimitiveType.Cube, new Vector3(-3f, 0.65f, -2f), new Color(0.45f, 0.48f, 0.55f), ResourceType.Stone, 3);
            PrototypeGatheringController controller = new GameObject("Prototype Gathering").AddComponent<PrototypeGatheringController>();
            controller.Configure(input, player, inventory, new[] { flower, tree, rock });
            return controller;
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

        private static void CreateRuntimeUi(PrototypeInputReader input, PrototypePlayerController player, PrototypePlayerHealth playerHealth, PrototypeEnemy enemy, TopDownCameraController cameraController, PauseController pause, PrototypeGatheringController gathering, PrototypeMerchantController merchant, PrototypeBuildController build, PrototypeCreatureController creature)
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
            RectTransform joystickArea = CreatePanel("Movement Joystick", safeArea, new Color(0f, 0f, 0f, 0.28f));
            joystickArea.sizeDelta = new Vector2(230f, 230f);
            joystickArea.anchoredPosition = new Vector2(56f, 56f);
            RectTransform joystickHandle = CreatePanel("Joystick Handle", joystickArea, new Color(0.8f, 0.9f, 1f, 0.75f));
            joystickHandle.sizeDelta = new Vector2(100f, 100f);
            joystickHandle.anchorMin = joystickHandle.anchorMax = joystickHandle.pivot = new Vector2(0.5f, 0.5f);
            joystickArea.gameObject.AddComponent<VirtualJoystick>().Configure(input, joystickHandle);

            RectTransform pauseButton = CreatePanel("Pause Button", safeArea, new Color(0.16f, 0.22f, 0.34f, 0.85f));
            pauseButton.sizeDelta = new Vector2(180f, 105f);
            pauseButton.anchoredPosition = new Vector2(-56f, 56f);
            Button button = pauseButton.gameObject.AddComponent<Button>();
            button.onClick.AddListener(pause.Toggle);
            CreateText("Pause Label", pauseButton, "PAUSE", 32, TextAnchor.MiddleCenter);

            RectTransform resetButton = CreatePanel("Reset Prototype Button", safeArea, new Color(0.42f, 0.18f, 0.18f, 0.9f));
            resetButton.sizeDelta = new Vector2(180f, 70f);
            resetButton.anchorMin = resetButton.anchorMax = new Vector2(1f, 1f);
            resetButton.pivot = new Vector2(1f, 1f);
            resetButton.anchoredPosition = new Vector2(-56f, -176f);
            resetButton.gameObject.AddComponent<Button>().onClick.AddListener(gathering.ResetPrototype);
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

            RectTransform developmentText = CreateText("Development Overlay", safeArea, string.Empty, 26, TextAnchor.UpperLeft).rectTransform;
            developmentText.anchorMin = new Vector2(0f, 1f);
            developmentText.anchorMax = new Vector2(1f, 1f);
            developmentText.pivot = new Vector2(0.5f, 1f);
            developmentText.anchoredPosition = new Vector2(0f, -24f);
            developmentText.sizeDelta = new Vector2(-48f, 80f);
            DevelopmentOverlay overlay = developmentText.gameObject.AddComponent<DevelopmentOverlay>();
            overlay.Configure(developmentText.GetComponent<Text>(), player, cameraController, pause);

            Text inventoryText = CreateText("Gathering Inventory", safeArea, string.Empty, 24, TextAnchor.UpperRight);
            inventoryText.rectTransform.anchorMin = inventoryText.rectTransform.anchorMax = new Vector2(1f, 1f);
            inventoryText.rectTransform.pivot = new Vector2(1f, 1f);
            inventoryText.rectTransform.anchoredPosition = new Vector2(-24f, -120f);
            inventoryText.rectTransform.sizeDelta = new Vector2(700f, 90f);
            inventoryText.gameObject.AddComponent<GatheringHud>().Configure(inventoryText, gathering, merchant, build, creature);

            Text combatText = CreateText("Combat Status", safeArea, string.Empty, 24, TextAnchor.UpperLeft);
            combatText.rectTransform.anchorMin = combatText.rectTransform.anchorMax = new Vector2(0f, 1f);
            combatText.rectTransform.pivot = new Vector2(0f, 1f);
            combatText.rectTransform.anchoredPosition = new Vector2(24f, -120f);
            combatText.rectTransform.sizeDelta = new Vector2(650f, 48f);
            combatText.gameObject.AddComponent<CombatHud>().Configure(combatText, playerHealth, enemy);

            SafeAreaLayout layout = safeArea.gameObject.AddComponent<SafeAreaLayout>();
            layout.Configure(safeArea, joystickArea, gatherButton);
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
            Material material = renderer.material;
            if (material.HasProperty("_BaseColor"))
            {
                material.SetColor("_BaseColor", color);
            }
            else
            {
                material.color = color;
            }
        }
    }
}

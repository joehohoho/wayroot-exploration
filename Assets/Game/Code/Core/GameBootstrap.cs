using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Wayroot.Camera;
using Wayroot.Character;
using Wayroot.Input;
using Wayroot.Inventory;
using Wayroot.Gathering;
using Wayroot.Combat;
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
            TopDownCameraController cameraController = CreateCamera(player.transform, input, out UnityEngine.Camera sceneCamera);
            CreateObstruction(sceneCamera, player.transform);
            PrototypeGatheringController gathering = CreateGathering(input, player);
            CreateCombat(input, player);
            PauseController pause = new GameObject("Pause Controller").AddComponent<PauseController>();
            pause.Configure(player, cameraController);
            CreateRuntimeUi(input, player, cameraController, pause, gathering);
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

        private static void CreateCombat(PrototypeInputReader input, PrototypePlayerController player)
        {
            GameObject enemyObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            enemyObject.name = "Practice Slime (hold SPACE)";
            enemyObject.transform.position = new Vector3(5f, 1f, -1f);
            SetMaterialColor(enemyObject.GetComponent<Renderer>(), new Color(0.85f, 0.25f, 0.3f));
            PrototypeEnemy enemy = enemyObject.AddComponent<PrototypeEnemy>();
            enemy.Configure(enemyObject.GetComponent<Renderer>());
            new GameObject("Prototype Attack").AddComponent<PrototypeAttackController>().Configure(input, player, enemy);
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

        private static void CreateRuntimeUi(PrototypeInputReader input, PrototypePlayerController player, TopDownCameraController cameraController, PauseController pause, PrototypeGatheringController gathering)
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
            inventoryText.rectTransform.sizeDelta = new Vector2(650f, 48f);
            inventoryText.gameObject.AddComponent<GatheringHud>().Configure(inventoryText, gathering);

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

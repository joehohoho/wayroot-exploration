using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using Wayroot.Camera;
using Wayroot.Character;
using Wayroot.Input;
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
            PauseController pause = new GameObject("Pause Controller").AddComponent<PauseController>();
            pause.Configure(player, cameraController);
            CreateRuntimeUi(input, player, cameraController, pause);
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

        private static void CreateRuntimeUi(PrototypeInputReader input, PrototypePlayerController player, TopDownCameraController cameraController, PauseController pause)
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
            CreateText("Pause Label", pauseButton, "PAUSE", 32, TextAlignmentOptions.Center);

            RectTransform developmentText = CreateText("Development Overlay", safeArea, string.Empty, 26, TextAlignmentOptions.TopLeft).rectTransform;
            developmentText.anchorMin = new Vector2(0f, 1f);
            developmentText.anchorMax = new Vector2(1f, 1f);
            developmentText.pivot = new Vector2(0.5f, 1f);
            developmentText.anchoredPosition = new Vector2(0f, -24f);
            developmentText.sizeDelta = new Vector2(-48f, 80f);
            DevelopmentOverlay overlay = developmentText.gameObject.AddComponent<DevelopmentOverlay>();
            overlay.Configure(developmentText.GetComponent<TMP_Text>(), player, cameraController, pause);

            SafeAreaLayout layout = safeArea.gameObject.AddComponent<SafeAreaLayout>();
            layout.Configure(safeArea, joystickArea, pauseButton);
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

        private static TMP_Text CreateText(string name, Transform parent, string value, float fontSize, TextAlignmentOptions alignment)
        {
            GameObject textObject = new(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
            textObject.transform.SetParent(parent, false);
            TMP_Text text = textObject.GetComponent<TMP_Text>();
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

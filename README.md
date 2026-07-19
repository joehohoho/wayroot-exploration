# Wayroot Exploration *(working title)*

A peaceful top-down 3D mobile exploration game made with Unity: restore magical Wayroots while gathering, crafting, building a homestead, discovering original creatures, and optionally engaging in light combat.

> **Status:** Phase 1 movement/camera prototype is implemented and desktop-validated. It awaits owner play review and physical-iPhone evidence; Phase 2 is not approved.

## Technology

| Area | Choice |
|---|---|
| Engine | Unity **6000.5.4f1**, owner-approved and pinned |
| Rendering | Universal Render Pipeline 17.5.0 |
| Language | C# |
| Input | Input System 1.19.0 (`activeInputHandler: 1`) |
| Runtime touch UI | uGUI prototype (ADR-007) |
| Primary target | iPhone, landscape, touch-first |
| Development | Desktop editor builds with device-independent input actions |
| Source control | Git and Git LFS for approved binary source assets |

The local workstation has Unity 6000.5.4f1, which the owner approved as the project pin because Unity Hub did not offer a suitable Unity 6.0 LTS update.

## Run the current prototype

1. Use Unity **6000.5.4f1**. iPhone builds additionally require a Mac with the same editor, iOS Build Support, full Xcode, and configured signing.
2. Open this folder in Unity Hub and allow Package Manager to resolve dependencies.
3. Open `Assets/Game/Scenes/Bootstrap.unity` and press **Play**.
4. Use **WASD**, arrow keys, gamepad left stick, or the on-screen joystick to move.
5. Use the mouse wheel or touch pinch to zoom. Press **Escape** or **PAUSE** to toggle pause.

The small controlled scene deliberately contains only primitive placeholder geometry, one player, one fadeable test obstruction, camera, joystick, pause button, and development overlay. It has no gathering, combat, inventory, saves, or generated world.

## Automated validation

```bash
# Git Bash, from the project root
"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" \
  -batchmode -nographics -quit -projectPath "$PWD" -logFile Logs/compile.log

# Do not pass -quit to Unity Test Framework; it owns process shutdown.
"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" \
  -batchmode -nographics -projectPath "$PWD" \
  -runTests -testPlatform EditMode -testResults TestResults/editmode.xml \
  -logFile Logs/editmode-tests.log

"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" \
  -batchmode -nographics -projectPath "$PWD" \
  -runTests -testPlatform PlayMode -testResults TestResults/playmode.xml \
  -logFile Logs/playmode-tests.log
```

In the Editor use **Window → General → Test Runner** to run EditMode or PlayMode tests.

## Manual testing

Follow `Documentation/Phase1ManualTest.md` for Editor, Device Simulator, profiler, safe-area, and iPhone handoff instructions.

## Documentation

- `Documentation/GameDesign.md` — approved product direction
- `Documentation/Architecture.md` — layers, data flow, Mermaid diagram
- `Documentation/Roadmap.md` — milestones 0–9 and current status
- `Documentation/OpenQuestions.md` — decisions needing owner input
- `Documentation/Phase1ImplementationPlan.md` — approved Phase 1 scope
- `Documentation/Phase1ManualTest.md` — exact manual validation guide
- `Documentation/AgentHandoff.md` — durable Codex/Hermes restart context
- `Documentation/ADRs/` — architecture decisions

## Current limitations

- The Phase 1 primitive scene and runtime-built uGUI are prototype-only placeholders.
- iPhone build/device performance has not been verified; Windows cannot produce a signed iOS build.
- No Android tooling or CI Unity license setup has been verified.
- Title/repository name is provisional and may change later.

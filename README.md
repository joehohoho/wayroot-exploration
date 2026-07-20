# Wayroot Exploration *(working title)*

A peaceful top-down 3D mobile exploration prototype made with Unity: gather materials, survive light combat, improve a weapon, build a shelter, befriend a creature, and restore the first Wayroot in the Sunmeadow clearing.

> **Status:** Phase 10’s one persistent Wayroot restoration objective is implemented. Desktop and Device Simulator review evidence are documented; physical-iPhone evidence remains an independent gate.

## Technology

| Area | Choice |
|---|---|
| Engine | Unity **6000.5.4f1**, owner-approved and pinned |
| Rendering | Universal Render Pipeline 17.5.0 |
| Language | C# |
| Input | Input System 1.19.0 (`activeInputHandler: 1`) |
| Runtime touch UI | uGUI prototype (ADR-007) |
| Primary target | iPhone, landscape, touch-first |
| Development validation | Desktop editor builds with device-independent input actions |
| Source control | Git; generated builds, test results, and signing material are ignored |

## Run the current prototype

1. Use Unity **6000.5.4f1**. iPhone builds additionally require a Mac with the same editor, iOS Build Support, full Xcode, and configured signing.
2. Open this folder in Unity Hub and allow Package Manager to resolve dependencies.
3. Open `Assets/Game/Scenes/Bootstrap.unity` and press **Play**.
4. Move with **WASD**, arrow keys, gamepad left stick, or the on-screen joystick.
5. Build **IRON EDGE** and the **SHELTER**, then bring `3 PETAL + 3 TIMBER + 3 STONE + 1 CORE` to the labelled dormant Wayroot. Use **HOLD GATHER** / `E` / gamepad south to restore it once; restoration persists until **RESET**.
6. Use mouse wheel or touch pinch to zoom. Use **HOLD ATTACK** / Space for combat. Press **Escape** or **PAUSE** to toggle pause.
7. Use **RESET** to clear the local prototype progression save.

## Automated validation

```bash
# Git Bash, from the project root
PROJECT_PATH="$(cygpath -w "$PWD")"
UNITY="/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe"

"$UNITY" -batchmode -nographics -quit -projectPath "$PROJECT_PATH" -logFile "$(cygpath -w "$PWD/Logs/compile.log")"
"$UNITY" -batchmode -nographics -projectPath "$PROJECT_PATH" -runTests -testPlatform EditMode -testResults "$(cygpath -w "$PWD/TestResults/editmode.xml")" -logFile "$(cygpath -w "$PWD/Logs/editmode-tests.log")"
"$UNITY" -batchmode -nographics -projectPath "$PROJECT_PATH" -runTests -testPlatform PlayMode -testResults "$(cygpath -w "$PWD/TestResults/playmode.xml")" -logFile "$(cygpath -w "$PWD/Logs/playmode-tests.log")"
```

Phase 10 results: batch compile passed; EditMode **25/25**; PlayMode **5/5**; the Windows development review build and bounded smoke passed. See `Documentation/Phase10ManualTest.md` and the handoff for exact scope and limitations.

## Manual and mobile validation

Phase 10 includes `Documentation/Phase10ManualTest.md` for the full objective/restart/reset path and Device Simulator review guide. Physical iPhone validation remains blocked as documented in `Documentation/Phase9iPhoneBlockers.md`.

## Documentation

- `Documentation/Phase9ProductionReadiness.md` — current evidence, release/device blockers, and explicit non-claims
- `Documentation/Phase10ImplementationPlan.md` and `Documentation/Phase10ManualTest.md` — approved objective contract and manual/Device Simulator review path
- `Documentation/Phase10DecisionProposal.md` — original decision record for the now-implemented Wayroot objective
- `Documentation/Roadmap.md` — milestone status and gates
- `Documentation/OpenQuestions.md` — owner decisions still required
- `Documentation/AgentHandoff.md` — durable restart context
- `Documentation/GameDesign.md`, `Documentation/Architecture.md`, and `Documentation/ADRs/` — product and architecture context

## Current limitations

- Primitive assets, runtime-built UI, controlled content, and working title are prototype-only.
- iPhone compilation/signing, physical-device behavior/performance, App Store acceptance, and a human full-loop playtest are not yet validated.
- No networking, cloud save, analytics, purchases, Android adaptation, or backend is included.

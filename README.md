# Wayroot Exploration *(working title)*

A peaceful top-down 3D mobile exploration prototype made with Unity: gather materials, survive light combat, improve a weapon, build a shelter, befriend a creature, and explore the Sunmeadow clearing.

> **Status:** Phase 9 production-readiness and next-feature decision package is complete. The controlled Phase 0–8 slice is desktop-validated; physical-iPhone evidence and an explicit owner choice are required before Phase 10 gameplay work.

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
5. Use mouse wheel or touch pinch to zoom. Use **HOLD GATHER** / `E` / gamepad south for gathering, interaction, and befriending; use **HOLD ATTACK** / Space for combat. Press **Escape** or **PAUSE** to toggle pause.
6. Use **RESET** to clear the local prototype progression save.

## Automated validation

```bash
# Git Bash, from the project root
PROJECT_PATH="$(cygpath -w "$PWD")"
UNITY="/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe"

"$UNITY" -batchmode -nographics -quit -projectPath "$PROJECT_PATH" -logFile "$(cygpath -w "$PWD/Logs/compile.log")"
"$UNITY" -batchmode -nographics -projectPath "$PROJECT_PATH" -runTests -testPlatform EditMode -testResults "$(cygpath -w "$PWD/TestResults/editmode.xml")" -logFile "$(cygpath -w "$PWD/Logs/editmode-tests.log")"
"$UNITY" -batchmode -nographics -projectPath "$PROJECT_PATH" -runTests -testPlatform PlayMode -testResults "$(cygpath -w "$PWD/TestResults/playmode.xml")" -logFile "$(cygpath -w "$PWD/Logs/playmode-tests.log")"
```

Phase 9 results: batch compile passed; EditMode **22/22**; PlayMode **4/4**; the Windows development review build passed. See the readiness report for exact scope and limitations.

## Manual and mobile validation

- `Documentation/Phase9FullLoopPlaytest.md` — fresh-reset through gathering, combat, progression, shelter, companion, restart, and reset.
- `Documentation/Phase9iPhoneBlockers.md` — actual Mac/Xcode/signing/device prerequisites and touch/performance evidence. Windows and Device Simulator do not close this gate.

## Documentation

- `Documentation/Phase9ProductionReadiness.md` — current evidence, release/device blockers, and explicit non-claims
- `Documentation/Phase10DecisionProposal.md` — ranked post-slice choices; recommended one-Wayroot objective is **not implemented**
- `Documentation/Roadmap.md` — milestone status and gates
- `Documentation/OpenQuestions.md` — owner decisions still required
- `Documentation/AgentHandoff.md` — durable restart context
- `Documentation/GameDesign.md`, `Documentation/Architecture.md`, and `Documentation/ADRs/` — product and architecture context

## Current limitations

- Primitive assets, runtime-built UI, controlled content, and working title are prototype-only.
- iPhone compilation/signing, physical-device behavior/performance, App Store acceptance, and a human full-loop playtest are not yet validated.
- No networking, cloud save, analytics, purchases, Android adaptation, or backend is included.

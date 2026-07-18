# Wayroot Exploration *(working title)*

A peaceful top-down 3D mobile exploration game made with Unity: restore magical Wayroots while gathering, crafting, building a homestead, discovering original creatures, and optionally engaging in light combat.

> **Status:** Phase 0 foundation only. No gameplay is implemented; Phase 1 must wait for owner review.

## Technology

| Area | Choice |
|---|---|
| Engine | Unity **6000.5.4f1**, owner-approved and pinned through Phase 1 |
| Rendering | Universal Render Pipeline |
| Language | C# |
| Primary target | iPhone, landscape, touch-first |
| Development | Desktop editor builds with device-independent Input System actions |
| Source control | Git and Git LFS for approved binary source assets |

The local workstation has Unity 6000.5.4f1, which the owner approved as the project pin because Unity Hub does not offer a suitable Unity 6.0 LTS update.

## Open and validate

1. Use Unity **6000.5.4f1**. iPhone builds additionally require a Mac with the same editor, iOS Build Support, full Xcode, and configured signing.
2. Open this folder in Unity Hub and let Package Manager resolve dependencies.
3. If prompted, enable the new Input System and restart Unity.
4. Open `Assets/Game/Scenes/Bootstrap.unity`; it deliberately only logs a Phase 0 bootstrap message.

```bash
# Git Bash, after installing the exact pinned editor
"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" \
  -batchmode -nographics -quit -projectPath "$PWD" -logFile Logs/compile.log

"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" \
  -batchmode -nographics -projectPath "$PWD" \
  -runTests -testPlatform EditMode -testResults TestResults/editmode.xml \
  -logFile Logs/editmode-tests.log
```

In the Editor use **Window → General → Test Runner → EditMode → Run All**.

## Documentation

- `Documentation/GameDesign.md` — approved product direction
- `Documentation/Architecture.md` — layers, data flow, Mermaid diagram
- `Documentation/Roadmap.md` — milestones 0–9 and current status
- `Documentation/OpenQuestions.md` — decisions needing owner input
- `Documentation/Phase1ImplementationPlan.md` — proposed next milestone; not approval
- `Documentation/AgentHandoff.md` — durable Codex/Hermes restart context
- `Documentation/ADRs/` — architecture decisions

## Current limitations

- Unity 6000.5.4f1 batch compilation and the initial EditMode test completed successfully on 2026-07-18. See the exact commands in `AGENTS.md`.
- No iPhone build worker, Android tooling, or CI Unity license setup has been verified.
- Title/repository name is provisional and may change later.

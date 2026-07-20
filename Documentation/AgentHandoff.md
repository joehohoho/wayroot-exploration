# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 7 Sunmeadow visual-region slice is implemented and desktop-validated. Owner play review and physical-iPhone evidence remain pending. **Do not begin Phase 8 without explicit approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Inspect public `main` and working tree before resuming.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Phase 7 presentation:** `GameBootstrap` still runtime-builds the controlled scene, but now composes a green clearing plus meadow zone, warm diagonal footpath, east creek/shore, north tree grove, south rock garden, colored flower clusters, and warm directional/trilight/fog palette. All new world art is built-in primitive geometry/material colors; decorative props have no collider, preserving the existing player routes and controlled gameplay locations.
- **Existing loops retained:** player/touch UI, resources, slime combat/drop/respawn, merchant upgrade, shelter construction/persistence, and Mossling companion/persistence all retain their Phase 1–6 object names and locations. A `FadeableObstruction` null-renderer guard prevents a teardown `MissingReferenceException` during repeated PlayMode scene loads.
- **Primary manual guide:** `Documentation/Phase7ManualTest.md` covers visual landmarks, all inputs, reset/persistence spot checks, and the explicit Phase 7 scope boundary.
- **Automation:** final Unity batch compile passed. EditMode passed **22/22**. PlayMode passed **3/3**, including the new visual-landmark composition test and both existing companion/composition tests. The Windows development review player built successfully at ignored `Builds/Phase7Review/WayrootPhase7.exe` (**667,648-byte exe**); an eight-second GPU-backed smoke run timed out as intended (**124**) and its log contains no `Exception`, `Error`, `Missing`, or shader failure.
- **Build entry point:** `Wayroot.Editor.PhaseSevenBuild.BuildWindowsReviewPlayer` produces the ignored Windows review player. Unity rewrites whitespace-only values in `ProjectSettings/ProjectSettings.asset` during batch work; revert that incidental file before committing.

## First actions when resuming

1. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `Documentation/OpenQuestions.md`, `Documentation/Phase7ImplementationPlan.md`, and `Documentation/Phase7ManualTest.md`.
2. Inspect `git status --short --branch`, last five commits, and Unity installation. Ensure generated `Builds/`, `Logs/`, `TestResults/`, `Library/`, and whitespace-only `ProjectSettings.asset` changes are not staged.
3. Confirm the pushed remote `main` equals local `HEAD` before accepting any new work.
4. Await owner review of the Phase 7 scene. No Phase 8 work is authorized.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the approved Phase 7 Sunmeadow visual-region slice. Verify claims using Unity execution and result XML. Do not implement Phase 8 without explicit owner approval. Update this handoff before stopping.

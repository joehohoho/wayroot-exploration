# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 6 creature companion vertical slice is implemented and desktop-validated. Owner play review and physical-iPhone evidence remain pending. **Do not begin Phase 7 without explicit approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Inspect public `main` and working tree before resuming.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Phase 1 composition:** `GameBootstrap` runtime-builds the controlled primitive Sunmeadow test scene, including player, fixed camera, touch uGUI, safe area, EventSystem, and obstruction test tree. The URP pipeline asset is source-controlled at `Assets/Game/Settings/WayrootPrototypeRenderPipeline.asset`; `Wayroot → Repair Phase 1 Rendering` repairs its assignment if settings are reset.
- **Phases 2–5 retained:** semantic keyboard/gamepad/touch gathering and attack, three controlled resource nodes, bounded inventory, slime combat/drop/respawn, merchant weapon upgrade, shelter build, HUD feedback, reset affordance, and versioned local save.
- **Phase 6 loop:** `Friendly Mossling (hold E)` is a visually distinct green sphere with yellow ears and tail at runtime. It accepts the existing E / gamepad south / touch HOLD GATHER semantic interaction once in range. `PrototypeCreatureController` persists befriending through `creatureBefriended` in version-4 local save, follows at a 1.5-unit buffer without colliders, pauses with the player, and restores at `(-4.1, 0.6, -4.1)` shelter home until approached after restart. RESET deletes the save and restores the wild creature.
- **Primary manual guide:** `Documentation/Phase6ManualTest.md` covers desktop/gamepad/touch befriending, safe follow, pause, persistence/reload, reset, and regression controls.
- **Automation:** Phase 6 batch compile passed. EditMode passed **22/22**; PlayMode passed **2/2**, including the saved companion shelter-restore and reset case. The Windows development player build succeeded at ignored `Builds/Phase6Review/WayrootPhase6.exe` (**158 MB directory / 667,648-byte exe**) and a timed eight-second smoke run exited only via expected timeout (**124**) with no `Exception`, `Error`, `Missing`, or shader entries in captured stdout.
- **Build entry point:** `Wayroot.Editor.PhaseSixBuild.BuildWindowsReviewPlayer` produces the ignored Windows review player. Unity rewrites whitespace-only values in `ProjectSettings/ProjectSettings.asset` during batch work; revert that incidental file before committing.

## First actions when resuming

1. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `Documentation/OpenQuestions.md`, `Documentation/Phase6ImplementationPlan.md`, `Documentation/Phase6ManualTest.md`, and relevant ADRs.
2. Inspect `git status --short --branch`, last five commits, and Unity installation. Ensure generated `Builds/`, `Logs/`, `TestResults/`, `Library/`, and whitespace-only `ProjectSettings.asset` changes are not staged.
3. Confirm the pushed remote `main` equals local `HEAD` before accepting any new work.
4. Await owner review of the Phase 6 loop. No Phase 7 work is authorized.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the approved Phase 6 creature companion slice. Verify claims using Unity execution and result XML. Do not implement Phase 7 without explicit owner approval. Update this handoff before stopping.

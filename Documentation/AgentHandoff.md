# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 5 homestead/building vertical slice is implemented and desktop-validated. Owner play review and physical-iPhone evidence remain pending. **Do not begin Phase 6 without explicit approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Inspect public `main` and working tree before resuming.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Phase 1 composition:** `GameBootstrap` runtime-builds the controlled primitive Sunmeadow test scene, including player, fixed camera, touch uGUI, safe area, EventSystem, and obstruction test tree. The URP pipeline asset is source-controlled at `Assets/Game/Settings/WayrootPrototypeRenderPipeline.asset`; `Wayroot → Repair Phase 1 Rendering` repairs its assignment if settings are reset.
- **Phase 2–4 systems retained:** semantic keyboard/gamepad/touch gathering and attack, three controlled resource nodes, bounded inventory, slime combat/drop/respawn, merchant weapon upgrade, HUD feedback, reset affordance, and versioned local save.
- **Phase 5 loop:** the visible `Shelter Build Plot (hold E)` accepts the existing Interact/Gather command in range. The only blueprint costs **3 TIMBER + 3 STONE**. `ShelterBuildRules` guards the one-time fixed cost; `PrototypeGatheringController` spends both only after the full preflight succeeds, sets `shelterBuilt`, and saves it in version-3 local state. `PrototypeBuildController` restores or reveals the primitive brown-wall/red-roof `Built Shelter`; RESET deletes the save and removes it.
- **Primary manual guide:** `Documentation/Phase5ManualTest.md`; it covers insufficient funds, build, duplicate interaction, persistence/reload, reset, desktop/gamepad/touch controls, and Device Simulator expectations.
- **Automation:** final batch compile passed. EditMode passed **19/19**; PlayMode passed **1/1**. The Windows development player build succeeded at ignored `Builds/Phase5Review/WayrootPhase5.exe` (**158 MB directory / 667,648-byte exe**) and ran under an eight-second timeout without `Exception`, `Error`, `Missing`, or shader messages in captured player output.
- **Build entry point:** `Wayroot.Editor.PhaseFiveBuild.BuildWindowsReviewPlayer` produces the ignored Windows review player. Unity rewrites whitespace-only values in `ProjectSettings/ProjectSettings.asset` during batch work; revert that incidental file before committing.

## First actions when resuming

1. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `Documentation/OpenQuestions.md`, `Documentation/Phase5ImplementationPlan.md`, `Documentation/Phase5ManualTest.md`, and relevant ADRs.
2. Inspect `git status --short --branch`, last five commits, and Unity installation. Ensure generated `Builds/`, `Logs/`, `TestResults/`, `Library/`, and whitespace-only `ProjectSettings.asset` changes are not staged.
3. Confirm the pushed remote `main` equals local `HEAD` before accepting any new work.
4. Await owner review of the Phase 5 loop. No Phase 6 work is authorized.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the approved Phase 5 homestead/building slice. Verify claims using Unity execution and result XML. Do not implement Phase 6 without explicit owner approval. Update this handoff before stopping.

# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 4 progression vertical slice is implemented and desktop-validated. Owner play review and physical-iPhone evidence remain pending. **Do not begin Phase 5 without explicit approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Inspect public `main` and working tree before resuming.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Phase 1 composition:** `GameBootstrap` runtime-builds the controlled primitive Sunmeadow test scene, including player, fixed camera, touch uGUI, safe area, EventSystem, and obstruction test tree. The URP pipeline asset is source-controlled at `Assets/Game/Settings/WayrootPrototypeRenderPipeline.asset`; `Wayroot → Repair Phase 1 Rendering` repairs its assignment if settings are reset.
- **Phase 2/3 systems retained:** semantic keyboard/gamepad/touch gathering and attack, three controlled resource nodes, bounded inventory, one slime with health/chase/respawn, combat health HUD, reset affordance, and versioned local save.
- **Phase 4 loop:** defeating the slime grants exactly one saved `SlimeCore`. The visible gold `Iron Edge Merchant Station (hold E)` accepts the existing Interact/Gather command once in range. Its only upgrade costs **1 PETAL + 1 CORE**, is capped at weapon level 1, persists as `weaponLevel`, and changes damage from **ATK 1** to **ATK 2**. HUD presents resources, CORE, weapon level, damage, cost, and purchase feedback.
- **Primary manual guide:** `Documentation/Phase4ManualTest.md`; it includes reset, desktop/touch controls, persistence, and expected three-hit upgraded slime defeat.
- **Automation:** final Unity batch compile passed; EditMode passed **17/17**; PlayMode passed **1/1**. The Windows development player build succeeded at ignored `Builds/Phase4Review/WayrootPhase4.exe` (158 MB) and ran for eight seconds without a logged exception or missing-shader error.
- **Build entry point:** `Wayroot.Editor.PhaseFourBuild.BuildWindowsReviewPlayer` produces the ignored Windows review player. Unity rewrites whitespace-only values in `ProjectSettings/ProjectSettings.asset` during batch work; revert that incidental file before committing.

## First actions when resuming

1. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `Documentation/OpenQuestions.md`, `Documentation/Phase4ImplementationPlan.md`, `Documentation/Phase4ManualTest.md`, and relevant ADRs.
2. Inspect `git status --short --branch`, last five commits, and Unity installation. Ensure generated `Builds/`, `Logs/`, `TestResults/`, `Library/`, and whitespace-only `ProjectSettings.asset` changes are not staged.
3. Confirm the pushed remote `main` equals local `HEAD` before accepting any new work.
4. Await owner review of the Phase 4 loop. No Phase 5 work is authorized.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the approved Phase 4 progression slice. Verify claims using Unity execution and result XML. Do not implement Phase 5 without explicit owner approval. Update this handoff before stopping.

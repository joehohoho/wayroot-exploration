# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-19 status

- **Current milestone:** Phase 2 controlled gathering loop is implemented and desktop-validated. Physical-iPhone evidence remains pending; do **not** start Phase 3 without explicit approval.
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`; public `main` must be inspected before resuming.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Phase 1 composition:** `GameBootstrap` runtime-builds a controlled primitive Sunmeadow test scene: player, fixed follow camera, a single explicit fadeable tree, uGUI joystick/pause/overlay, safe-area layout, and EventSystem. The URP pipeline asset is source-controlled at `Assets/Game/Settings/WayrootPrototypeRenderPipeline.asset`; `Wayroot → Repair Phase 1 Rendering` repairs its assignment if project settings are reset. It is intentionally disposable prototype presentation, not final scene authoring.
- **Input boundary:** `PrototypeInputReader` owns keyboard/arrows, gamepad stick, mouse-wheel zoom, and virtual joystick input. `PrototypePlayerController` owns movement/facing. UI callbacks never contain movement rules.
- **Phase 2 composition:** three controlled gathering nodes (wildflower/tree/stone), semantic keyboard/gamepad/touch interaction, cadence-limited gathering, bounded inventory HUD, and a versioned local gathered-state prototype save. `Documentation/Phase2ManualTest.md` covers manual behavior and reset steps.
- **Validation:** Phase 2 EditMode and PlayMode suites passed; a Windows development build passed. Physical iPhone evidence remains pending.
- **Manual validation:** follow `Documentation/Phase1ManualTest.md`. Windows cannot produce a signed iPhone build; a matching Mac/Xcode worker remains required.

## First actions when resuming

1. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `OpenQuestions.md`, Phase 1 plan, manual test guide, and ADR-007.
2. Inspect `git status --short --branch`, the last five commits, and the actual Unity installation.
3. If Phase 1 has not yet been published, finish source/diff/secret checks, commit/push, and verify remote `main` equals local `HEAD`.
4. Wait for the owner’s Phase 1 review before proposing any Phase 2 plan.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the Phase 1 scope. Verify all claims using Unity execution and result XML. Do not implement Phase 2 without explicit owner approval. Update this handoff before stopping.

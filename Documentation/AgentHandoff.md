# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 15 Mossling resource-guide utility is implemented and desktop-validated. **Do not begin Phase 16 without explicit owner approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Phase 15 approval changes in `AGENTS.md` and `Documentation/Phase15ImplementationPlan.md` are intentional and must ship with this milestone.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Implementation:** `MosslingGuideRules` makes a stable nearest available flower/tree/stone decision and falls back to the nearest renewing node only when none are available. `MosslingResourceGuide` gives a befriended companion a small pulsing leaf/spark marker and motes for available targets; it hides the marker for renewing nodes and presents a compact countdown in the retained HUD. It observes the existing live node/renewal state, so gathering and renewal retarget safely. It adds no combat, auto-gathering, mount, skills, stats, or loot behavior.
- **Tests:** The red-first pure targeting test initially failed compilation because guide rules did not exist. After implementation, batch compile passed; EditMode **37/37** and PlayMode **12/12** passed. PlayMode covers befriended guide composition, retarget after gathering, and renewal-only countdown without a depleted-node marker.
- **Windows review:** `Wayroot.Editor.PhaseFifteenBuild.BuildWindowsReviewPlayer` passed, creating ignored `Builds/Phase15Review/WayrootPhase15.exe` (667,648 bytes; 158 MB directory). Eight-second smoke stayed alive until intentional timeout 124 with no captured application exception, error, or missing-shader text.
- **Manual review:** follow `Documentation/Phase15ManualTest.md` for the visible guide, reset, and persistence route. Windows evidence is not physical-iPhone validation.
- **Known environment warnings:** Unity retains pre-existing LicenseClient handshake/access-token messages and a `TagManager.asset` parser warning despite successful compilation/tests/build. Restore Unity-generated `ProjectSettings.asset` whitespace churn before staging.

## First actions when resuming

1. Inspect `git status --short --branch`, `git diff --check`, `git log --oneline -5`, and compare `HEAD` to `origin/main`.
2. Stage only source/docs/meta files (not `Builds/`, `Logs/`, `TestResults/`, `Library/`, or generated settings churn), then inspect the staged diff and commit/push the cohesive Phase 15 milestone.
3. Fetch and verify `HEAD == origin/main`; replace this pending note with the published SHA.
4. Physical iPhone validation remains independently blocked by the documented Mac/Xcode/signing/device requirements.

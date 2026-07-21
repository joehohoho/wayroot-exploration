# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-21 status

- **Current milestone:** Phase 20 Gentle Journey Guidance is implemented and desktop-validated. **Do not begin Phase 21 without explicit owner approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Phase 20 scope guard (`AGENTS.md`) and implementation plan are owner-authorized and intentionally tracked.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Implementation:** `JourneyGuidanceRules` is pure and selects exactly one existing target solely from `PrototypeGatheringSave`: meadow resource/merchant, shelter, Wayroot, Thorn Guardian, and Bloomwell; Bloomwell completion produces a non-directive free-explore state. `JourneyGuidanceController` refreshes that save-derived state, uses the existing target transforms, and only displays its subtle firefly when the target is off-camera. It alters no existing unlock, interaction, reward, inventory, or movement behavior.
- **Coverage:** `JourneyGuidanceRulesTests` covers fresh, merchant-ready, each later target, and completion; `JourneyGuidancePlayModeTests` covers each saved state across scene restart, completed no-pointer behavior, and RESET back to the fresh journey.
- **Manual review:** follow `Documentation/Phase20ManualTest.md`; desktop evidence remains distinct from physical-iPhone validation.
- **Known environment warnings:** Unity retains LicenseClient handshake/access-token messages and older nullable-annotation/obsolete API warnings in existing code. Do not stage generated `Logs/`, `TestResults/`, `Builds/`, `Library/`, or Unity-generated `ProjectSettings` whitespace churn.

## First actions when resuming

1. Inspect `git status --short --branch`, `git diff --check`, and staged diff.
2. Run the required Unity compile/EditMode/PlayMode checks and `Wayroot.Editor.PhaseTwentyBuild.BuildWindowsReviewPlayer` only after any source change.
3. Physical-iPhone validation remains independently blocked by the documented Mac/Xcode/signing/device requirements.

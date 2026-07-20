# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 10 first Wayroot restoration objective is implemented and verified. **Do not begin Phase 11 without explicit owner approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. This phase must be committed/pushed after the final diff review.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Phase 10 contract:** one dormant labelled Wayroot in the existing Sunmeadow clearing restores via the existing `E` / gamepad-south / **HOLD GATHER** interaction only after **IRON EDGE**, a built **SHELTER**, and exactly `3 PETAL + 3 TIMBER + 3 STONE + 1 CORE`. It spends safely once, persists across restart, and RESET returns it to dormant. A primitive green bloom/label provides the only new clearing presentation.
- **Persistence:** `PrototypeGatheringSave` is version 5; old save JSON omits `wayrootRestored`, which defaults to `false` through `JsonUtility`. `PrototypeGatheringSaveService.Reset()` deletes the record, clearing Wayroot state.
- **Tests:** pure `WayrootRestorationRulesTests` cover prerequisites, fixed one-time spending, and non-spending failure. PlayMode coverage saves restored state, reloads Bootstrap, asserts bloom/label composition, then asserts RESET state.
- **Manual/device guide:** `Documentation/Phase10ManualTest.md` documents fresh-reset objective validation, restart/reset behavior, and a Device Simulator review procedure. Device Simulator remains desktop evidence only.
- **Phase 10 automation:** batch compile passed; EditMode **25/25**; PlayMode **5/5**. `Wayroot.Editor.PhaseTenBuild.BuildWindowsReviewPlayer` passed and produced ignored `Builds/Phase10Review/WayrootPhase10.exe` (667,648-byte executable; 158 MB directory). Its eight-second Windows smoke stayed alive until intentional timeout 124 with no captured application exception, error, or missing-shader text.
- **Environment note:** Unity logs still contain transient LicenseClient handshake/access-token messages despite all successful exits, and existing nullable/obsolete API compiler warnings. Treat licensing as local/CI follow-up; no Phase 10 compile error occurred.
- **iPhone status:** blocked. Windows/Device Simulator evidence does not validate iOS compilation, signing, physical touch behavior, safe area, performance, persistence, or App Store readiness. No Mac/Xcode/iOS Build Support/signed-device result is available.
- **Git hygiene:** generated `Builds/`, `Logs/`, `TestResults/`, `Library/`, etc. are ignored. Unity rewrites `ProjectSettings/ProjectSettings.asset`; restore that whitespace/settings churn before committing.

## First actions when resuming

1. Inspect `git status --short --branch`, `git log --oneline -5`, then compare local `HEAD` to `origin/main`.
2. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `Documentation/OpenQuestions.md`, `Documentation/Phase10ImplementationPlan.md`, and `Documentation/Phase10ManualTest.md`.
3. If Phase 10 is not yet published, review staged diff, run the documented checks after any code change, commit, push, fetch, and compare `HEAD` to `origin/main`.
4. Obtain explicit Phase 11 authorization before new gameplay. For iPhone work, complete `Documentation/Phase9iPhoneBlockers.md` with real Mac/Xcode/signed-device evidence.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the approved Phase 0–10 controlled slice. Phase 10’s finite Wayroot objective is complete: do not expand it into resource renewal, crafting, quest trees, regions, or repeatable loops without a new owner-approved phase. Use actual Unity execution; do not claim a Device Simulator or Windows pass is physical-iPhone validation.

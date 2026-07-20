# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 9 production-readiness, playtest, iPhone-blocker, and next-feature decision package is complete. **Do not begin Phase 10 gameplay expansion without explicit owner approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Inspect public `main` and working tree before resuming.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, uGUI **2.0.0**. `activeInputHandler: 1` is required.
- **Current slice:** the runtime-built Bootstrap scene retains Sunmeadow presentation, touch UI, gathering/inventory, slime combat/drop/respawn, merchant weapon upgrade, shelter/persistence, and Mossling companion/persistence. It is a controlled primitive-asset prototype, not an iOS-certified release.
- **Phase 9 documents:** read `Phase9ProductionReadiness.md`, `Phase9FullLoopPlaytest.md`, `Phase9iPhoneBlockers.md`, and `Phase10DecisionProposal.md` before proposing further work.
- **Automation completed in Phase 9:** Unity batch compile passed; EditMode **22/22**; PlayMode **4/4**. `Wayroot.Editor.PhaseEightBuild.BuildWindowsReviewPlayer` passed and produced ignored `Builds/Phase8Review/WayrootPhase8.exe` (667,648 bytes; 158 MB directory). The player survived the intentional eight-second smoke timeout (124) and captured output had no application exception, missing-shader, or error text.
- **Environment note:** Unity logs reported LicenseClient handshake/access-token messages while compile/tests/build returned success. Treat that as a local/CI licensing follow-up, not proof of a product error.
- **iPhone status:** blocked. Windows evidence does not validate iOS compilation/signing, physical touch behavior, safe area, performance, persistence, or App Store readiness. Apple team/profile fields are empty and automatic signing is disabled; no Mac/Xcode/iOS Build Support or signed device result was available.
- **Git hygiene:** generated `Builds/`, `Logs/`, `TestResults/`, `Library/`, etc. are ignored. Unity may rewrite whitespace-only `ProjectSettings/ProjectSettings.asset` during batch work; restore it before committing.

## First actions when resuming

1. Read `AGENTS.md`, this handoff, `Documentation/Roadmap.md`, `Documentation/OpenQuestions.md`, and all four Phase 9 documents.
2. Inspect `git status --short --branch`, last five commits, and verify `origin/main` equals local `HEAD` before work.
3. Obtain an explicit owner Phase 10 selection, or follow the owner-approved physical-iPhone validation path only.
4. For iPhone work, complete prerequisites and record every real device result; do not claim a simulator/desktop pass closes the gate.

## Resume prompt

> Preserve Unity 6000.5.4f1 and the approved Phase 0–8 controlled slice. Phase 9 is documentation/validation complete, not an authorization to implement Phase 10. Use the published readiness report and actual Unity execution; close physical-iPhone blockers only with real Mac/Xcode/signed-device evidence.

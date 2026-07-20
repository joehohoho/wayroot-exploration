# Phase 9 Production-Readiness Report

**Assessment date:** 2026-07-20

**Scope:** evidence-based readiness review of the approved Phase 0–8 controlled vertical slice. This is **not** an iOS release certification and does not authorize Phase 10.

## Decision summary

| Area | Status | Evidence / consequence |
|---|---|---|
| Desktop prototype reproducibility | **Pass** | Unity 6000.5.4f1 batch compile, 22 EditMode tests, 4 PlayMode tests, and the Phase 8 Windows development player all completed successfully in this Phase 9 review. |
| Core-loop automated coverage | **Pass, bounded** | Rules tests cover movement, gathering, inventory, combat, weapon upgrade, shelter, creature follow, zoom, project identity, and URP. PlayMode composition tests cover the Bootstrap scene, Sunmeadow landmarks, companion composition, and mobile HUD identifiers. Manual end-to-end behavior still requires a human tester. |
| Windows review build | **Pass** | `Wayroot.Editor.PhaseEightBuild.BuildWindowsReviewPlayer` produced ignored `Builds/Phase8Review/WayrootPhase8.exe` (667,648-byte executable; 158 MB directory). An eight-second launch remained alive until the deliberate timeout (124); its captured standard output contained no application exception or shader failure. |
| Primary iPhone target | **Blocked** | No Mac/Xcode/iOS Build Support, Apple team, provisioning profile, generated Xcode project, signed build, or physical-device result is available from this Windows review. See `Phase9iPhoneBlockers.md`. |
| Release/package readiness | **Not ready** | This is a primitive-asset prototype with a provisional title/bundle identity and no completed iOS signing/device/performance/accessibility/release-artifact review. |
| Repository safety | **Pass for this review** | `.gitignore` excludes Unity generated paths, build output, test results, `.env*`, and signing formats. Tracked-file scan found no ignored build/cache paths, `.env`, or signing material; no tracked blob exceeded 5 MB. |

## Validated configuration

- **Editor pin:** `ProjectSettings/ProjectVersion.txt` is Unity **6000.5.4f1** (`d550df8bd089`).
- **Packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**, and uGUI **2.0.0** in `Packages/manifest.json`.
- **Input/presentation:** `activeInputHandler: 1`; landscape rotations are enabled and portrait rotations disabled. The iPhone target is `com.joehohoho.wayrootexploration`, minimum iOS is 15.0, fullscreen is required, and iPhone multi-threaded rendering is enabled. These are configuration observations, not device validation.
- **Persistence/reset:** `PrototypeGatheringSaveService` writes one JSON save atomically using a temporary file and `Reset()` deletes it. The record includes depleted nodes, resource counts, weapon level, shelter state, and companion state. The manual checklist verifies restart and reset behavior in a player.
- **Scope boundary:** no source scan hit `PlayerPrefs`, networking, Addressables, analytics, IAP, `Resources.Load`, `GameObject.Find`, `FindObjectOfType`, or `async void` in tracked game code. This is a hygiene signal, not a security audit.

## Phase 9 execution evidence

All commands ran from the project root with the installed Windows editor; generated Logs, TestResults, and Builds remain ignored.

| Check | Result |
|---|---|
| Batch compile | Exit 0 |
| EditMode test run | Exit 0; **22 passed, 0 failed, 0 skipped** |
| PlayMode test run | Exit 0; **4 passed, 0 failed, 0 skipped** |
| `PhaseEightBuild.BuildWindowsReviewPlayer` | Exit 0; Unity reported `Build Successful` |
| Windows player smoke | Process intentionally stopped after 8 seconds (`timeout` exit 124); no app exception, missing-shader, or error text in captured output |
| `git diff --check` / artifact hygiene | Required again immediately before commit; no generated artifact is part of this package |

Unity emitted LicenseClient handshake/access-token messages in compile, test, and build logs, but the editor returned 0 and generated successful test/build results. Treat this as an environment follow-up, not a product failure; recheck licensing before unattended CI is introduced.

## Residual risks and required gates

1. **Physical iPhone gate — blocking:** complete the checklist in `Phase9iPhoneBlockers.md`, then run the full loop on at least one supported physical iPhone in landscape. Capture device, iOS version, build number, tester, result, and a short profiler observation.
2. **Human usability gate — blocking for a release decision:** execute `Phase9FullLoopPlaytest.md` from a fresh reset using touch. Automated composition tests cannot prove control comfort, text readability, safe-area behavior, or progression comprehension.
3. **Release identity/content gate — blocking:** owner must settle the working title and bundle identity before any public store/release packaging; replace primitive prototype presentation as a separately approved scope.
4. **Scope gate — blocking:** select a Phase 10 proposal in `Phase10DecisionProposal.md` before any new major mechanic is implemented.

## Explicit non-claims

This report does not claim iOS compilation, signing, App Store eligibility, physical-device performance, accessibility compliance, crash-free production telemetry, or a completed human playtest. Those claims require the blocked platform/tooling and the documented manual evidence.

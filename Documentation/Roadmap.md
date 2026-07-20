# Roadmap

## Current milestone

**Phase 10 — first Wayroot restoration objective is implemented and desktop-validated. Physical-iPhone evidence and release identity/signing remain open. Do not begin Phase 11 without explicit owner approval.**

The project remains pinned to Unity **6000.5.4f1**. Phase 1 passed desktop owner play review; physical-iPhone evidence is an independent gate for the complete controlled slice.

## Phases

| Phase | Scope | Status |
|---|---|---|
| 0 | Repository, URP foundation, assemblies, bootstrap, docs, ADRs, editor validation | Complete |
| 1 | Handcrafted scene, movement, camera, touch/desktop input, safe area, obstruction fading | Implemented; desktop-validated; iPhone pending |
| 2 | Controlled gathering, bounded inventory, pickup feedback, local gathered-state save | Implemented; desktop-validated; iPhone pending |
| 3 | Combat, stats, sword, target selection, enemy, defeat/respawn | Implemented; desktop-validated; iPhone pending |
| 4 | Merchant weapon upgrade using slime drop and local persistence | Implemented; desktop-validated; owner review pending |
| 5 | Homestead and shelter building | Implemented; desktop-validated; owner review pending |
| 6 | Creature vertical slice | Implemented; desktop-validated; owner review pending |
| 7 | Sunmeadow visual region | Implemented; desktop-validated; owner review pending |
| 8 | Mobile vertical-slice polish | Implemented; desktop-validated; physical-iPhone evidence pending |
| 9 | Production readiness, full-loop playtest, iPhone blockers, and next-feature decision proposal | **Complete; device/release gates explicitly pending** |
| 10 | One persistent first Wayroot restoration objective in Sunmeadow | **Implemented; desktop-validated** |

## Phase 10 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **25 passed, 0 failed, 0 skipped**, including the pure Wayroot prerequisite and safe-spending rules.
- PlayMode: **5 passed, 0 failed, 0 skipped**, including restored-state Bootstrap composition and RESET coverage.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseTenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase10Review/WayrootPhase10.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- Device Simulator: a landscape/safe-area manual review procedure is in `Documentation/Phase10ManualTest.md`. It is not physical-iPhone evidence.

## Phase 9 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **22 passed, 0 failed, 0 skipped**.
- PlayMode: **4 passed, 0 failed, 0 skipped**.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseEightBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase8Review/WayrootPhase8.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- Repository scan: ignored build/cache/test paths, `.env*`, and signing formats are excluded; tracked-file scan found no generated artifacts or signing material and no tracked blob above 5 MB.
- Unity logs contained LicenseClient handshake/access-token environment messages despite successful editor/test/build exits. Recheck licensing before unattended CI.

See `Documentation/Phase9ProductionReadiness.md` for scope, commands/results, residual risks, and non-claims.

## Next milestone gate

1. Owner reviews the recommended **one Wayroot restoration objective** and alternatives in `Documentation/Phase10DecisionProposal.md`, then explicitly selects/defer a Phase 10 slice.
2. Before a release-quality or mobile-ready claim, complete `Documentation/Phase9iPhoneBlockers.md` and the touch-only `Documentation/Phase9FullLoopPlaytest.md` on physical iPhone hardware.

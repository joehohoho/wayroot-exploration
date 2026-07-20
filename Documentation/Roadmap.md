# Roadmap

## Current milestone

**Phase 14 — warm stylized magical-world art-direction pass is implemented and desktop-validated.**

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
| 11 | Bounded persisted renewal return loop for the three existing gathering nodes | **Implemented; desktop-validated** |
| 12 | Shelter rest and active return point | **Implemented; owner-confirmed** |
| 13 | One Wayroot-gated restored-grove Thorn Guardian encounter | **Implemented; desktop-validated** |
| 14 | Cohesive warm stylized magical-world art direction using original runtime geometry, URP materials, and lighting | **Implemented; desktop-validated** |

## Phase 14 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **33 passed, 0 failed, 0 skipped**. PlayMode: **11 passed, 0 failed, 0 skipped**, including the two new Phase 14 visual-composition/readability checks for the player, creature, enemy, creek, merchant, Wayroot, and compact world-label anchors.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseFourteenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase14Review/WayrootPhase14.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- `Documentation/Phase14ManualTest.md` covers the visual pass and retained-loop regression route. This remains desktop evidence, not physical-iPhone validation.

## Phase 13 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **33 passed, 0 failed, 0 skipped**, including pure Wayroot-gate and guardian-profile rules.
- PlayMode: **9 passed, 0 failed, 0 skipped**, including locked/unlocked Bootstrap composition and guardian profile coverage.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseThirteenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase13Review/WayrootPhase13.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- `Documentation/Phase13ManualTest.md` covers the Wayroot gate, guardian combat/core reward, fixed respawn, shelter return regression, restart, and RESET. This remains desktop evidence, not physical-iPhone validation.

## Phase 11 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **29 passed, 0 failed, 0 skipped**, including pure deadline, due-boundary, and legacy JSON migration rules.
- PlayMode: **6 passed, 0 failed, 0 skipped**, including a saved expired renewal deadline restoring a runtime-composed node and RESET cleanup.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseElevenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase11Review/WayrootPhase11.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- `Documentation/Phase11ManualTest.md` covers the fixed 20-second renewal, restart-before/after-deadline, and RESET checks. This remains desktop evidence, not physical-iPhone validation.

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

1. Owner reviews the completed Phase 14 art-direction pass and explicitly selects or defers the next bounded slice; do not begin Phase 15 automatically.
2. Before a release-quality or mobile-ready claim, complete `Documentation/Phase9iPhoneBlockers.md` and the touch-only `Documentation/Phase9FullLoopPlaytest.md` on physical iPhone hardware.

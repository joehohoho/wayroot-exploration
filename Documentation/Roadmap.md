# Roadmap

## Current milestone

**Phase 32 — Accessibility and clarity pass is implemented and desktop-validated.**

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
| 15 | Befriended Mossling's non-combat nearest-resource guide with renewal fallback | **Implemented; desktop-validated** |
| 16 | Cozy magical procedural ambient bed, event feedback, and persistent SOUND preference | **Implemented; desktop-validated** |
| 17 | One persistent Moonlit Glade exploration pocket opened by the first Thorn Guardian victory | **Implemented; desktop-validated** |
| 18 | Major original runtime-composed stylized graphics and procedural animation pass | **Implemented; desktop-validated** |
| 19 | Guardian-gated Moonlit Bloomwell restoration finale using existing renewable resources | **Implemented; desktop-validated** |
| 20 | Compact save-derived Journey status and off-screen firefly guide for the existing route | **Implemented; desktop-validated** |
| 21 | Dodge and combat readability | **Implemented; desktop-validated** |
| 22 | Alpha visual, camera, and HUD overhaul | **Implemented; desktop-validated** |
| 23 | Animated original sprite character presentation | **Implemented; desktop-validated** |
| 24 | Actor presentation cleanup, readable fallback silhouettes, and player-build sprite-material reliability | **Implemented; desktop-validated; owner-confirmed** |
| 25 | Exploration art and mobile composition pass for existing Sunmeadow, Grove, Glade, and Bloomwell route | **Implemented; desktop-validated** |
| 26 | Combat encounter polish for the existing Practice Slime and Thorn Guardian | **Implemented; desktop-validated** |
| 27 | Shelter homestead cozy visual pass | **Implemented; desktop-validated** |
| 28 | Bloomwell finale presentation polish | **Implemented; desktop-validated** |
| 29 | First-session onboarding and pacing pass | **Implemented; desktop-validated** |
| 30 | Mossling companion presence pass using existing guide state | **Implemented; desktop-validated** |
| 31 | Combat feel and environmental ambience using original procedural audio, optional haptic seam, and runtime URP presentation | **Implemented; desktop-validated** |
| 32 | Compact persistent reduced-flash/reduced-motion preferences and presentation clarity improvements | **Implemented; desktop-validated** |

## Phase 20 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **49 passed, 0 failed, 0 skipped**, including pure fresh/merchant/later-milestone/completed selection rules.
- PlayMode: **22 passed, 0 failed, 0 skipped**, including saved-state target progression across Bootstrap restarts, completed no-pointer behavior, and RESET returning to the fresh guide.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseTwentyBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase20Review/WayrootPhase20.exe`.
- `Documentation/Phase20ManualTest.md` covers the safe-area Journey card, off-screen firefly behavior, retained route, persistence, and RESET. This remains desktop evidence, not physical-iPhone validation.

## Phase 18 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **42 passed, 0 failed, 0 skipped**. PlayMode: **18 passed, 0 failed, 0 skipped**, including active procedural player/Mossling/slime/water composition, root/collider preservation, world-label readability, and restore-safe single-rig coverage.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseEighteenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase18Review/WayrootPhase18.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows headless smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- `Documentation/Phase18ManualTest.md` covers the layered visual/motion review and retained-loop regression route. This remains desktop evidence, not physical-iPhone validation.

## Phase 16 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0). The only reported C# warnings are pre-existing obsolete `FindObjectsByType` overloads in an older PlayMode test.
- EditMode: **40 passed, 0 failed, 0 skipped**, including deterministic procedural-profile and default-enabled preference rules. PlayMode: **13 passed, 0 failed, 0 skipped**, including SOUND OFF restart persistence, visible label state, and RESET's enabled default.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseSixteenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase16Review/WayrootPhase16.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows headless smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text. The runtime bypasses audio-source/clip work in batch mode.
- `Documentation/Phase16ManualTest.md` covers sound toggle/persistence/reset, ambient bed, every feedback family, and retained-loop regression checks. This remains desktop evidence, not physical-iPhone validation.

## Phase 15 verification record

- Unity **6000.5.4f1** batch compile: **passed** (exit 0).
- EditMode: **37 passed, 0 failed, 0 skipped**. PlayMode: **12 passed, 0 failed, 0 skipped**, including pure target selection plus live post-gather retarget and renewal-only no-marker coverage.
- Windows development review build: **passed** through `Wayroot.Editor.PhaseFifteenBuild.BuildWindowsReviewPlayer`; ignored output is `Builds/Phase15Review/WayrootPhase15.exe` (667,648-byte executable; 158 MB directory).
- Timed Windows smoke: player remained running for the intended eight seconds (timeout 124); captured output had no application exception, missing-shader, or error text.
- `Documentation/Phase15ManualTest.md` covers fresh befriending, available-resource marker/trail, post-gather retarget, renewal countdown/no-marker behavior, restart, and RESET. This remains desktop evidence, not physical-iPhone validation.

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

1. Phase 32 is published for owner review. **Do not begin Phase 33 without explicit owner approval.**
2. Before a release-quality or mobile-ready claim, complete `Documentation/Phase9iPhoneBlockers.md` and the touch-only `Documentation/Phase9FullLoopPlaytest.md` on physical iPhone hardware.

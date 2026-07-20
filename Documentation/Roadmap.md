# Roadmap

## Current milestone

**Phase 4 — Progression vertical slice: implemented and desktop-validated; owner play review and iPhone evidence remain pending.**

Phase 1 completed desktop owner play review on 2026-07-19; physical-iPhone evidence remains an independent mobile gate. The project remains pinned to Unity **6000.5.4f1**. Phase 5 is not authorized.

## Phases

| Phase | Scope | Status |
|---|---|---|
| 0 | Repository, URP project foundation, assemblies, bootstrap, docs, ADRs, editor validation | Complete |
| 1 | Handcrafted test scene, movement, top-down camera, touch/desktop input, safe area, obstruction fading | Implemented; desktop-validated; iPhone pending |
| 2 | Controlled interaction, three prototype gathering nodes, bounded inventory, pickup feedback, and one local gathered-state save | Implemented; desktop-validated; iPhone pending |
| 3 | Basic combat, stats, sword, target selection, one enemy, defeat/respawn | Implemented; desktop-validated; iPhone pending |
| 4 | One merchant-station weapon upgrade using a slime combat drop and local persistence | Implemented; desktop-validated; owner review pending |
| 5 | Homestead and building | Deferred; not authorized |
| 6 | Creature vertical slice | Deferred |
| 7 | Generated Sunmeadow region | Deferred; only after Phases 1–4 are enjoyable in a controlled scene |
| 8 | Mobile vertical-slice polish and iPhone profiling | Deferred |
| 9 | Production planning and post-slice features | Deferred |

## Verification record

### Phase 0

- Unity 6000.5.4f1 batch compile: **passed**.
- Unity Test Framework EditMode: **1 passed, 0 failed**.
- Input System **1.19.0** with `activeInputHandler: 1` validated.

### Phase 1

- Unity batch compile: **passed**.
- EditMode tests: **8 passed, 0 failed** (includes a regression check that a render pipeline is assigned).
- PlayMode composition test: **1 passed, 0 failed**.
- Windows desktop development build: **passed**; a timed runtime smoke launch produced no runtime exception or missing-shader error.
- The URP render-pipeline asset is source-controlled at `Assets/Game/Settings/WayrootPrototypeRenderPipeline.asset`; this corrected a reported magenta-screen failure caused by an unassigned pipeline asset.

### Phase 4

- One visible gold `Iron Edge Merchant Station (hold E)` is runtime-composed into the controlled test scene.
- A defeated slime grants one saved `CORE`; the only upgrade costs **1 PETAL + 1 CORE**, advances `WEAPON 0/1` to `1/1`, and changes attack damage from **ATK 1** to **ATK 2**.
- `slimeCores` and `weaponLevel` are persisted in the version-2 prototype local save; reset clears both.
- Unity batch compile: **passed**.
- EditMode tests: **17 passed, 0 failed**.
- PlayMode composition test: **1 passed, 0 failed**.
- Windows development player build: **passed** at ignored `Builds/Phase4Review/WayrootPhase4.exe` (158 MB); it remained running for eight seconds and its player log contained no exception or missing-shader error.

## Next milestone gate

The owner must play/review the cohesive Phase 4 loop using `Documentation/Phase4ManualTest.md`. Do not begin Phase 5 without explicit approval.

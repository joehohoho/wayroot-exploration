# Roadmap

## Current milestone

**Phase 2 — Controlled interaction and gathering loop: implemented and desktop-validated; iPhone evidence remains pending.**

Phase 1 completed desktop owner play review on 2026-07-19; physical-iPhone evidence remains an independent mobile gate. The project remains pinned to Unity **6000.5.4f1**.

## Phases

| Phase | Scope | Status |
|---|---|---|
| 0 | Repository, URP project foundation, assemblies, bootstrap, docs, ADRs, editor validation | Complete |
| 1 | Handcrafted test scene, movement, top-down camera, touch/desktop input, safe area, obstruction fading | Implemented; desktop-validated; iPhone pending |
| 2 | Controlled interaction, three prototype gathering nodes, bounded inventory, pickup feedback, and one local gathered-state save | Implemented; desktop-validated; iPhone pending |
| 3 | Basic combat, stats, sword, target selection, one enemy, defeat/respawn | Deferred |
| 4 | Vertical-slice progression: weapons, enemies, equipment, skills, crafting, merchant, NPC/quest | Deferred |
| 5 | Homestead and building | Deferred |
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
- Phase 1 manual/iPhone instructions: `Documentation/Phase1ManualTest.md`.

## Next milestone gate

The owner must play/review Phase 1 and evaluate iPhone evidence when a Mac/Xcode worker is available. Do not begin Phase 2 until that review explicitly approves the movement/camera/touch loop.

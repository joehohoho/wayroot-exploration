# Roadmap

## Current milestone

**Phase 8 — Mobile playability and polish slice: implemented and desktop-validated; owner landscape-touch review and physical-iPhone evidence remain pending.**

Phase 1 completed desktop owner play review on 2026-07-19; physical-iPhone evidence remains an independent mobile gate. The project remains pinned to Unity **6000.5.4f1**. Phase 8 is not authorized.

## Phases

| Phase | Scope | Status |
|---|---|---|
| 0 | Repository, URP project foundation, assemblies, bootstrap, docs, ADRs, editor validation | Complete |
| 1 | Handcrafted test scene, movement, top-down camera, touch/desktop input, safe area, obstruction fading | Implemented; desktop-validated; iPhone pending |
| 2 | Controlled interaction, three prototype gathering nodes, bounded inventory, pickup feedback, and one local gathered-state save | Implemented; desktop-validated; iPhone pending |
| 3 | Basic combat, stats, sword, target selection, one enemy, defeat/respawn | Implemented; desktop-validated; iPhone pending |
| 4 | One merchant-station weapon upgrade using a slime combat drop and local persistence | Implemented; desktop-validated; owner review pending |
| 5 | Homestead and building | Implemented; desktop-validated; owner review pending |
| 6 | Creature vertical slice | Implemented; desktop-validated; owner review pending |
| 7 | Generated Sunmeadow region | Implemented; desktop-validated; owner review pending |
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

### Phase 5

- One gold-brown `Shelter Build Plot (hold E)` is runtime-composed in the controlled test scene. It costs **3 TIMBER + 3 STONE** through the existing semantic Interact/Gather command.
- The one-time purchase safely spends the fixed cost, persists `shelterBuilt` in the version-3 prototype local save, and reveals a primitive brown-wall/red-roof shelter. RESET clears the build state.
- Unity batch compile, EditMode, PlayMode, Windows development build, and timed player smoke were completed for the Phase 5 review build; exact results are recorded in the published handoff.

### Phase 6

- One green primitive Mossling with yellow ears is composed into the controlled scene. It reuses E/gamepad-south/touch HOLD GATHER for befriending, follows with a 1.5-unit safety buffer, and pauses safely with the player.
- `creatureBefriended` is persisted in the version-4 local save. A restart restores the creature at the shelter home until the player walks up; RESET clears it.
- Unity batch compile, EditMode, PlayMode, Windows development build, and timed player smoke are completed for the review build; exact results are recorded in the published handoff.

### Phase 7

- The runtime-controlled scene is now a coherent Sunmeadow clearing: grass/meadow zones, a warm footpath, east creek/shore, north grove, south rock garden, clustered flowers, and a warm URP/ambient palette. All props use built-in primitives/materials only and retain existing Phase 1–6 landmarks and loops.
- Unity batch compile, EditMode, PlayMode, Windows development build, and timed player smoke are completed for the review build; exact results are recorded in the published handoff.

## Next milestone gate

The owner must play/review the cohesive Phase 7 scene using `Documentation/Phase7ManualTest.md`. Do not begin Phase 8 without explicit approval.

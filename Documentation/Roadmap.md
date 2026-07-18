# Roadmap

## Current milestone

**Phase 0 — Repository and project foundation: complete and Unity-validated.**

The owner approved installed Unity **6000.5.4f1** as the project pin because Unity Hub did not offer the prior intended patch. Batch compilation and the initial Unity EditMode test passed on 2026-07-18. **Owner review is still required before Phase 1 begins.**

## Phases

| Phase | Scope | Status |
|---|---|---|
| 0 | Repository, URP project foundation, assemblies, bootstrap, docs, ADRs, editor validation | Complete; awaiting owner review |
| 1 | Handcrafted test scene, movement, top-down camera, touch/desktop input, safe area, obstruction fading | Planned; see `Phase1ImplementationPlan.md` |
| 2 | Interaction, woodcutting/mining/foraging, inventory, pickup, gathered-state save | Deferred |
| 3 | Basic combat, stats, sword, target selection, one enemy, defeat/respawn | Deferred |
| 4 | Vertical-slice progression: weapons, enemies, equipment, skills, crafting, merchant, NPC/quest | Deferred |
| 5 | Homestead and building | Deferred |
| 6 | Creature vertical slice | Deferred |
| 7 | Generated Sunmeadow region | Deferred; only after Phases 1–4 are enjoyable in a controlled scene |
| 8 | Mobile vertical-slice polish and iPhone profiling | Deferred |
| 9 | Production planning and post-slice features | Deferred |

## Phase 0 verification record

- Unity 6000.5.4f1 batch compile: **passed**.
- Unity Test Framework EditMode: **1 passed, 0 failed** (`ProjectIdentity_UsesConfiguredNamespaceRoot`).
- Input System dependency: **1.19.0** with `activeInputHandler: 1`; required and validated under the pinned editor.
- Package-resolution-generated assets and project settings were reviewed as source-controlled Unity project configuration.

## Next milestone gate

Before any Phase 1 code: owner reviews Phase 0, confirms the initial project state, and approves Phase 1. Then agents must report the implementation plan, dependencies, risks, expected files, and acceptance criteria before editing.

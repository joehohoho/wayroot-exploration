# Phase 1 Plan — Movement and Camera Prototype

**Prerequisite:** owner review of Phase 0 plus successful compile/EditMode tests with Unity 6000.5.4f1.

## Goal

A small handcrafted Sunmeadow-inspired scene with placeholder player movement from keyboard, controller-compatible Input System actions, and virtual joystick; fixed top-down camera follow; pinch zoom; obstruction fading; safe-area UI; pause; development overlay. No gathering, combat, inventory, or generation.

## Risks

| Risk | Mitigation |
|---|---|
| Pinned editor unavailable | Use the owner-approved 6000.5.4f1 project pin; future upgrades require a planned decision. |
| Touch UI undecided | Run a tiny uGUI/UI Toolkit spike, write ADR, choose one. |
| Fade allocation/performance | Non-alloc queries, cached renderers/material state, profile on device. |
| iPhone build unavailable | Establish matching Mac/Xcode worker before mobile acceptance. |

## Ordered tasks

1. **Input and command boundary** — semantic actions for keyboard/gamepad/touch adapter; UI callbacks do not contain movement rules. Verify mappings with tests/manual desktop input. Files: `Code/Input`, `Code/Character`, tests, input asset.
2. **Controlled scene/movement** — placeholder moves/faces travel direction on terrain; no jump or ordinary movement allocations. Verify Game view + Profiler. Files: scene, character scripts/prefab/tests.
3. **Camera/zoom** — fixed-angle smooth follow with pinch and desktop simulation, configurable min/max, no rotation. Verify Device Simulator/manual. Files: `Code/Camera`, data/settings/tests.
4. **Obstruction/UI/pause** — only camera obstructions fade/recover smoothly; joystick follows safe area; pause exists. Verify Profiler allocation trace, Device Simulator, iPhone.

## Acceptance checkpoint

Pinned batch compile, EditMode/needed PlayMode tests, desktop development build, and iPhone touch evidence pass; roadmap/handoff/ADRs are updated. Phase 1 manual test instructions must cover landscape Device Simulator, keyboard/controller movement, joystick/pinch, obstruction fade, 60-second allocation sampling, and physical iPhone repeat.

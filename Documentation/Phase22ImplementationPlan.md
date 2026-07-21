# Phase 22 Implementation Plan — Alpha Visual, Camera, and HUD Overhaul

**Authorization:** Owner supplied an in-editor screenshot on 2026-07-21 and requested graphics that read as a completed alpha game. This supersedes any claim that the prior Phase 18 pass alone was visually sufficient.

## Screenshot-grounded problems to fix

The supplied capture shows release-blocking prototype presentation issues:

1. World labels can appear enormous, inverted/rotated, and intersect the camera view.
2. HUD information is duplicated/overlapping across the top, obscuring the world and losing hierarchy.
3. Debug-like status (`ZOOM`, `RUNNING`, raw inventory/progression strings) competes with player-facing UI.
4. Large primitive geometry, flat ground/path blocks, repeated cylinders/spheres, and weak composition still read as greybox rather than alpha art.
5. Camera framing and control clusters do not preserve a clean playable focal area.

## Goal

Deliver one integrated alpha-quality presentation baseline: a legible camera composition, a compact mobile-safe HUD, clean semantic world markers, and a more authored-looking warm stylized environment using original source-controlled runtime geometry/materials only.

## Locked visual contract

- Replace world text labels with small screen-facing interaction markers/short names that never invert, exceed a compact scale, or cover the player/controls.
- Consolidate HUD into a clear hierarchy: health/combat compact top-left; journey/objective compact top-center; resources/progression as a restrained top-right card; contextual prompt only near relevant action; touch controls remain bottom corners.
- Remove non-player-facing debug/status text from normal review builds.
- Improve ground/path/creek/tree/resource/shelter/merchant silhouettes and environmental layering so the scene reads as a cohesive alpha rather than raw primitives.
- Retune camera position/FOV/zoom bounds and occlusion treatment for a clean focus zone around the player and nearby interaction targets.
- Preserve all Phase 1–21 mechanics, input, persistence, labels' semantic meaning, and mobile-safe touch controls.

## Boundaries

- Original runtime-composed geometry, URP settings/materials, particles, and C# only. No external asset packs, paid content, generated image assets, new mechanics, changed progression values, or Phase 23 work.
- The result must remain performant/mobile-conscious and be verified in the Game view/player, not assumed from source.

## Acceptance criteria

- A fresh 16:9 Game-view capture visibly resolves the screenshot's label inversion/overlap and HUD crowding.
- Essential information remains readable without covering the central play space.
- Existing full loop still works and no interaction target is made unclear.
- Compile, unit/PlayMode UI/composition coverage, Windows build and visual/player smoke, documentation, and Git hygiene pass together.

# Phase 7 Implementation Plan — Sunmeadow Visual Region Slice

**Authorization:** Owner approved Phase 7 after successful Phase 6 review on 2026-07-20.

## Goal

Replace the single primitive test ground with one controlled, more readable Sunmeadow-inspired region while retaining the proven Phase 1–6 loops.

## First visual-region delivery

- Replace flat single-color ground presentation with a bounded terrain-like clearing, shoreline/path color zones, and a coherent palette.
- Create clustered trees, rocks, flowers, shelter/merchant landmarks, and one companion area using generated primitive geometry/materials only.
- Improve scene lighting, sky/ambient colors, camera readability, and object silhouettes without external art packages.
- Keep the existing player, resource, combat, merchant, shelter, and creature positions discoverable and playable.
- Preserve touch safe-area UI and performance-conscious primitive counts.

## Explicit limits

- This is a handcrafted/procedural controlled region slice, not infinite generation.
- No new gameplay economies, quests, NPC dialogue, biomes, streaming, Addressables, terrain tools, or Phase 8 device profiling.
- Do not use external unapproved art assets. Final character/creature assets remain an owner decision.

## Acceptance criteria

- The scene visibly reads as a coherent clearing rather than isolated primitives.
- Existing Phase 1–6 manual loops remain testable in their documented locations.
- Existing URP assignment remains intact and no magenta/missing shader error occurs.
- Compile, EditMode, PlayMode, Windows build/smoke, manual guide, and Git hygiene pass together.

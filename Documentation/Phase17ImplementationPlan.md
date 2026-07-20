# Phase 17 Implementation Plan — Moonlit Glade Exploration Extension

**Authorization:** Owner approved the next phase after Phase 16 on 2026-07-20.

## Goal

Add one compact second exploration pocket that rewards the existing Wayroot → Restored Grove → Thorn Guardian path, expanding the world without starting a broad region system or a new economy.

## Locked player contract

- Defeating the Thorn Guardian opens a clearly visible path from the Restored Grove into the **Moonlit Glade**.
- The glade uses the existing resource types only: Wild Petal, Timber, and Stone. Its nodes participate in the established automatic renewal loop.
- It contains a distinct magical landmark and a small visual discovery reward, but no new quest chain, currency, crafting material, merchant, or fast-travel mechanism.
- Its unlocked visual state persists after restart and returns to locked on RESET.
- Existing player controls, shelter return point, Mossling guide, soundscape, and encounters continue to work in both spaces.

## Presentation

- Follow the approved warm stylized magical-world language: rounded moonlit trees/rocks, soft violet-blue glow, readable silhouettes, subtle ambient effects/audio, and a mobile-conscious compact footprint.
- The unlock must be obvious in world feedback and the existing HUD without adding a map overlay.

## Exclusions

- No procedural region system, biome generation, new inventory types, boss chain, NPC dialogue, fast travel, or Phase 18 work.

## Acceptance criteria

- Before the first Guardian victory, the glade path is visibly sealed/unavailable.
- Guardian victory opens it once, persists it, and RESET removes it.
- Existing gather/renewal/guide loops work on glade nodes.
- Compile, focused tests, PlayMode persistence/condition coverage, Windows build/smoke, documentation, and Git hygiene pass together.

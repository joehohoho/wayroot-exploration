# Phase 5 Implementation Plan — Homestead and Building Slice

**Authorization:** Owner approved Phase 5 after successful Phase 4 review on 2026-07-20.

## Goal

Add one compact, persistent homestead loop to the controlled prototype:

> Gather timber and stone → walk to a build plot → interact to construct one shelter → save its built state → receive clear visual/HUD confirmation.

## Locked scope

- One visible build plot and one shelter blueprint only.
- Cost: **3 TIMBER + 3 STONE**. Reuse the bounded inventory and local save.
- Existing Interact/Gather input activates building while within plot range; keyboard/gamepad/touch stay semantic.
- Built shelter is a primitive placeholder with readable roof/walls, not final art.
- Build status persists across Play-mode restarts and RESET clears it.
- No freeform placement, rotations, demolition, multiple plots, homestead storage, farming, crafting graph expansion, NPC housing, or Phase 6 creatures.

## Acceptance criteria

- Plot is visible and clearly explains its cost/status through HUD.
- Purchase only succeeds with sufficient resources and never double-spends.
- Constructed shelter is visibly distinct from the plot.
- Save/reload/RESET behavior is deterministic.
- Compile, EditMode, PlayMode, Windows build/smoke, manual guide, handoff, and Git hygiene are completed as one review build.

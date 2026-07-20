# Phase 6 Implementation Plan — Creature Vertical Slice

**Authorization:** Owner approved Phase 6 after successful Phase 5 review on 2026-07-20.

## Goal

Add one friendly creature companion loop to the controlled homestead prototype:

> Find one creature → interact to befriend it → it follows the player → it persists at the shelter after restart.

## Locked scope

- One primitive-friendly creature, one trust/befriend interaction, follow behavior, shelter-home restoration, and persistent befriended state.
- Reuse semantic Interact/Gather input and existing save/reset patterns.
- No breeding, creature inventory, combat pets, skill trees, multiple species, mounts, farming automation, or Phase 7 world generation.

## Graphics milestone

Prototype primitives remain intentional through Phase 6. The first focused visual-art pass is scheduled after the creature loop is proven, before Phase 7’s generated Sunmeadow region: consistent terrain palette, better material/lighting pass, readable prop silhouettes, and approved placeholder character/creature art. Final mobile art/polish remains Phase 8 after the controlled loops are enjoyable.

## Acceptance criteria

- Creature is visibly distinct, interactable, and follows safely.
- Befriended state and shelter-home state persist; RESET clears them.
- Desktop/gamepad/touch interaction all work.
- Compile, EditMode, PlayMode, Windows build/smoke, manual guide, and Git hygiene are completed together.

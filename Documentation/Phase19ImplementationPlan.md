# Phase 19 Implementation Plan — Bloomwell Restoration Finale

**Authorization:** Owner asked to proceed through the next few steps after Phase 18 on 2026-07-20. This phase delivers a compact multi-step completion arc rather than another isolated system.

## Goal

Give the Moonlit Glade a meaningful, visible purpose and establish a satisfying prototype completion state using the loops the player has already learned.

## Player arc

```text
Restore Wayroot
→ defeat Thorn Guardian
→ enter Moonlit Glade
→ gather renewable existing materials
→ activate the Bloomwell
→ Sunmeadow enters a persistent restored-finale state
```

## Locked implementation

- The Moonlit Glade's existing Bloomwell becomes interactable only after its current Guardian-gated unlock.
- Activation consumes a clearly documented, modest bundle of existing items only: **2 PETAL + 2 TIMBER + 2 STONE + 1 CORE**.
- No weapon/shelter requirement beyond the path already needed to reach the glade; no new inventory types, crafting recipe system, currency, or merchant.
- Activation persists, resets cleanly, and visibly changes the Bloomwell/Glade/Sunmeadow through magical lighting, motifs, companion response, and a compact completion state.
- The full existing return loop remains playable afterward: gathering/renewal, companion guide, combat, shelter, Wayroot, guardian, and glade.

## Presentation

- Treat this as a small magical finale, following the approved warm stylized art, animation, and sound direction.
- Make the final objective and requirement legible without a large map, quest log, or dialogue tree.

## Explicit exclusions

- No credits sequence, new region, new enemy/boss chain, map/fast travel, dialogue/NPC system, new economy, physical-device release claim, or Phase 20 work.

## Acceptance criteria

- The whole path can be completed from RESET using existing controls.
- Incomplete-state feedback names missing items; successful activation visibly and audibly communicates completion.
- Save/restart/reset state behavior and existing loops remain reliable.
- Compile, EditMode/PlayMode coverage, Windows build/smoke, documentation, and Git hygiene pass in one reviewable delivery.

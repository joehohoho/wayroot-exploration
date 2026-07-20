# Phase 8 Implementation Plan — Mobile Playability and Polish Slice

**Authorization:** Owner approved Phase 8 after successful Phase 7 visual-region review on 2026-07-20.

## Goal

Make the controlled Sunmeadow slice more legible and dependable for landscape touch play, while grouping several player-facing refinements into one review package.

## Locked delivery

- Improve touch UI hierarchy, labels, contrast, and safe-area placement for landscape phones.
- Add clear on-world identifiers for critical interactables: resource/merchant/shelter/companion/slime.
- Add concise contextual status/feedback for attack, gathering, building, merchant, companion, pause, reset, and respawn events.
- Audit/limit visual-region primitive counts and add a light performance diagnostic overlay suitable for development builds.
- Keep existing save/persistence and gameplay rules intact.

## Limits

- No iOS signing/build claim without a Mac/Xcode worker.
- No new gameplay phase, economy, region generation, external art, analytics, or Phase 9 production planning.

## Acceptance criteria

- Landscape iPhone Device Simulator layout is readable and controls do not overlap safe areas.
- All Phase 1–7 interactables are identifiable without relying on hierarchy names.
- Existing loops work under the revised UI.
- Compile, EditMode, PlayMode, Windows review build/smoke, and manual guide pass together.

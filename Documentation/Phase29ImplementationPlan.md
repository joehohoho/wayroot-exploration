# Phase 29 Implementation Plan — First-Session Onboarding and Pacing Pass

**Authorization:** Owner directed continued development after Phase 28 validation on 2026-07-23. This is the next bounded enhancement chosen to improve the existing complete route without expanding systems.

## Goal

Make a first play session easier to understand and more smoothly paced from gathering through the existing Bloomwell finale, using only existing Journey guidance, contextual feedback, landmarks, and interaction affordances.

## Scope

- Present compact, progressive first-session guidance for the existing milestones: gather, merchant/Iron Edge, shelter, Wayroot, Guardian/Grove, Moonlit Glade, Bloomwell.
- Improve context-specific wording and timing of existing guidance/feedback so it is actionable without feeling like a quest log.
- Add subtle non-blocking landmark emphasis based on current existing save state; never add a map, minimap, waypoint UI, dialogue tree, new quest, or navigation mechanic.
- Ensure post-Bloomwell state becomes peaceful/non-directive and RESET restores the fresh onboarding state.

## Boundaries

- No new economy, item, combat values, controls, region, enemy, objective chain, currency, map, dialogue, fast travel, or save-schema change.
- Preserve Journey card footprint, safe-area behavior, touch controls, actor visibility, and existing route gates.

## Acceptance

- A fresh player receives one clear next-action cue at a time through the existing route.
- Guidance stays compact, no HUD/touch-control occlusion, and no debug/prototype text.
- Existing completion persistence/RESET behavior is unchanged.
- Compile/tests/player build/smoke/actual visual review/docs/commit/push pass.

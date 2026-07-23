# Phase 30 Implementation Plan — Mossling Companion Presence Pass

**Authorization:** Owner directed continued development after verified Phase 29. This low-risk bounded enhancement strengthens an existing companion without adding mechanics.

## Goal

Make the existing Mossling guide feel more alive, legible, and magical at mobile scale while preserving its current nearest-resource/renewal guidance role.

## Scope

- Improve Mossling idle, follow, guide, and arrival presentation using existing guide state only.
- Add compact state-specific visual signals: a gentle guide glow/pointer when leading, a restrained wait/renewal cue, and a pleased arrival acknowledgement.
- Improve silhouette/readability through original runtime-composed accents without replacing its gameplay root/collider or adding a second companion.
- Maintain clear camera/HUD/touch-control composition.

## Boundaries

- No combat, loot, mounts, inventory, stats, gathering automation, new controls, resources, currencies, map, quest, dialogue, save schema, or progression change.
- Existing resource/renewal targeting remains authoritative; no route or economy changes.

## Acceptance

- Mossling visibly distinguishes idle/follow/guide/arrival/renewal states at mobile scale.
- Existing guidance target and RESET/persistence behavior remain unchanged.
- Compile/tests/review build/smoke/actual player visual check/docs/commit/push pass.

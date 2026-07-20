# Phase 11 Implementation Plan — Wayroot Renewal Return Loop

**Authorization:** Owner approved the next phase on 2026-07-20 after Phase 10 delivery.

## Goal

Make the existing finite gathering loop returnable without introducing a broad economy: after the first Wayroot is restored, depleted resource nodes renew on a deterministic, player-visible cadence so the clearing can support continued play.

## Locked delivery

- Reuse the three existing resource nodes: Wildflower, Young Tree, and Stone Outcrop.
- A depleted node remains visibly depleted until its recorded renewal deadline.
- Each node returns once after a fixed prototype interval; no random/drop-table/background-service system.
- Persist depletion and renewal timestamps/deadlines in the existing versioned local save.
- On restart, renewal state is resolved correctly from saved state; RESET remains a fresh start.
- Provide clear HUD/world feedback for depleted/renewing/available states and one concise completion/return status.
- Preserve Phase 1–10 controls, objective prerequisites, and local progression state.

## Explicit limits

- No additional resources, crafting, shop inventory, daily timers, real-time notifications, offline rewards, multiple biomes, or Phase 12 work.
- The first Wayroot remains finite; renewal supports repeatable gathering/combat/home play, not a second quest system.

## Acceptance criteria

- Gather each node, observe its depleted state and renewal information, and confirm it returns after the documented interval.
- Stop/restart around the renewal deadline and confirm state resolves correctly.
- RESET clears all renewal state.
- Pure timing rules and persistence migration have EditMode coverage; runtime composition has PlayMode coverage.
- Compile, EditMode, PlayMode, Windows build/smoke, manual guide, and Git hygiene pass together.

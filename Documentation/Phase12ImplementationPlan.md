# Phase 12 Implementation Plan — Shelter Rest and Return Utility

**Authorization:** Owner approved the next phase after confirming the Phase 11 renewal loop on 2026-07-20.

## Goal

Give the already-built shelter one useful, bounded purpose: it becomes a persistent safe rest/return point that supports the established exploration loop without adding fast-travel networks, beds, crafting, or a new economy.

## Locked player contract

- A built shelter becomes interactable with existing `E`, gamepad south, and **HOLD GATHER**.
- Resting at the shelter fully restores player health and records it as the active return point.
- When the player is defeated, the existing forgiving respawn returns them to the shelter instead of the initial world spawn.
- Before shelter construction, existing default respawn behavior remains unchanged.
- Shelter return-point state persists across restart and clears with **RESET**.
- The shelter provides clear world/HUD/action feedback for unavailable, rest, active-home, respawn, restart, and reset states.

## Explicit limits

- No teleport UI, multiple homes, fast-travel map, sleep/day-night system, storage, crafting, farming, NPCs, combat companion behavior, or Phase 13 work.

## Acceptance criteria

- Owner can build shelter, rest, take damage/die, and visibly respawn at the active shelter point.
- Default respawn remains correct if no shelter exists.
- Restart and RESET behavior are deterministic.
- Pure rules receive EditMode coverage and runtime composition/persistence gets PlayMode coverage.
- Compile, tests, Windows review build/smoke, manual guide, and Git hygiene pass as one complete package.

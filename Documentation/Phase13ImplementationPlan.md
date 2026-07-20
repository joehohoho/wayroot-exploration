# Phase 13 Implementation Plan — Restored Grove Encounter

**Authorization:** Owner approved the next phase after confirming Phase 12 shelter utility on 2026-07-20.

## Goal

Add one bounded post-Wayroot encounter to give the restored Sunmeadow clearing a second point of interest, reusing established combat, health, persistence, and return-point loops without adding a combat system overhaul.

## Locked player contract

- Restoring the first Wayroot opens a small, visibly changed grove edge in the existing clearing.
- The grove contains one distinct **Thorn Guardian** encounter.
- It uses existing attack controls, player health, and shelter respawn mechanics.
- It has a clear, modestly tougher health/contact profile than the Practice Slime.
- Defeat grants the existing **Slime Core** resource only—no new currency, inventory category, quest chain, or loot table.
- The guardian respawns on a fixed prototype cadence; the grove remains unavailable until Wayroot restoration.

## Explicit limits

- No additional region streaming, procedural generation, enemy roster, player skills, ranged weapons, boss phases, dialogue, quests, crafting, or Phase 14 work.

## Acceptance criteria

- The post-Wayroot grove change is visible and clearly labelled.
- Guardian appears only after Wayroot restoration and remains playable with current attack/touch controls.
- Enemy and player/shelter-respawn behavior are understandable and do not regress the slime loop.
- Compile, rules tests, PlayMode composition/condition coverage, Windows build/smoke, documentation, and Git hygiene pass together.

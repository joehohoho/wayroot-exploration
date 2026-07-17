# ADR-005: Separate static definitions from mutable runtime state

## Status
Accepted — 2026-07-17

## Context
Items, resources, weapons, biomes, recipes, and creatures need authored data; worlds and players need mutable per-save state.

## Decision
Use ScriptableObjects chiefly for static authored definitions. Use plain C# types for runtime state, instances, and save records. Presentation renders/references state but is not the permanent state.

## Alternatives
ScriptableObjects as live save state and MonoBehaviours as all state are rejected because both make per-save persistence and testing fragile.

## Consequences
Each feature must identify definition, runtime record, save record, and presentation responsibility before implementation.

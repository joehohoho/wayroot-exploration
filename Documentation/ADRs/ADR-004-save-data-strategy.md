# ADR-004: Versioned save records plus seeded world deltas

## Status
Accepted — 2026-07-17

## Context
Independent worlds need persistent generated regions, recovery backups, migrations, and no brittle scene-state serialization.

## Decision
Use stable-ID, versioned plain C# save records. Generate base regions from seed + generation version + definitions, then apply deltas. Never serialize MonoBehaviours/scenes as durable saves.

## Alternatives
Full scene/MonoBehaviour serialization and saving every generated object are rejected as fragile and unnecessarily large.

## Consequences
Future saving requires atomic replacement, current/previous/older backups, migration tests, corruption handling, and partitioned region deltas.

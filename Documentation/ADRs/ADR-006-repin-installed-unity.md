# ADR-006: Repin to installed Unity 6000.5.4f1

## Status
Accepted — 2026-07-17

## Context
ADR-001 pinned 6000.0.78f1 for Unity 6.0 LTS. Unity Hub on the development workstation does not offer that editor or another suitable Unity 6.0 LTS update. The owner explicitly directed the project to proceed using the installed Unity 6000.5.4f1.

## Decision
Supersede ADR-001's version choice. Pin Phase 0 and Phase 1 to **Unity 6000.5.4f1** and compile/test with this installed editor. Future milestone upgrades still require explicit owner approval and must keep the same patch through their milestone.

## Alternatives considered
- Block work until 6000.0.78f1 becomes installable: rejected by owner direction.
- Compile an unpinned project with the installed editor: rejected because it leaves versions undocumented and non-reproducible.

## Consequences
Project metadata, docs, and validation commands use 6000.5.4f1. The Mac iPhone build worker must use the same editor plus iOS Build Support and full Xcode. This is an owner-approved deviation from the original Unity 6.0 LTS request.

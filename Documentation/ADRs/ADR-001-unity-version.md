# ADR-001: Pin Unity 6000.0.78f1

## Status
Accepted — 2026-07-17

## Context
The project requires Unity 6.0 LTS and a stable patch per milestone. The workstation has 6000.5.4f1, which could migrate project metadata.

## Decision
Use Unity **6000.0.78f1** for Phase 0 and Phase 1; upgrades need owner approval.

## Alternatives
Installed 6000.5.4f1 is rejected because it is not the selected Unity 6.0 LTS patch. Older 6000.0 patches are rejected because 6000.0.78f1 is the latest discoverable 6.0 LTS patch at setup time.

## Consequences
Install/license the pinned editor before compilation. iPhone build needs a Mac with matching editor, iOS support, and Xcode.

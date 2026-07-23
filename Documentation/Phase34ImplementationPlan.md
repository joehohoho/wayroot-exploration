# Phase 34 Implementation Plan — Full-Route Regression and Release-Readiness Pass

**Authorization:** Owner directed continued development after Phase 33. This is a bounded stabilization milestone before any material feature expansion.

## Goal

Harden the complete existing route for a coherent desktop review package and accurately document the remaining physical-iPhone gate.

## Scope

- Add focused regression coverage across save/reset, merchant, shelter, Wayroot, Guardian, Glade, Bloomwell, Journey, accessibility, audio, Mossling, and mobile safeguard integration.
- Add player-facing completion/readiness feedback only where it removes ambiguity in the existing route; no new progression.
- Produce a practical release/device-review checklist, including known Mac/Xcode/signing/device blockers.
- Audit and correct visible presentation regressions found during a fresh end-to-end player review.

## Boundaries

- No new feature, gameplay value, item, currency, map, quest, dialogue, region, enemy, settings system, iOS build, or save-schema change.
- Do not label store/release/iPhone ready without required external evidence.

## Acceptance

- The existing full route passes fresh RESET-to-Bloomwell regression checks.
- Review player has no known magenta/oversized/hovering geometry or essential UI obstruction.
- Windows build/smoke/tests/docs/commit/push pass; device evidence is accurately pending.

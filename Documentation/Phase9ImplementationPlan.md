# Phase 9 Implementation Plan — Production Readiness and Next-Feature Plan

**Authorization:** Owner approved Phase 9 after successful Phase 8 review on 2026-07-20.

## Goal

Convert the proven controlled vertical slice into a responsible production-ready next-work plan, while fixing only high-confidence polish/quality gaps discovered from the completed slice.

## Delivery

1. **Production readiness audit**
   - Reconfirm Unity/URP/Input System pins, source-control hygiene, save/reset behavior, desktop build reproducibility, and public-repository safety.
   - Record actual iPhone/macOS/Xcode/signing blockers without claiming iOS validation.
2. **Playtest and quality plan**
   - One full-loop test script from fresh reset through gather, combat, progression, shelter, companion, restart, and reset.
   - Explicit mobile Device Simulator checklist and performance observation points.
3. **Prioritized next-feature roadmap**
   - Rank viable post-slice work by player value, risk, dependencies, and mobile cost.
   - Keep new major mechanics unimplemented until an owner chooses the next approved vertical slice.
4. **Small corrective work only**
   - Fix directly observed reliability/presentation defects from the audit if tests can prove them.

## Exclusions

- No automatic Phase 10 gameplay expansion.
- No external art, networking, accounts, purchases, analytics, cloud saves, or iOS signing configuration without explicit owner direction.

## Acceptance criteria

- A reviewable production-readiness report and a single operator/tester checklist exist in the repo.
- Current build/test evidence is accurately summarized; open mobile blockers are explicit.
- The next recommended feature slice is a decision proposal, not silently implemented scope.

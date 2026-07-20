# Phase 10 Decision Proposal — Select One Approved Vertical Slice

**Status:** proposal only. No Phase 10 mechanics are implemented by Phase 9. The owner must choose one option (or explicitly defer) before code changes begin.

## Decision framing

The current slice already proves movement/touch, three finite gathering nodes, bounded inventory, one combat/drop loop, one weapon upgrade, one shelter build, one companion, persistence/reset, and a readable Sunmeadow setting. The strongest next step should turn those demonstrated systems into a clearer player purpose without expanding into networking, cloud save, accounts, paid content, external art, or a broad new economy.

## Ranked candidates

| Rank | Candidate | Player value | Dependencies / delivery risk | Mobile cost | Recommendation |
|---|---|---|---|---|---|
| 1 | **One Wayroot restoration objective**: gather existing materials, satisfy one explicit requirement, activate a single landmark, save its restored state, and present clear completion feedback. | High: makes the title fantasy and the existing gathering/progression systems cohere around a visible short-term goal. | Medium: needs a single authored objective, requirement rule, state/persistence migration, UI feedback, and reset coverage. It should reuse existing resources rather than introduce crafting, quests, or a new world generator. | Low–medium: one landmark and compact status UI; validate touch readability. | **Recommend as the next approved vertical slice.** |
| 2 | **Resource renewal plus repeatable return loop**: deterministic reset/respawn of existing nodes with clear world feedback. | Medium–high: turns finite gathering into repeatable play and supports later objectives. | Medium: economy cadence, persistence semantics, visual state, and edge-case testing need owner choices. By itself it lacks a player-facing purpose. | Low: mostly rules/UI, but background/timing behavior needs device testing. | Good foundation only if the owner prioritizes repeatability over a visible goal. |
| 3 | **Shelter utility interaction**: one safe, explicit use of the built shelter (for example a rest/return point) with saved state. | Medium: gives the existing shelter a purpose. | Medium: respawn/return semantics, UX, and abuse cases can become design-heavy; does not strengthen the main exploration promise as directly as a Wayroot goal. | Low. | Consider after an objective establishes why the player is building. |
| 4 | **Second bounded encounter/region variation**: a small additional landmark or enemy/resource arrangement. | Medium: improves exploration variety. | Medium–high: risks content multiplication, balance churn, and presentation work before a repeatable purpose is established. | Medium: increases scene/rendering and touch-navigation review surface. | Defer until a primary goal/repeat loop is proven. |

## Recommended Phase 10: one Wayroot restoration objective

### Proposed player contract

1. The player can discover one clearly labelled dormant Wayroot in the existing Sunmeadow clearing.
2. The player uses only already-proven resource/progression concepts to meet one owner-approved requirement.
3. On success, the Wayroot visibly changes state, communicates completion, persists across restart, and clears on RESET.
4. The vertical slice has a clear end state; it does **not** silently become a multi-quest system, crafting tree, procedural region, or live-service loop.

### Owner decisions required before implementation

1. What exact existing resources and quantities restore the first Wayroot?
2. Should the weapon upgrade and/or shelter be prerequisites, optional parallel progression, or unrelated to this objective?
3. What is the desired completion feedback/reward: visual restoration only, access to a small existing-space change, or another explicitly bounded reward?
4. Is a finite single-objective prototype acceptable before resource renewal is introduced?
5. What mobile device/performance gate must pass before the next slice is considered owner-reviewable?

### Acceptance and verification shape if approved

- One focused implementation plan and manual test are written and owner-approved before code work.
- Pure requirement/state rules receive EditMode coverage.
- Bootstrap composition/persistence/reset receives PlayMode coverage.
- Unity compile, EditMode, PlayMode, Windows development build/smoke, full-loop regression, and iPhone checklist are rerun.
- The build remains primitive/prototype scoped; no unapproved platform, monetization, networking, or content-system expansion.

## Deferral option

If physical-iPhone evidence is the priority, defer Phase 10 and use the next owner-approved work solely to close the blockers in `Phase9iPhoneBlockers.md`. That is a valid release-readiness decision, not a missing gameplay feature.

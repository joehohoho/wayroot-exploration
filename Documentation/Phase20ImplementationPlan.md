# Phase 20 Implementation Plan — Gentle Journey Guidance

**Authorization:** Owner approved the next step after Phase 19 on 2026-07-20.

## Goal

Make the now-complete prototype route understandable to a first-time player without adding a quest system, map, dialogue tree, or intrusive UI.

## Locked player contract

- A compact **Journey** status names the single recommended next milestone based solely on existing persistent progress.
- A small magical compass/firefly indicator points toward the relevant existing world target while it is off-screen.
- The guide progresses through existing objectives only: first resource/merchant, shelter, Wayroot, Guardian/Grove, Moonlit Glade, and Bloomwell.
- After Bloomwell restoration, it changes to a non-directive completed/free-exploration state.
- Guidance never blocks play, grants items, changes requirements, auto-navigates, adds a map, or creates a formal quest system.
- It persists correctly through restart because it derives from existing save state; RESET returns to the initial gentle prompt.

## Presentation

- Follow the warm stylized magical direction: subtle compass glow/firefly trail, concise mobile-readable text, safe-area layout, and optional compact toggle if required to avoid visual clutter.
- Preserve existing action feedback, guide, inventory, renewal, and sound controls.

## Acceptance criteria

- A fresh reset points players toward the first useful existing step.
- Each existing progression state selects the correct next target; off-screen guidance behaves safely.
- Completed Bloomwell state uses no mandatory pointer.
- Compile, rules tests, PlayMode state/target coverage, Windows build/smoke, documentation, and Git hygiene pass.

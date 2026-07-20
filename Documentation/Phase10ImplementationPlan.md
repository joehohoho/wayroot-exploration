# Phase 10 Implementation Plan — First Wayroot Restoration Objective

**Authorization:** Owner approved the recommended Phase 10 direction on 2026-07-20.

## Locked player contract

1. Find the clearly labelled dormant Wayroot in the existing Sunmeadow clearing.
2. Build the shelter and purchase the Iron Edge weapon upgrade.
3. Hold the existing interaction control near the Wayroot while carrying:

   ```text
   3 PETAL + 3 TIMBER + 3 STONE + 1 CORE
   ```

4. The objective safely spends the full cost once, restores the Wayroot, persists across restart, and returns to dormant after RESET.
5. Restoration provides a small visible change in the existing clearing—no new region, quest tree, crafting system, or repeatable resource loop.

## Design decisions

- The shelter and weapon upgrade are **required prerequisites** for this first objective.
- This is one finite prototype objective; resource renewal is deferred.
- Phase 10 owner review uses Device Simulator and desktop evidence only. Physical-iPhone work remains an explicit future gate.

## Implementation requirements

- Reuse existing semantic interaction: `E`, gamepad south, and **HOLD GATHER**.
- Add deterministic pure rules for cost/prerequisite checks and safe one-time spending.
- Extend existing versioned local save; use a backward-compatible default for the restored state.
- Provide clear world/HUD feedback for unmet requirements, successful restoration, already-restored state, restart, and RESET.
- Use built-in primitive/URP presentation only.

## Acceptance criteria

- A dormant Wayroot is easy to locate and understand.
- It cannot restore without all four resources plus shelter and weapon prerequisites.
- It visibly transforms the clearing and communicates success.
- Restart preserves restoration; RESET removes it.
- Compile, EditMode, PlayMode, Windows build/smoke, Device Simulator checklist, documentation, and Git hygiene pass as one cohesive package.

# ADR-007: Use uGUI for Phase 1 runtime touch controls

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

Phase 1 requires a mobile-first virtual joystick, pause control, safe-area layout, handedness support, and development overlay. The project must choose deliberately between Unity UI (uGUI) and UI Toolkit before implementing runtime controls.

## Decision

Use **Unity UI/uGUI** for Phase 1 runtime controls. The controlled prototype builds its small Canvas/EventSystem hierarchy at runtime, keeping the bootstrap scene minimal and the prototype reviewable without opaque serialized UI assets.

uGUI pointer events are suitable for a virtual joystick and are familiar for touch-heavy gameplay UI. The choice is not a permanent claim that all future menus must be runtime-built; a later production UI pass may replace prototype presentation while retaining the input-command boundary.

## Consequences

- `VirtualJoystick` owns pointer interpretation only; it forwards a vector to `PrototypeInputReader` and contains no player movement rules.
- `SafeAreaLayout` owns safe-area anchoring and handedness placement.
- Input commands remain device-independent and use the Unity Input System.
- No UI Toolkit package or runtime UI framework is added in Phase 1.

## Alternatives considered

- **UI Toolkit:** strong for tooling and structured menus, but the early touch joystick path would add learning/implementation risk without a current payoff.
- **Legacy immediate-mode GUI:** unsuitable for production-oriented touch controls and excluded from the prototype.

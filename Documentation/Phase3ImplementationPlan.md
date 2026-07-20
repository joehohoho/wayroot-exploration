# Phase 3 Implementation Plan — Forgiving Combat Slice

**Authorization:** Owner approved Phase 3 on 2026-07-19 after Phase 2 completion.

## Goal

Extend the controlled prototype with one clear, touch-first combat loop:

> Move into target range → hold Attack → damage one slime-like placeholder enemy → enemy pursues and lightly damages player → defeat respawns player safely with gathered inventory retained.

## Locked scope

- One placeholder enemy, one sword-style attack, one target at a time, player/enemy health, simple chase/contact damage, a forgiving home respawn.
- Keyboard/gamepad/touch all drive semantic Attack; a uGUI Attack button uses the existing safe-area action side.
- No equipment, loot, enemy AI states beyond idle/chase/contact, animation, audio, drops, leveling, multiple enemies, or Phase 4 progression.

## Vertical tasks

1. **Pure rules and tests:** health clamp/damage/defeat and range/cooldown rules. Red-first EditMode coverage.
2. **Combat runtime:** player health, attack controller, one enemy health/chase/contact controller, controlled target resolver.
3. **Presentation/UI:** target/health feedback, lower-right Attack hold button, clear respawn state.
4. **Forgiving respawn:** reset position/health, retain inventory and gathered state; enemy respawns after a short delay.
5. **Verification:** EditMode/PlayMode tests, Windows build/smoke, manual guide, roadmap/handoff, commit/push.

## Acceptance criteria

- Attack does not hit outside range or faster than its cooldown.
- Only the selected/nearest active enemy takes damage.
- Player defeat restores at home with full health; Phase 2 inventory and gathered-state save are untouched.
- All input routes trigger the same attack command.
- Automated tests, compilation, PlayMode, and a Windows development build pass.

## Risks

- Combat can unintentionally expand into Phase 4. Keep one enemy and one attack only.
- Touch controls can conflict with Gather. Give Attack its own right-side action location and retain semantic input separation.
- Any persistence change risks Phase 2 state. Combat health/position reset is session-only in this phase.

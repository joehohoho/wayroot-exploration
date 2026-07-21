# Phase 21 Implementation Plan — Dodge and Combat Readability

**Authorization:** Owner approved the next step after Phase 20 on 2026-07-20.

## Goal

Make the existing melee encounters more responsive and readable by adding one semantic dodge action and clear enemy attack anticipation—without adding new weapons, enemy types, damage values, or a combat overhaul.

## Locked player contract

- Add one dodge action: keyboard **Left Shift**, gamepad east face button, and a mobile-safe **DODGE** touch button.
- Dodge performs a short directional burst based on current movement/facing, has a visible compact cooldown, and grants a brief contact-damage immunity window.
- Slime and Thorn Guardian gain clear pre-contact visual/audio anticipation so a dodge is learnable rather than required by surprise.
- Existing attack, gathering, interaction, pause, health, shelter respawn, enemy rewards, and accessibility-safe UI continue to work.
- No stamina bar, talent tree, new weapons, ranged attacks, new enemy roster, difficulty selector, or changed damage/health/reward values.

## Presentation

- Use the warm stylized direction: soft motion trail, squash/lean, readable cooldown ring/text, and creature anticipation consistent with Phase 18 animation/sound.
- Keep the touch action cluster usable and non-overlapping in landscape safe areas.

## Acceptance criteria

- Dodging out of contact avoids damage only during its short documented immunity window; ordinary hits remain unchanged.
- Keyboard, gamepad, and touch all drive the same semantic action.
- Existing full loop, restart, and RESET behavior remain intact.
- Compile, EditMode rules tests, PlayMode input/combat coverage, Windows build/smoke, documentation, and Git hygiene pass.

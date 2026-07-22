# Phase 26 Implementation Plan — Combat Encounter Polish

**Authorization:** Owner selected this bounded Phase 26 scope on 2026-07-22.

## Goal

Make the existing Practice Slime and Thorn Guardian encounters feel more intentional, readable, and responsive at mobile scale while retaining all established combat values, controls, rewards, and progression.

## Player-facing scope

- Improve pre-attack telegraphs: clear brief anticipation body language, ground/arc cues, and distinct readable colors for Slime and Guardian.
- Improve player attack feedback: compact swing trail, impact flash, hit-stop-like visual pulse, and short-lived readable contact marker; no damage, cooldown, range, or weapon changes.
- Improve enemy hit/defeat/respawn feedback and chase movement readability using existing semantic enemy state.
- Improve Guardian arena encounter presentation: visible combat boundary accents, threat focus, and response feedback without changing its gate, health, damage, reward, or respawn interval.
- Maintain clear mobile composition: effects stay brief, player/enemy silhouettes remain visible, and HUD/touch controls remain unobstructed.

## Boundaries

- No weapons, skills, stamina, enemy types, AI behavior changes, boss chain, resources, currencies, rewards, combat values, save-schema changes, maps, or new UI systems.
- Use only original runtime-composed geometry/materials/C# and existing audio pipeline; no external assets.
- Preserve semantic controls: attack, dodge, interaction, keyboard/gamepad/touch mappings, persistence, and RESET.

## Acceptance criteria

- The Slime and Guardian visibly communicate anticipation, contact/hit, defeat, and respawn states.
- Player attacks have clear but brief impact feedback without concealing actors or mobile controls.
- Existing combat values/progression remain unchanged and existing Phase 21 dodge behavior remains readable.
- Compile, focused EditMode/PlayMode coverage, Windows review build/smoke, fresh visual player review, docs, clean commit, and push pass.

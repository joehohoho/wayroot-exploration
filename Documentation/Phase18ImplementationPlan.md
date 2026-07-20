# Phase 18 Implementation Plan — Major Stylized Art and Animation Pass

**Authorization:** Owner explicitly requested a major improvement to graphics and animations before further gameplay expansion on 2026-07-20.

## Goal

Substantially improve the visual quality and sense of life in the existing warm, modern stylized magical world using original, runtime-composed Unity geometry/materials and procedural animation. This is an art-and-motion milestone, not another gameplay phase.

## Visual target

- Rounded, layered silhouettes with clearer character proportions and stronger color grouping.
- Softer, richer lighting, ambient contrast, magical glow, water/foliage movement, and improved world depth.
- Expressive original creatures readable at mobile scale.
- Cohesive child-and-adult-friendly cozy adventure tone.

## Motion target

- Player: readable idle breathing, moving stride/bob, facing response, cloak/lantern secondary motion, and gathering/attack emphasis.
- Mossling: springy follow gait, ear/tail/face-like idle movement, guide excitement.
- Slime/Thorn Guardian: distinct idle cycles, squash/stretch locomotion, hit reaction, attack pulse, defeat/respawn transitions.
- World: gentle tree/flower sway, water movement, Wayroot/Glade motes and landmark pulse.
- Motion must preserve collision, interaction positions, existing controls, and mobile performance.

## Boundaries

- Source-controlled original procedural geometry/material/animation only. No unapproved asset packs, purchased content, external characters, online-generation dependency, or copyrighted likenesses.
- No changes to game rules, economy, unlock requirements, combat values, persistence contracts, or Phase 19 content.
- Keep labels/HUD/action buttons readable and safe-area-aware.

## Acceptance criteria

- Major visible improvement is evident immediately in Play mode, including active player, creature, enemy, and environment motion.
- Existing full loop still behaves identically and all world interactions remain reachable.
- Add focused PlayMode coverage for active animation composition/restore-safe behavior where practical, compile/test/build/smoke, documentation, and Git hygiene.

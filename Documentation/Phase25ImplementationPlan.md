# Phase 25 Implementation Plan — Exploration Art and Mobile Composition Pass

**Authorization:** Owner selected the exploration/art pass on 2026-07-21 after Phase 24 completion.

## Goal

Make the existing Sunmeadow, Restored Grove, Moonlit Glade, and Bloomwell route feel more intentionally composed and visually distinct at mobile-readable scale, without adding a new gameplay system or expanding the progression contract.

## Player-facing scope

- Give each existing space a stronger visual identity using only original runtime-composed geometry, URP materials, source-controlled C#, and existing procedural audio/effects:
  - **Sunmeadow:** warmer entry clearing, layered meadow edges, clearer creek/path rhythm and resource pockets.
  - **Restored Grove:** stronger sheltered transition, thorn/stone boundary, Wayroot-to-Guardian encounter focus.
  - **Moonlit Glade:** more coherent violet-blue canopy, readable route, luminous gathering clearings, and Bloomwell sightline.
  - **Bloomwell:** stronger distant landmark silhouette and understated completed-state ambience.
- Add restrained authored landmark/route dressing that reinforces existing destinations without blocking movement, interactions, world markers, touch controls, or the central player focus zone.
- Retune existing camera composition only where necessary to preserve player/actor readability and compact mobile presentation.
- Keep the established actor fallback/sprite presentation readable; do not reopen the Phase 24 rendering architecture.

## Boundaries

- No new region, map, fast travel, NPC, quest, currency, resource type, crafting recipe, enemy type, combat value, unlock requirement, or save schema.
- No third-party/external/generated art or audio assets.
- Existing gathering, renewal, shelter, Mossling, Wayroot, Guardian, Moonlit Glade, Bloomwell, Journey guide, touch controls, HUD, and RESET must retain their behavior.

## Acceptance criteria

- A fresh 16:9 review-player capture clearly differentiates Sunmeadow, Grove, Glade, and Bloomwell while keeping a clean central focal area.
- Existing player, slime, and Guardian remain readable against each region's palette.
- Existing interaction/world markers stay legible and unobstructed.
- Existing route and persisted visual gates retain their behavior across restart and RESET.
- Compile, EditMode, PlayMode, Windows review build/smoke, fresh visual inspection, documentation, clean commit, and push succeed.

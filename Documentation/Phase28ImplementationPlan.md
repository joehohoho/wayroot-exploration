# Phase 28 Implementation Plan — Bloomwell Finale Presentation Pass

**Authorization:** Owner selected Phase 28 option 5 on 2026-07-22.

## Goal

Give the existing Bloomwell restoration finale a more memorable visual and procedural-audio climax, clear completion feedback, and a peaceful post-completion world state—without adding progression beyond the already implemented finale.

## Player-facing scope

- Improve the approach/readability of the existing Bloomwell landmark in Moonlit Glade.
- Add a compact staged restoration presentation at successful activation: visible pulse, petal/mote orbit, water/light response, and a short original procedural finale motif using the established soundscape.
- Improve completion feedback in the existing Journey/status treatment without adding a new UI system.
- Add restrained persistent post-completion environmental response across the existing Glade/Sunmeadow/Bloomwell route.
- Preserve clear mobile composition: effects must never obscure player, actors, HUD, touch controls, or interaction prompts.

## Boundaries

- No new ending route, region, enemy, boss, item, resource, currency, crafting, quest, dialogue tree, map, fast travel, achievement system, or save-schema change.
- Do not change Bloomwell requirements, Guardian gate, combat values/rewards, current controls, reset semantics, or persistence behavior.
- Only original runtime-composed visuals/C# and existing procedural audio patterns.

## Acceptance criteria

- Bloomwell restoration has a clearly visible, bounded climax and an unmistakable peaceful completed-state read.
- Existing completion persists; RESET restores the existing pre-completion prototype state.
- No magenta materials, hovering/oversized geometry, or HUD/control occlusion in Windows player review.
- Compile, focused EditMode/PlayMode tests, Windows review build/smoke, actual player screenshot inspection, docs, clean commit, and push pass.

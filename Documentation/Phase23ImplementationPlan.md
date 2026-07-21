# Phase 23 Implementation Plan — Animated Sprite Character Pass

**Authorization:** Owner explicitly requested animated sprites for the main character and stronger graphical enemy sprites on 2026-07-21.

## Goal

Replace the current primitive-first actor read with original animated 2.5D sprite characters while preserving the existing top-down world, colliders, gameplay roots, controls, and camera.

## Player sprite contract

- Create an original, source-controlled animated sprite character with a clear blue-cloak explorer silhouette, warm lantern accent, face/hair detail, and readable top-down/three-quarter direction.
- Use a real Unity `SpriteRenderer` presentation rig on the existing player gameplay root; never use the old geometry as the primary visible player character.
- Select visible frames from existing semantic state, not raw key input:
  - idle/breathing;
  - moving/walk cycle and facing;
  - attack windup/swing/recover;
  - gathering reach/collect;
  - dodge burst/recover;
  - defeated/respawn-safe transition.
- Keep collider, interaction point, camera target, movement, save, and input behavior unchanged.

## Enemy sprite contract

- Replace the primitive-first visual read of the Practice Slime and Thorn Guardian with distinct original animated SpriteRenderer rigs.
- Slime: expressive body, eyes, idle squash, hop/chase, contact anticipation, hit, defeat, and respawn frames.
- Thorn Guardian: larger thorn/leaf guardian silhouette, breathing/idle, advance, windup, hit, defeat, and respawn frames.
- Preserve their existing colliders, chase, attack timing, health, reward, unlock conditions, and semantic labels.

## Art and technical constraints

- Create original in-repo sprite artwork via source-controlled C#/SVG-style procedural sprite-frame generation only; no asset packs, external character art, paid assets, generated-image service, or recognizable third-party style/character.
- Rigs must billboard consistently to the top-down camera, render above their gameplay roots without obscuring interactable world markers, and remain performant/mobile-conscious.
- Retain the Phase 22 compact alpha HUD and visual hierarchy.

## Acceptance criteria

- Game view/player visually demonstrates clear player action transitions for move, attack, gather, and dodge.
- Both enemies are recognizably distinct animated sprites rather than primitive body shapes.
- Existing gameplay/combat/gathering/unlocks/persistence survive the presentation replacement.
- Add animation state-selection unit tests and PlayMode sprite-rig/combat/gather integration coverage; compile, test, build, smoke, visually inspect, document, and publish as one Phase 23 review build.

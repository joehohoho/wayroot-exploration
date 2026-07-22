# Phase 24 Implementation Plan — Sprite Rig Cleanup and Alpha Actor Polish

**Authorization:** Owner supplied an after-Phase-23 screenshot on 2026-07-21 and requested cleanup and graphic improvement.

## Screenshot-grounded defects

The capture confirms Phase 23's core direction but exposes presentation defects that block acceptance:

1. The explorer is visibly duplicated: a small legacy primitive character remains underneath a much larger sprite read.
2. The prominent player sprite displays as a large flat green rectangular/panel-like shape instead of a clean transparent character silhouette.
3. Enemy/actor presentation still mixes visible primitive shells with sprite rigs, creating inconsistent scale and silhouette language.
4. Sprite scale, anchor, depth sorting, and camera-billboard alignment are not yet tuned to the scene.

## Goal

Make each actor read as exactly **one polished character**: a transparent, correctly sized, correctly anchored animated sprite with no visible legacy body duplication.

## Required correction

- Explicitly use a transparency-safe URP sprite material/shader and verify transparent pixels do not render as a colored rectangle.
- Centralize actor visual masking so all legacy body/child visual renderers are hidden while gameplay components/colliders remain intact; keep only the designated sprite rig visible.
- Correct player/slime/guardian sprite scale, sorting order, pivot/height, billboard orientation, and camera-relative facing.
- Improve original frame art: stronger silhouette, outline/shadow, clearer cloak/head/lantern separation, and more expressive slime/guardian shapes at mobile scale.
- Preserve semantic action transitions from Phase 23 and all existing gameplay state.

## Acceptance criteria

- 16:9 Game-view/player visual evidence has no double player, opaque sprite panel, or duplicate primitive enemy body.
- Player and enemy sprite rigs remain visibly distinct while idle, moving, attacking, gathering, and hit/defeat states.
- HUD and world presentation from Phase 22 remain intact.
- Tests, build, smoke, visual inspection, docs, clean commit, and push complete as one corrective update.

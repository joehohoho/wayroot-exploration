# Phase 14 Implementation Plan — Art Direction Foundation

**Status:** Owner-approved and implemented as the Phase 14 warm stylized magical-world art-direction pass.

## Why now

The prototype has enough proven loops—movement, combat, gathering/renewal, progression, shelter, companion, Wayroot, and a second encounter—to make a cohesive art pass worthwhile. Further gameplay expansion should not continue ahead of this first intentional style pass.

## Approved visual direction

Create a **warm, modern, stylized 3D magical world** with rounded shapes, expressive original creatures, soft lighting, clean mobile-readable silhouettes, and a cozy adventurous tone that appeals to both children and adults.

Use original runtime-composed geometry and source-controlled Unity materials in the first pass; no unapproved third-party assets or packages.

## First visual delivery after style selection

- Create one approved palette/material/lighting language for the player, environment, resource nodes, enemies, Wayroot, shelter, and UI.
- Replace inconsistent primitive coloring with a coherent style treatment using only owner-approved/licensed source assets.
- Improve terrain/prop/character silhouettes, readability, and labels while retaining the validated interaction locations.
- Preserve mobile-safe performance and all existing control/persistence loops.

## Delivered approach

- A compact source-controlled URP material library under `Assets/Game/Art/Resources/Phase14` establishes the warm ground, foliage, water, bark, accent, and Wayroot glow palette.
- Runtime-composed primitive geometry now builds rounded, layered silhouettes for the explorer, Mossling, slime, Thorn Guardian, trees, creek, merchant, shelter, gathering props, and Wayroot without moving interaction anchors or adding assets/packages.
- Lighting moved to warmer directional light, softer tri-light ambient, and lighter fog. World identifiers keep their existing compact font sizing and screen-horizontal billboard behavior.
- Phase 14 PlayMode coverage asserts the visible silhouette anchors and mobile-readable labels; all existing loops remain in scope and unchanged.

## Exclusions

- No unapproved third-party art, generated art with uncertain licensing, paid asset purchases, external packages, or gameplay Phase 15 work.

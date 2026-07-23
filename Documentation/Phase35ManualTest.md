# Phase 35 Manual Test — Iron Edge Merchant Presentation

## Scope

Phase 35 is a presentation-only pass over the existing Iron Edge merchant and its single weapon upgrade. It does **not** change the existing price, combat values, controls, inventory, route, save schema, persistence, or RESET behavior.

## Desktop review-player controls

- Move: **WASD** or the lower-left virtual joystick.
- Interact: hold **E** or the on-screen **INTERACT** button while beside a landmark.
- Gather / attack: existing **SPACE** / **ATTACK** controls.
- Reset the disposable prototype save: **RESET** in the upper-right HUD.

## Focused Iron Edge review

1. Press **RESET**. Move to the warm red-and-wood merchant at the front-right of the Sunmeadow spawn area.
2. Verify its counter, posts, canopy trim, anvil, display blade, ember lantern, and compact ground ring read as one stall without blocking the interaction area or HUD.
3. With no Petal/Core, hold **E** beside the merchant. Confirm the existing `Need 1 PETAL + 1 CORE.` feedback remains, with a compact coral presentation pulse and original low unavailable tone.
4. Gather one **PETAL**, defeat the existing Practice Slime for one **CORE**, then return and hold **E**. Confirm the unchanged purchase result `IRON EDGE purchased: ATK 1 -> 2.`, compact warm-gold ring/tone feedback, and a visible gold blade/guard alongside the player.
5. Try again. Confirm the existing already-owned result `IRON EDGE already purchased: ATK 2.`, a compact cool-blue pulse/tone, and no second purchase.
6. Restart the player. Confirm the equipped Iron Edge visual remains whenever the existing saved level is owned. Press **RESET**, restart, and confirm the blade is absent again.
7. Regression-check a nearby interaction and one combat hit: merchant art must have no collision/occlusion effect, player controls remain unchanged, and attack damage remains the existing **1 → 2** upgrade only.

## Desktop evidence only

The Windows review player validates the runtime presentation path. It is not a physical-iPhone, signing, or store-release claim.

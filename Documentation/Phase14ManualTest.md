# Phase 14 Manual Test — Warm Stylized Magical World

## Setup

1. Open `Assets/Game/Scenes/Bootstrap.unity` in Unity **6000.5.4f1** and press Play.
2. Use **RESET** before a fresh-state check. Existing saved progress is intentionally retained.

## Visible art-direction pass

- Confirm a warm, soft-lit Sunmeadow: layered rounded tree canopies, flower clusters, a shallow creek with lily pads, and a clear warm path.
- Confirm the player reads as a blue-cloaked explorer with hair and a lantern; the Mossling has a face/ears/tail; the slime and Thorn Guardian read as distinct rounded creatures with crown/face or crown/heart accents.
- Confirm the merchant has a red rounded awning and lantern; the dormant Wayroot has a petal crown and glowing heart; built shelter, resources, and restored grove remain at their existing positions.
- Confirm world labels remain compact, horizontal to the screen, and readable. They must not be diagonal or oversized.

## Regression path

1. Gather, trade, build/rest at the shelter, restore the Wayroot, and open the grove using the existing controls.
2. Fight both enemy types, allow a respawn, and use RESET.
3. Restart Play Mode after a state change and confirm prior persistence behavior remains unchanged.

This is desktop evidence only; it is not a physical-iPhone performance or device-validation claim.

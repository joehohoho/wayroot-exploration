# Phase 10 Manual Test — First Wayroot Restoration

## Scope and setup

1. Open `Assets/Game/Scenes/Bootstrap.unity` with Unity **6000.5.4f1**, press **Play**, and use **RESET** before starting.
2. Confirm the existing Sunmeadow clearing now contains a labelled **DORMANT WAYROOT / HOLD GATHER** near the south side of the clearing. The HUD lists the exact objective cost.
3. Interaction remains semantic: **HOLD GATHER**, `E`, or gamepad south. The Wayroot never uses a new input path.

## Full objective loop

1. At the Wayroot, hold interaction before prerequisites are met. Confirm the action feedback identifies the missing **IRON EDGE** upgrade, then (after obtaining it) the missing **SHELTER**, and then the exact resource requirement:
   `3 PETAL + 3 TIMBER + 3 STONE + 1 CORE`.
2. Gather the finite existing resources, defeat the existing slime for cores as needed, buy **IRON EDGE**, and build the shelter. Verify their established feedback and persistence still work.
3. Return while carrying the exact Wayroot cost. Hold interaction once. Confirm the full cost leaves the HUD exactly once, the feedback reads **WAYROOT RESTORED**, the label changes to **WAYROOT / RESTORED**, and the bright green bloom appears in the existing clearing.
4. Interact again. Confirm the response says the Wayroot is already restored and no resources can be spent again.
5. Stop and press Play again (or restart the development player). Confirm the restored label/bloom persist.
6. Press **RESET**, let the scene reload, and confirm the Wayroot is dormant again with no bloom and no restored state.

## Device Simulator review guide

> Device Simulator is desktop evidence only; it does not validate iOS compilation, signing, physical touch ergonomics, performance, or persistence on an iPhone.

1. In the Unity Editor open **Window → General → Device Simulator**. If the window/package is unavailable, record that condition rather than adding a package in this milestone.
2. Select an iPhone landscape profile matching the project’s intended landscape orientation, enter Play mode, and keep the simulator safe-area visualization enabled.
3. From a fresh **RESET**, verify the title, resource/HUD text, **HOLD GATHER**, **PAUSE**, and **RESET** remain reachable and legible. Use the simulator’s pointer-to-touch controls to complete the Wayroot interaction path; keyboard `E` remains a desktop fallback only.
4. Check that the dormant label, restoration feedback card, restored bloom, and HUD objective state are not hidden by safe-area margins or the touch controls.
5. Record simulator profile, Unity version, tester, result, and any readability/overlap issue. Run `Documentation/Phase9iPhoneBlockers.md` on physical hardware before any iPhone-ready claim.

## Regression and scope

- Repeat the established movement, gathering, combat/core, merchant, shelter, companion, pause, restart, and reset checks.
- The objective is one finite persistent landmark. It introduces no resource renewal, crafting, quest tree, additional region, external art, network, or platform expansion.

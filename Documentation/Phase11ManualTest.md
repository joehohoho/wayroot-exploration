# Phase 11 Manual Test — Wayroot Renewal Return Loop

## Scope and setup

1. Open `Assets/Game/Scenes/Bootstrap.unity` with Unity **6000.5.4f1**, press **Play**, and use **RESET** before the first check.
2. Complete the existing first Wayroot objective if it is not already restored. Renewal starts only after the persistent Wayroot restoration; it does not add resources, crafting, regions, or another quest.
3. Use the established semantic interaction: **HOLD GATHER**, `E`, or gamepad south. The short prototype renewal interval is a fixed **20 seconds**.

## Renewal loop

1. With **WAYROOT RESTORED** visible in the HUD, gather the existing **Wildflower**, **Young Tree**, or **Stone Outcrop** completely.
2. Confirm its physical resource visual disappears and its world label changes to `RENEWING 0:20` (then counts down). Confirm feedback announces the gathered item and its return time; the HUD's final line reports the concise next renewal status.
3. Wait up to 20 seconds. Confirm one `RESOURCE RETURNED` feedback message, the world label returns to its resource name, the physical node returns, and gathering is available again.
4. Repeat with all three nodes if desired. Each keeps its own recorded deadline; no new resource types or reward system appear.

## Restart and reset

1. Gather a restored-Wayroot resource, note its renewal countdown, then stop/restart Play mode before the deadline. Confirm the same node remains absent/renewing and its label/HUD continue from the saved deadline rather than a fresh interval.
2. Repeat, but restart after the recorded deadline. Confirm the node loads available and its renewal save record is cleared.
3. Press **RESET** at any point. After reload, confirm inventory, Wayroot restoration, depleted/renewing labels, and renewal state are all cleared; all three original resources are available.

## Regression and limits

- Recheck movement/touch controls, pause, gathering, combat/core, merchant upgrade, shelter, companion, Wayroot prerequisites/restoration, restart, and reset.
- This is local prototype persistence. It has no notifications, background/offline reward processing, daily timers, network save, shop/crafting expansion, or Phase 12 content.
- Device Simulator and Windows review builds remain desktop evidence only. Run the physical-iPhone process in `Documentation/Phase9iPhoneBlockers.md` before making an iPhone-ready claim.

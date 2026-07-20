# Phase 9 Full-Loop Manual Playtest Checklist

**Purpose:** execute the entire approved vertical slice as a tester, not a developer. Record each result; do not mark an unperformed step as passed. This checklist adds no Phase 10 behavior.

## Test record

- Date / build commit:
- Tester:
- Platform, device, OS, and orientation:
- Input used: touch / keyboard / gamepad
- Fresh reset confirmed: yes / no
- Result: pass / fail / blocked
- Defects, screenshots, or video:

## Setup and presentation

- [ ] Start `Assets/Game/Scenes/Bootstrap.unity` in Play Mode or a development review player.
- [ ] Use **landscape**. Confirm the safe-area layout keeps the title, joystick, gather/attack controls, pause, and reset controls usable and unobscured.
- [ ] Confirm **WAYROOT | SUNMEADOW**, the **MOVE** joystick, **HOLD GATHER**, **HOLD ATTACK**, **PAUSE**, and **RESET** are legible without hiding the world.
- [ ] Identify each labelled landmark without consulting the hierarchy: wildflower/petal, young tree/timber, stone outcrop/stone, merchant/iron edge, shelter/build plot, Mossling/companion, and slime/hold attack.
- [ ] Move with touch; spot-check keyboard (WASD/arrows) and gamepad left stick if available. Confirm pause/resume through the UI and Escape/PAUSE.
- [ ] Zoom with pinch or mouse wheel. Note any camera obstruction/fade issue, clipped geometry, unreadable label, or control overlap.

## Fresh-reset progression loop

- [ ] Press **RESET** and confirm the reset feedback appears, the scene reloads, and the inventory/progression presentation returns to its fresh state.
- [ ] At the wildflower, use **HOLD GATHER** / `E` / gamepad south. Confirm the action card and one PETAL reward.
- [ ] At the young tree, repeat gather until it yields the expected TIMBER; at the stone outcrop, repeat until it yields the expected STONE. Confirm depleted-node behavior is understandable and the HUD updates.
- [ ] Approach Mossling and use the semantic gather action. Confirm companion feedback and a following creature that keeps an understandable buffer rather than colliding with the player.
- [ ] Approach the slime and use **HOLD ATTACK** / Space. Confirm hit feedback, defeat feedback, the CORE reward, and eventual respawn feedback.
- [ ] Let the slime defeat the player once. Confirm player health/respawn feedback, recovery to a playable state, and no broken controls.
- [ ] Gather/retain one PETAL and one CORE, then interact at **MERCHANT / IRON EDGE**. Confirm the upgrade consumes the displayed cost exactly once and changes `WEAPON 0/1` to `1/1` and attack from 1 to 2. Also verify the missing-cost feedback before resources are sufficient if practical.
- [ ] Obtain three TIMBER and three STONE, then interact at **SHELTER / BUILD PLOT**. Confirm the cost is spent once, the shelter becomes visible, and the build-plot label no longer distracts. Attempt a repeat interaction and confirm it does not spend again.

## Restart, reset, and regressions

- [ ] Exit/stop and relaunch without pressing RESET. Confirm saved depleted nodes, inventory/progression, weapon level, shelter, and befriended Mossling restore as designed; Mossling begins at the shelter home until approached.
- [ ] Pause during ordinary movement and during/near an interaction; resume and confirm no stuck input, duplicated reward, or frozen AI.
- [ ] Press **RESET**, relaunch, and confirm all persisted progression above is cleared.
- [ ] Watch the DEV line during one representative loop. Record noticeable sustained stutter, input lag, or an FPS drop; this is an observation, not a performance pass/fail threshold.

## Result and escalation

- **Pass:** all executed checks work, no blocker found, and observations are recorded.
- **Fail:** capture the smallest reproduction, expected/actual result, platform/input, and screenshot/video. Do not silently fix it under Phase 9 unless it is a high-confidence, isolated quality correction approved by scope.
- **iPhone:** also complete `Phase9iPhoneBlockers.md`; a Device Simulator or desktop pass cannot close the physical-device gate.

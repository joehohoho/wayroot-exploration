# Phase 6 Manual Test — Friendly Mossling Companion

## Start

1. Open `Assets/Game/Scenes/Bootstrap.unity` and press **Play** (or run the Phase 6 Windows review player).
2. Confirm the green, yellow-eared primitive **Friendly Mossling (hold E)** is visible near the northwest side of the test ground and the HUD says `MOSSling nearby`.

## Befriend and follow

1. Walk beside the Mossling with **WASD/arrow keys**, the **left gamepad stick**, or the touch joystick.
2. Hold **E**, **gamepad south button**, or the on-screen **HOLD GATHER** button once in range.
3. Confirm the HUD reports `MOSSling befriended`, then walk away. The creature follows but stops roughly 1.5 world units from the player rather than overlapping or blocking movement.
4. Pause while it follows; confirm it stops. Resume and confirm it can continue following.

## Persistence and reset

1. With the Mossling befriended, stop and restart Play mode (or close and relaunch the review player).
2. Confirm it is restored at the shelter plot/home position in the southwest and remains there until the player walks up to it; then confirm it resumes the safe follow behavior.
3. Click **RESET**. Confirm the scene reloads with the Mossling back at its original wild location, the HUD says `MOSSling nearby`, and it requires befriending again.

## Regression spot check

- Confirm existing gathering, shelter build, merchant upgrade, combat, keyboard, gamepad, and touch controls still operate through the shared semantic actions.
- The creature is deliberately primitive in Phase 6; no external art assets are required.

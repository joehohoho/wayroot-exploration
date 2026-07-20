# Phase 5 Manual Test Guide — Homestead Shelter

Use Unity **6000.5.4f1** and open `Assets/Game/Scenes/Bootstrap.unity`.

## One repeatable homestead loop

1. Press **RESET** before testing so the prototype starts from a known save state, then press **Play**. Confirm the blue player, resource nodes, gold merchant, and the gold-brown **Shelter Build Plot (hold E)** in the lower-left part of the test ground are visible.
2. Confirm the upper-right HUD includes `SHELTER 3 TIMBER + 3 STONE` and shows the current TIMBER and STONE counts.
3. Walk to the green `Young Tree (hold E)` and hold **E** (or hold the green **GATHER** touch control) until it disappears. Confirm TIMBER becomes 3.
4. Walk to the gray `Stone Outcrop (hold E)` and hold the same action until it disappears. Confirm STONE becomes 3.
5. Walk within range of the Shelter Build Plot. The HUD must show `HOLD E / GATHER: SHELTER: 3 TIMBER + 3 STONE`.
6. Hold **E**, gamepad south button, or the green **GATHER** button once. Confirm the HUD says `SHELTER built: home is ready.`, TIMBER and STONE each decrease from 3 to 0, the HUD changes to `SHELTER BUILT`, and the plot gains the brown-walled, red-roofed primitive shelter.
7. Release and press the interaction again while in range. Confirm `SHELTER already built.` appears and no resources are spent.
8. Stop Play and enter Play again. Confirm the built shelter is immediately visible, the plot is green, the HUD says `SHELTER BUILT`, and the spent resources/depleted nodes remain saved.
9. Press **RESET**, then enter Play again. Confirm the shelter is absent, the plot returns gold-brown, HUD returns to `SHELTER 3 TIMBER + 3 STONE`, resources are zero, and the tree/stone are available again.

## Insufficient-resource check

1. After RESET, gather only the tree or only the stone node, then approach the plot.
2. Interact once. Confirm the HUD reports `Need 3 TIMBER + 3 STONE.`, no shelter appears, and the resource count you collected does not change.

## Controls and mobile check

- Movement: **WASD**, arrow keys, gamepad left stick, or virtual joystick.
- Gather / build / merchant: **E**, gamepad south button, or green **HOLD GATHER** control.
- Attack: **Space** or red **HOLD ATTACK** touch control.
- Pause: **Escape** or **PAUSE**. Movement, gathering, building, purchases, and attacks must stop while paused.
- In Device Simulator, select an iPhone landscape preset and confirm joystick, GATHER, ATTACK, PAUSE, RESET, shelter prompt/status, and the expanded upper-right HUD remain readable and inside the safe area.

## Scope and limitations

This Phase 5 slice provides exactly one fixed build plot and one fixed-cost shelter. It does not add free placement, rotation, demolition, storage, crafting expansion, farming, NPC housing, multiple plots, creatures, or Phase 6 work. Physical-iPhone validation still requires a Mac/Xcode worker.

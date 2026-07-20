# Phase 4 Manual Test Guide — Merchant Weapon Upgrade

Use Unity **6000.5.4f1** and open `Assets/Game/Scenes/Bootstrap.unity`.

## One repeatable progression loop

1. Press **Play**. Confirm the Game view contains the blue player, red `Practice Slime (hold SPACE)`, and a gold **Iron Edge Merchant Station** near the lower-right area of the test ground.
2. Check the upper-right HUD. It displays PETAL, TIMBER, STONE, CORE, `WEAPON 0/1`, and `ATK 1` on a fresh prototype save.
3. Walk to the pink wildflower and hold **E** (or press and hold the green **GATHER** touch button) until it disappears. Confirm PETAL becomes 1.
4. Walk to the slime and hold **Space** (or the red **ATTACK** touch button) while in range. Defeat it once; confirm CORE increases by exactly 1. The slime may respawn after its normal delay.
5. Walk within range of the gold merchant station. The HUD must state: `IRON EDGE: 1 PETAL + 1 CORE -> ATK 2`.
6. Hold **E** or the green **GATHER** touch button once. Confirm the HUD reports `IRON EDGE purchased: ATK 1 -> 2`, PETAL and CORE decrease to 0, and the HUD shows `WEAPON 1/1  ATK 2`.
7. Defeat the respawned slime while in range. Its health should fall in three attacks rather than five. CORE increases by exactly one for that new defeat.
8. Stop Play and enter Play again. Confirm `WEAPON 1/1`, `ATK 2`, the spent PETAL/CORE values, and depleted flower state restore from the local save.
9. Return to the merchant and try the interaction again. Confirm the HUD says `IRON EDGE already purchased: ATK 2.` and no resource is spent.
10. Press **RESET**. Re-enter Play and confirm resources, weapon level, attack damage, and depleted nodes return to their fresh-prototype state.

## Controls and mobile check

- Movement: **WASD**, arrow keys, gamepad left stick, or virtual joystick.
- Interact/Gather/Purchase: **E**, gamepad south button, or the green **HOLD GATHER** touch control.
- Attack: **Space**, or red **HOLD ATTACK** touch control.
- Pause: **Escape** or **PAUSE**. Purchases, gathering, movement, and attacks must stop while paused.
- In Device Simulator, select an iPhone landscape preset and confirm joystick, GATHER, ATTACK, PAUSE, RESET, merchant feedback, and the expanded upper-right HUD remain readable and inside the safe area.

## Scope and limitations

This is a controlled Phase 4 progression slice only: one slime drop, one merchant station, and one capped weapon upgrade. It does not add a crafting graph, multiple weapons, equipment, skills, quests, NPC dialogue, final art, or any Phase 5 homestead work. Physical-iPhone validation still requires a Mac/Xcode worker.

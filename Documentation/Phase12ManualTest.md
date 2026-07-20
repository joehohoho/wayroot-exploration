# Phase 12 Manual Test — Shelter Rest and Return

1. Open `Assets/Game/Scenes/Bootstrap.unity`, press **Play**, and use **RESET** for a fresh check.
2. Before building the shelter, allow the slime to defeat the player. Confirm the feedback reports `RESPAWNED AT DEFAULT SPAWN`.
3. Gather **3 TIMBER** and **3 STONE**, walk to the shelter plot, and interact with **E**, gamepad south, or **HOLD GATHER** to build it.
4. Return to the completed shelter and interact once more. Confirm the label becomes **SHELTER / ACTIVE HOME**, health returns to 10, and feedback confirms the active return point.
5. Take damage from the slime, then rest at the shelter. Confirm health fully restores.
6. Let the slime defeat the player. Confirm feedback reports `RESPAWNED AT ACTIVE SHELTER` and the player appears beside the shelter with full health.
7. Stop and restart Play mode. Confirm the built shelter remains active as the return point, then repeat the respawn check.
8. Press **RESET**. Confirm the shelter is unbuilt and player defeats return to the default spawn again.

## Scope

This update adds one shelter rest/return point only. It does not add fast-travel, multiple homes, sleep/day-night, storage, crafting, farming, or any other home system.

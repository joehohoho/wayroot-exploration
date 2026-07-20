# Phase 13 Manual Test — Restored Grove and Thorn Guardian

1. Open `Assets/Game/Scenes/Bootstrap.unity`, press **Play**, then use **RESET**.
2. On the fresh state, confirm the combat HUD reads **GROVE LOCKED: RESTORE WAYROOT**. The labelled **THORN GUARDIAN** and the restored-grove edge must not be present.
3. Complete the existing controlled loop: gather resources, defeat the Practice Slime for a core, purchase **IRON EDGE**, build/rest at the shelter, then restore the Wayroot using **E**, gamepad south, or **HOLD GATHER**.
4. Confirm the clearing changes visibly at the grove edge and the feedback reports **RESTORED GROVE OPEN: THORN GUARDIAN AWAITS**. The HUD must read **GROVE OPEN: GUARDIAN 8 HP / 2 DMG**.
5. Walk into the labelled **RESTORED GROVE / THORN GUARDIAN**. Use the existing **HOLD ATTACK** touch control (or Space on desktop) until it is defeated. Confirm every defeat awards exactly one existing **CORE** and no new item/currency appears.
6. Confirm the guardian deals two contact damage per hit (it is tougher than the 5 HP / 1 damage slime). Let it defeat the player after resting at the shelter; confirm the established **RESPAWNED AT ACTIVE SHELTER** return still works.
7. Wait 15 seconds after guardian defeat. Confirm it returns at its fixed grove home with 8 HP and feedback reports **THORN GUARDIAN RESPAWNED**.
8. Stop and restart Play mode with a restored Wayroot save. Confirm the grove and guardian are already open. Press **RESET** and confirm the grove locks again.

## Scope

This is one post-Wayroot guardian encounter in existing Sunmeadow. It reuses attack, player health, shelter return, and Slime Core systems only. It does not add regions, enemy roster expansion, skills, ranged combat, boss phases, quests, currencies, or Phase 14 work.

# Phase 8 Manual Test — Mobile Playability and Polish

## Start

1. Open `Assets/Game/Scenes/Bootstrap.unity` and press **Play**, or run the Phase 8 Windows review player.
2. Confirm the landscape HUD has a centered **WAYROOT | SUNMEADOW** title card, separated left **MOVE** joystick, right **HOLD GATHER** and **HOLD ATTACK** controls, and high-contrast **PAUSE**/**RESET** buttons within the safe area.
3. In an Editor or development build, confirm the small **DEV** line reports FPS, renderer count, zoom, and pause state without covering the touch controls.

## Identifiers and actions

1. Locate every labeled landmark without looking at the hierarchy: **WILDFLOWER / PETAL**, **YOUNG TREE / TIMBER**, **STONE OUTCROP / STONE**, **MERCHANT / IRON EDGE**, **SHELTER / BUILD PLOT**, **MOSSling / COMPANION**, and **SLIME / HOLD ATTACK**. After construction, verify **SHELTER / HOME** becomes visible while the build-plot label does not distract.
2. Use **HOLD GATHER** / `E` / gamepad south at resources. Confirm a brief lower-center action card reports the gathered item.
3. Befriend Mossling and verify its action card confirms the companion status; restart and verify the established persistence behavior remains intact.
4. Defeat the slime with **HOLD ATTACK** / Space. Confirm hit/defeat/core feedback, then wait for the concise respawn feedback. Let the slime defeat the player to confirm the health-restored respawn feedback.
5. Gather the existing purchase materials and interact at the merchant; verify success or missing-cost feedback. Build the shelter with its established costs; verify success or missing-cost feedback.
6. Use **PAUSE** or Escape, resume, then click **RESET**. Confirm the action confirmation appears before the fresh scene reloads and existing saved progression is cleared.

## Regression and scope

- Repeat the Phase 1–7 movement, gathering, combat/drop, merchant, shelter, and companion spot checks. Touch, keyboard, and gamepad retain the same semantic commands.
- The new labels and HUD use built-in Unity primitives/materials/text only. This is a controlled landscape-touch polish slice; it adds no Phase 9 gameplay, iOS signing/build, Android adaptation, external assets, or new economy.

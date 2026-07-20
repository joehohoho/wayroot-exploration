# Phase 17 Manual Test — Moonlit Glade Exploration

## Scope and reset

This is a desktop review of the compact Moonlit Glade extension, not physical-iPhone evidence. Open the project in Unity **6000.5.4f1**, open `Assets/Game/Scenes/Bootstrap.unity`, and enter Play mode. Use **RESET** before and after the route to clear prototype progress; this removes the glade unlock as well as the existing save data.

The route intentionally uses no map, fast travel, new currency, crafting material, merchant, dialogue, or second boss chain. It reuses only Wild Petal, Timber, Stone, normal combat, the existing Mossling guide, renewal loop, soundscape, and shelter return point.

## Locked-to-open route

1. Complete the retained Wayroot path: gather resources, acquire Iron Edge, build the shelter, obtain a Slime Core, and restore the Wayroot.
2. Walk to the newly visible **RESTORED GROVE / THORN GUARDIAN** at the northwest edge. Before defeating the guardian, confirm the nearby violet **MOONLIT GLADE / SEALED PATH** marker is unmistakably sealed and there are no Moonlit Glade nodes to gather or guide toward.
3. Defeat the Thorn Guardian once with the existing ATTACK control. Confirm the existing defeat sound family plays, HUD feedback announces **MOONLIT GLADE OPEN**, the seal disappears, and the compact violet-blue path and clearing appear immediately.
4. Follow the path into the compact glade. Confirm the warm stylized Bloomwell landmark has a soft violet/blue point glow and small floating-mote visual reward, while retaining readable labels and no dense/mobile-costly decoration.

## Existing loops in the glade

1. Gather **MOON PETAL / WILD PETAL**, **MOON SAPLING / TIMBER**, and **MOON STONE / STONE** with the unchanged hold-E control. Confirm each adds only the established resource type and begins the shared 20-second renewal loop.
2. If the Mossling is befriended, confirm its nearest-resource marker/trail can select an available Moonlit node and, after all nodes are depleted, falls back to the existing nearest-renewal text without marking a depleted node.
3. Confirm pause, SOUND ON/OFF, combat, player defeat, and shelter return remain available while moving between Sunmeadow, the Grove, and the Glade.

## Persistence and reset

1. After opening the glade, stop Play and enter Play again. Confirm the path remains open, the Bloomwell and three glade nodes compose, and expired node renewal deadlines restore normally.
2. Press **RESET**. After the scene reloads, confirm the Moonlit Glade path is sealed/unavailable again and no Moonlit nodes are registered until the Guardian is defeated again.

## Regression checks

- The Thorn Guardian still uses its established health, contact damage, 15-second respawn, and Slime Core reward; the first defeat additionally records only the exploration unlock.
- Existing Sunmeadow gathering, renewal, Wayroot, shelter, Mossling, soundscape, pause, and combat behavior remain unchanged.
- The moonlit colors, landmark light, and static motes are original runtime geometry/effects; no external assets or new economies were added.

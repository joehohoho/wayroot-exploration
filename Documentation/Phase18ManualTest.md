# Phase 18 Manual Test — Major Stylized Art and Animation

## Purpose

Review the cohesive original runtime-composed art and procedural-motion pass. This is presentation only: it must not alter interactions, combat values, economy, unlocks, saves, controls, or safe-area HUD behavior.

## Start

1. Open `Assets/Game/Scenes/Bootstrap.unity` and enter Play mode, or launch `Builds/Phase18Review/WayrootPhase18.exe`.
2. For a clean visual route use **RESET**. Use WASD/arrow keys or the MOVE pad; hold E/GATHER for interaction and Space/ATTACK to attack.

## Immediate composition review

- Player has a warmer layered silhouette (cloak, scarf, hair, lantern glow) with breathing at idle, stride/bob/facing while moving, cloak/lantern secondary swing, and a brief readable gather/attack emphasis.
- Mossling has a leaf cap and cheek glow; ears/tail/body have a springy idle/follow rhythm, becoming more lively after befriending.
- Practice Slime has an animated shell and gold attack pulse with distinct squash/stretch idle and hit/defeat/respawn emphasis. After restoring the Wayroot, the Thorn Guardian gets its separate animated shell/pulse as well.
- Trees, flowers, creek lilies/ripples, Wayroot heart/crown, and unlocked Bloomwell motes are visibly alive without shifting their interaction roots.

## Retained-loop regression

1. Gather WILDFLOWER, YOUNG TREE, and STONE OUTCROP; confirm labels remain readable and the objects remain reachable.
2. Attack the Slime; confirm contact, health, core reward, defeat, and respawn remain unchanged while the combat motion reads clearly.
3. Befriend Mossling; confirm it follows and still points to the nearest available resource/renewal state.
4. Build/rest at shelter, purchase Iron Edge, restore Wayroot, defeat Thorn Guardian, and enter Moonlit Glade. Confirm saves persist through restart; RESET returns the same pre-Phase-18 state.
5. Inspect iPhone-landscape-safe controls if a device is available. The HUD/buttons should remain unobscured and labels should remain compact and horizontal.

## Expected runtime hierarchy anchors

`Prototype Player/Player Lantern Glow`, `Friendly Mossling (hold E)/Mossling Leaf Cap`, `Practice Slime (hold SPACE)/Slime Animated Shell`, `Sunmeadow Creek/Creek Ripple A`, and each root's `ProceduralStylizedAnimator` component are Phase 18 evidence.

## Scope note

This is desktop review evidence only; it does not replace the independently-blocked physical-iPhone validation.

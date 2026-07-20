# Phase 7 Manual Test — Sunmeadow Clearing

## Start

1. Open `Assets/Game/Scenes/Bootstrap.unity` and press **Play**, or run the Phase 7 Windows review player.
2. Confirm the scene reads as a bounded Sunmeadow clearing: a bright green meadow, warm diagonal footpath, blue creek and shore to the east, north-tree grove, south rock garden, and clustered colored flowers.
3. Confirm the HUD, **PAUSE**, **RESET**, joystick, **HOLD GATHER**, and **HOLD ATTACK** remain readable above the new palette.

## Visual landmarks and loops

- Player begins at the clearing center; the pink wildflower remains northwest, young tree northeast, and stone outcrop southwest.
- The gold merchant remains near the north path; the shelter plot/home remains southwest; the green/yellow Mossling remains in the northwest meadow; the red slime remains east of center.
- Walk the path and around the creek edge with keyboard, gamepad, or touch. Decorative trees, rocks, and flowers must not block player movement.
- Check the tree/rock/flower silhouettes against the improved warm directional lighting and ambient sky/ground palette. There must be no magenta materials.

## Regression spot check

1. Use **HOLD GATHER** / `E` / gamepad south at each resource and at the Mossling.
2. Use **HOLD ATTACK** / Space at the slime, then verify merchant upgrade still works after collecting the existing required resources/drop.
3. Build the shelter after collecting the existing timber and stone requirement; restart to confirm the established shelter and companion persistence behavior still works.
4. Click **RESET** and confirm the existing prototype progression resets while the Sunmeadow presentation returns.

## Scope note

This is a controlled primitive/URP visual-region slice only. It adds no new progression, economy, quest, biome, streaming, or external art asset.

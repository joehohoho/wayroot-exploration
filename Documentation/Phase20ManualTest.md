# Phase 20 Manual Test — Gentle Journey Guidance

## Purpose

Review the compact, non-blocking Journey cue that makes the existing route understandable without a quest system, map, rewards, or automatic movement.

## Start and controls

1. Open `Assets/Game/Scenes/Bootstrap.unity` and enter Play mode, or launch `Builds/Phase20Review/WayrootPhase20.exe`.
2. Select **RESET** for a fresh route. Use WASD/arrow keys or MOVE; hold E / HOLD GATHER to interact; hold Space / ATTACK to fight.
3. The warm **JOURNEY** card sits below the title in the safe area. The small green-gold firefly appears only when its current existing target is outside the camera view.

## Guided existing route

1. On fresh RESET, confirm `GATHER WHAT THE MEADOW OFFERS` recommends the existing Wildflower path. Move until it leaves view and confirm the firefly remains safely inset from the screen edge; move back and it disappears.
2. With one PETAL and one CORE saved, restart or progress normally. Confirm the card changes to `VISIT THE IRON EDGE MERCHANT` and points only to the existing merchant when off-screen.
3. Purchase Iron Edge. Confirm the next cue is the existing **SHELTER** build plot; after shelter construction it becomes the dormant **WAYROOT**.
4. Restore Wayroot. Confirm the next cue names the existing **THORN GUARDIAN** in the restored grove; defeat it and follow the existing Moonlit Glade route.
5. Confirm the next cue directs to the existing **BLOOMWELL**. It does not add a marker, item, reward, gate, map, dialogue, or navigation behavior.
6. Restore Bloomwell. Confirm the card reads `BLOOMWELL RESTORED — EXPLORE FREELY` and no firefly pointer is required, while gathering, renewal, companion, combat, shelter, and existing controls remain playable.

## Persistence and reset

- Restart at any listed milestone: the Journey card must choose the same next cue only from the local saved progress.
- Select **RESET**, restart, and confirm the initial meadow-gathering cue returns.
- Review in a landscape/safe-area configuration: Journey must not overlap the title, inventory, action feedback, movement, GATHER, ATTACK, PAUSE, SOUND, or RESET controls.

## Scope note

Desktop validation only; it does not replace the separately blocked physical-iPhone validation.

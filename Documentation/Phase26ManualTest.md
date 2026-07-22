# Phase 26 Manual Test — Combat Encounter Polish

## Review target

Open `Assets/Game/Scenes/Bootstrap.unity` and press Play. This is desktop evidence for the mobile-first composition, not physical-iPhone validation.

## Fresh-route Slime review

1. Begin from RESET and move to the Practice Slime.
2. Hold **ATTACK** (or the red mobile-safe attack button) in range.
3. Confirm each hit produces a short golden swing trail, contact flash/ground marker, and a compact player pulse without covering the explorer, Slime, or touch controls.
4. Let the Slime reach the player. Its coral ground cue and forward arc should appear briefly before contact; use the existing dodge control to confirm its readable Phase 21 trail remains intact.
5. Defeat it. Confirm the orange defeat marker persists only while it is down, then the coral respawn ring briefly appears when the unchanged five-second respawn completes.

## Guardian arena review

1. Restore the existing Wayroot through its current requirements, then enter the Restored Grove.
2. Confirm the four thin lime boundary accents and Guardian threat-focus ring frame the existing encounter without blocking movement or hiding the actor.
3. Let the Guardian enter contact range: its larger lime anticipation ground cue and forward arc must appear before its existing contact damage.
4. Attack it using the unchanged control/value loop. Confirm gold hit flashes, a defeat marker, and a brief lime respawn cue on its unchanged 15-second return.
5. Verify the Guardian health, contact damage, core reward, Moonlit Glade unlock, attack cooldown/range, movement, save/restart, and RESET behavior remain unchanged.

## Mobile composition checks

- HUD title/cards and left MOVE/right ATTACK/DODGE controls remain unobstructed.
- Effects are compact and short-lived; actor sprites/health bars remain readable.
- No new inventory, progression, save, economy, enemy, or UI system is present.

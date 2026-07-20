# Phase 16 Manual Test — Cozy Magical Soundscape

## Scope and reset

This is a desktop review of the original runtime-generated procedural audio layer; it is not physical-iPhone evidence. Open the project in Unity **6000.5.4f1**, open `Assets/Game/Scenes/Bootstrap.unity`, and enter Play mode. Use **RESET** before and after the route to clear prototype progress; RESET also restores **SOUND ON**.

Use normal speakers/headphones at a low, comfortable level. The intended tone is quiet, warm, and magical—not arcade-like.

## Sound controls and persistence

1. In the safe-area HUD, confirm the compact left-side control reads **SOUND ON** below PAUSE and does not overlap movement/action controls or status text.
2. Toggle it to **SOUND OFF**. Confirm the label changes, the ambient exploration bed stops, and gather/combat/UI feedback is silent.
3. Stop Play and enter Play again. Confirm **SOUND OFF** remains selected.
4. Toggle back to **SOUND ON**. Confirm the label updates and a small gentle UI chime plus a quiet ambient bed return.
5. Press **RESET**, re-enter Play if the scene reloads, and confirm the default is **SOUND ON**.

## Audible feedback route

With SOUND ON, check each action remains gentle and distinct:

1. Gather the wildflower/tree/stone: a short soft leaf/wood-like two-note cue plays. Wait for the 20-second renewal: a brighter but quiet return chime plays.
2. Hold ATTACK near a Slime or unlocked Thorn Guardian: normal hits use a muted low impact; defeat uses a small resolving magical tone. Let the player be defeated to confirm the same restrained defeat/respawn family—not a harsh alarm.
3. Build then interact with the shelter to rest: hear a warm settling/rest cue.
4. Complete the existing requirements and restore the Wayroot: hear the longest gentle bloom/chime cue as the visual restoration occurs.
5. Befriend the Mossling: hear a tiny friendly guide chime. Its existing visible guide remains non-combat and does not auto-gather.
6. Tap PAUSE/RESUME and RESET: UI controls have soft click feedback while sound is enabled.

## Regression checks

- Gathering, renewal, combat, shelter return, Wayroot restoration, Mossling guide, pause, and RESET behavior remain unchanged beyond audio feedback.
- SOUND OFF suppresses the ambient bed and all procedural cues.
- No downloaded/imported audio assets or music are used; `ProceduralSoundscape` makes all clips at runtime.
- Batch/headless execution remains safe: no audio source/clip work is required when `Application.isBatchMode` is true.

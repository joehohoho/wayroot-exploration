# Phase 2 Manual Test Guide — Gathering Loop

1. Open `Assets/Game/Scenes/Bootstrap.unity` with Unity **6000.5.4f1** and press **Play**.
2. Confirm the upper-right HUD says `Move close to a resource` and shows PETAL, TIMBER, and STONE counts.
3. Walk to the pink wildflower. The prompt should name it and show `0/1`. Hold **E**; it disappears and PETAL becomes 1.
4. Walk to the green young tree and gray stone outcrop. Hold **E** until each progress prompt reaches `3/3`; the matching resource count becomes 1 and the node disappears.
5. Repeat with the on-screen green **HOLD GATHER** button. It must behave exactly like holding E.
6. Press PAUSE during gathering. Player movement and gathering progress must stop; resume and confirm controls work.
7. Stop Play and re-enter Play. Gathered nodes and inventory should restore from the Phase 2 local prototype save.
8. In Device Simulator, choose a landscape iPhone preset and confirm joystick and HOLD GATHER remain inside the safe area.

## Reset prototype data

To reset the local Phase 2 gathering state, delete `wayroot-phase2-gathering.json` from Unity's application persistent-data folder, then re-enter Play mode. This is development-only; a player-facing reset/save-slot screen is deferred.

## Limits

This phase proves a controlled gathering loop only: three primitive resources, no tools, crafting, economy, audio, animation, final art, or iPhone hardware validation.

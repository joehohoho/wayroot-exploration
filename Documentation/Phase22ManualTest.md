# Phase 22 Manual Test — Alpha Presentation Overhaul

1. Open `Assets/Game/Scenes/Bootstrap.unity` and enter Play mode at 16:9 landscape / review player resolution.
2. Confirm the centre remains clear: no oversized, mirrored, inverted, or screen-spanning world labels.
3. Walk near a flower, tree, stone, merchant, shelter, Wayroot, guardian, and Bloomwell. Confirm each uses a compact upright marker and contextual prompt instead of stacked world text.
4. Confirm HUD hierarchy: compact health/combat at upper-left, Journey at upper-centre, resource/progression card at upper-right; the normal build does not display debug `ZOOM`/`RUNNING` status.
5. Verify PAUSE/SOUND stay away from the central play area and touch MOVE/DODGE/ATTACK/GATHER remain separated in bottom safe areas.
6. Review paths, creek, trees, resources, shelter, merchant, Grove, and Moonlit Glade: terrain/material layering, color grouping, lighting, and silhouettes should read as a coherent stylized alpha scene.
7. Complete a few existing interactions and use RESET; confirm visual cleanup did not change controls, requirements, save behavior, or gameplay.

Desktop/Device Simulator evidence only; this is not a physical-iPhone release validation.

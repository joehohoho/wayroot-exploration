# Game Design — Working Specification

## Vision

Wayroot Exploration is a casual, all-ages, stylized top-down 3D mobile game centred on relaxed exploration, resource gathering, crafting, equipment progression, homestead building, original creature discovery, optional accessible combat, and open-ended play. It is peaceful, humorous, magical, adventurous, and friendly without copying any existing game’s assets, names, creatures, worlds, or interfaces.

## Provisional lore

The Wayroots once connected distant regions. The Great Tangle disrupted them, isolating settlements and altering some creatures. The player discovers a dormant Wayroot, builds a homestead around it, and reconnects regions by exploration, gathering, and helping locals. Story stays optional and light.

## Approved product rules

- iPhone first; landscape; touch-first; local offline play in prototype/vertical slice.
- A player can stop safely after five minutes or continue a long session.
- Homesteads, settlements, and sanctuaries do not spawn hostile creatures.
- Defeat is forgiving: home respawn, equipped/inventory/currency retained, no durability or corpse retrieval.
- Permanent regions use deterministic seeded generation and retain deltas; only the active region and transition data load.
- Controlled prototype uses a small handcrafted Sunmeadow-inspired area before full generation.
- No ads, loot boxes, battle pass, pay-to-win, paid power, paid inventory, paid crafting acceleration, or paid required regions.

## Planned touch controls

Left analog movement joystick. Right-side main/charged attacks, future abilities, interaction, and weapon switch. Hold-to-repeat avoids rapid tapping. Safe-area layouts, left/right-handed variants, scalable UI/text, subtitles, high-contrast prompts, camera-shake toggle, separate audio controls, and reduced flashing are architecture requirements.

## Vertical-slice ceiling

One homestead, one controlled adventure area, one later partial generated region, three weapons, three normal enemies, one elite, one optional boss, three gathering activities, ~20 resources, ~15 recipes, two stations, one merchant, one quest NPC/chain, one collectible creature, English/local saves/iPhone only.

## Deferred

Networking, accounts, cloud saves, monetization, Addressables, fishing, weather, seasons, extra biomes, mounts, boats, extensive creature systems, and Android adaptation.

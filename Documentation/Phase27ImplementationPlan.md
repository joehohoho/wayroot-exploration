# Phase 27 Implementation Plan — Shelter and Homestead Cozy-Art Pass

**Authorization:** Owner selected scope option 3 on 2026-07-22.

## Goal

Make the existing shelter/rest-home location feel like a memorable, warm magical homestead and unmistakable player return point, without expanding it into a housing or crafting system.

## Visible scope

- Strengthen the built-shelter silhouette with original runtime-composed roof, warm window/lantern glow, foundation, and compact garden/path dressing.
- Improve the build-to-rest visual transition so an incomplete build plot remains clear but the completed shelter feels durable and inviting.
- Add restrained rest/home feedback: a short warm pulse, hearth/lantern ambience, and clear return-point framing when the shelter is used.
- Improve composition around the existing shelter so it remains readable against the Sunmeadow and never blocks the explorer, resource nodes, HUD, touch controls, or existing prompts.
- Keep existing Reset, rest/heal, home-respawn, construction costs, and persistence behavior intact.

## Boundaries

- No interior, furniture placement, farming, crafting expansion, currency, inventory, NPC, map, fast travel, new buildable, resource, requirement, control, save-schema, or combat change.
- Use only original source-controlled runtime geometry/materials/C# and existing procedural audio/visual patterns.

## Acceptance criteria

- The unbuilt plot and built shelter are visually distinct at mobile scale.
- Built shelter has a cozy readable home silhouette and rest/return feedback.
- Existing 3 TIMBER + 3 STONE cost, shelter completion, full-heal rest, home respawn, persistence, and RESET behavior are unchanged.
- Compile, focused EditMode/PlayMode coverage, Windows review build/smoke, actual player visual review, docs, clean commit, and push pass.

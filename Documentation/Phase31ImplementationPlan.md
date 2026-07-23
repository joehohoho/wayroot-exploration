# Phase 31 Implementation Plan — Combat Feel and Environmental Ambience

**Authorization:** Owner selected scope options 2 and 3 on 2026-07-23.

## Goal

Improve moment-to-moment combat feel and cohesive world ambience using original procedural audio/runtime visual presentation, while preserving every existing mechanic and progression value.

## Scope

### Combat feedback
- Enrich existing player attack, dodge, hit, enemy anticipation, defeat, respawn, and Guardian moments using original procedural audio and compact visual cues.
- Add mobile-ready optional haptic abstraction/hooks only; desktop behavior must remain fully functional.

### Environment ambience
- Improve restrained existing ambience across Sunmeadow, Grove, Glade, shelter, and Bloomwell through original lighting/particle/foliage/water motion and procedural sound layering.
- Keep effects mobile-readable and non-obstructive.

## Boundaries

- No damage, cooldown, range, health, reward, enemy, resource, control, save schema, map, quest, currency, crafting, region, day/night clock, weather system, or progression change.
- Do not introduce external assets or platform APIs that prevent Windows builds.

## Acceptance

- Combat actions have clearer, richer feedback without altering gameplay behavior.
- Existing regions feel more coherent and alive without HUD, actor, or touch-control occlusion.
- SOUND preference remains respected; RESET/persistence behavior unchanged.
- Compile, EditMode/PlayMode, Windows player build/smoke, actual focused screenshot inspection, docs, clean commit/push pass.

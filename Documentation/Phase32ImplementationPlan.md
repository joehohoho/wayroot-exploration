# Phase 32 Implementation Plan — Accessibility and Clarity Pass

**Authorization:** Owner directed continued development after Phase 31. This is the lowest-risk bounded enhancement for the established route.

## Goal

Make the current mobile-first prototype easier to read and more comfortable to play without changing its mechanics or adding a settings framework.

## Scope

- Strengthen text, interaction-marker, enemy-telegraph, resource, and objective contrast where existing presentation needs it.
- Add compact persistent accessibility preferences within the existing HUD/settings pattern: reduced flash/impact presentation and reduced ambient motion.
- Ensure SOUND preference remains independent; RESET restores accessibility defaults and existing save behavior is preserved.
- Improve semantic labels and touch-control readability only.

## Boundaries

- No gameplay values, input bindings, map, quest, dialogue, economy, region, character, accessibility remapping system, or save schema change.
- No external assets, fonts, plugins, platform APIs, or broad UI redesign.

## Acceptance

- Preferences visibly reduce flash/motion without hiding required gameplay feedback.
- Existing controls, safe-area UI, route, persistence/reset, and sound behavior remain correct.
- Compile/tests/Windows build/smoke/focused player inspection/docs/commit/push pass.

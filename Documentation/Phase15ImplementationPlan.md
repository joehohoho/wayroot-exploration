# Phase 15 Implementation Plan — Mossling Resource Guide

**Authorization:** Owner approved the next phase after the Phase 14 art-direction review on 2026-07-20.

## Goal

Give the befriended Mossling one calm, useful exploration role without turning it into a combat pet, automation system, or navigation UI: it guides the player toward the nearest available renewable resource node.

## Locked player contract

- After befriending the Mossling, its visual focus/marker identifies the nearest currently available flower, tree, or stone node.
- If all nodes are renewing, it communicates the nearest renewal countdown rather than pointing at a depleted node.
- Guidance updates safely as player/node state changes and respects Wayroot/renewal persistence.
- The companion remains non-combat: no attacks, damage, loot, autonomous gathering, mounts, stats, or skills.
- RESET clears befriending and guidance; restart restores the existing companion persistence behavior.

## Presentation

- Use the approved warm stylized magical language: soft glowing motes, a small leaf/spark trail, compact readable status, and no large map overlay.
- Preserve mobile readability and existing HUD/world-label placement.

## Acceptance criteria

- A fresh unbefriended Mossling offers no resource guidance.
- Befriend it, then clearly observe guidance to an available node; gather that node and see guidance choose another available node or renewal status.
- Compile, EditMode/PlayMode coverage, Windows build/smoke, manual guide, and Git hygiene pass as one package.

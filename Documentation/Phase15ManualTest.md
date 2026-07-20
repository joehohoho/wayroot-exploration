# Phase 15 Manual Test — Mossling Resource Guide

## Setup

1. Open `Assets/Game/Scenes/Bootstrap.unity` with Unity **6000.5.4f1** and press Play.
2. Select **RESET** for the fresh-state path. This clears befriending, guide state, resources, and the existing prototype save.

## Guide loop

1. From a fresh state, approach the Mossling by the flower clusters and hold **E** (or **GATHER**) once.
2. Confirm its small glowing leaf/spark marker and three motes lead to the nearest currently available flower, tree, or stone node. Confirm the compact inventory line says `MOSSling GUIDE → …`; no map overlay appears.
3. Gather the marked flower. Confirm the marker and trail immediately choose an available tree or stone instead; the Mossling never gathers it itself, attacks, grants loot, mounts the player, or exposes stats/skills.
4. Deplete all three existing nodes. Confirm no marker is left above a depleted node and the compact guide line reports the nearest renewing node plus its countdown.
5. Wait for a renewal. Confirm the returned node becomes a valid guide target with the leaf/spark marker again.

## Persistence and regression

1. Befriend the Mossling, stop Play, then press Play again without RESET. Confirm it restores at the shelter and resumes guidance when the player approaches.
2. Use **RESET**, restart Play, and confirm the Mossling is no longer befriended and there is no guide marker/trail/status.
3. Retain the existing gathering, merchant, shelter/rest, Wayroot, grove, combat, respawn, pause, and reset checks from earlier phases.

This is desktop evidence only; it is not a physical-iPhone performance or device-validation claim.

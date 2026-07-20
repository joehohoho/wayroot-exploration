# Phase 4 Implementation Plan — Progression Vertical Slice

**Authorization:** Owner approved beginning the next phase on 2026-07-20.

## Goal

Turn the controlled gathering/combat sandbox into a small repeatable progression loop:

> Gather materials → defeat a slime → earn a combat drop → spend resources at one merchant station → improve the prototype weapon.

## Locked first delivery

- Keep current Phase 1–3 controls, save behavior, and placeholder presentation.
- Add a combat-drop resource, clear reward feedback, a merchant-station primitive, and one visible weapon-upgrade purchase.
- Persist the new resource and upgrade level in the existing local prototype save.
- Weapon upgrade increases the single attack's damage; it does not add new weapons, abilities, quests, or a crafting graph.
- No NPC dialogue tree, broad equipment, skills, procedural content, networking, economy balancing, or final art.

## Acceptance criteria

- Defeated slime awards exactly one combat-drop resource per defeat.
- The combat/drop count and weapon level are visible in HUD and survive Play-mode restart.
- Player can interact with merchant station using existing semantic Gather/Interact input and purchase one upgrade when resources are sufficient.
- Upgrade visibly changes attack damage and the cost/reward status.
- Full compile, EditMode, PlayMode, Windows build, and manual guide are completed before the next owner review.

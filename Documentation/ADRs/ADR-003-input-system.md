# ADR-003: Use Unity Input System

## Status
Accepted — 2026-07-17

## Context
Touch controls are primary, while keyboard/controller-compatible development input must not couple game rules to UI buttons.

## Decision
Use device-independent Input System actions routed into player commands. Do not use the legacy Input Manager.

## Alternatives
Legacy Input Manager is prohibited and weaker for multi-device actions. Direct UI callback movement is rejected because it couples presentation to gameplay and harms future compatibility.

## Consequences
Phase 1 defines actions and validates desktop mappings; a separate ADR chooses uGUI or UI Toolkit for the virtual controls.

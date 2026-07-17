# ADR-002: Use Universal Render Pipeline

## Status
Accepted — 2026-07-17

## Context
Stylized top-down 3D targets broad-range iPhones first and needs scalable quality.

## Decision
Use URP.

## Alternatives
Built-in pipeline is rejected in favour of Unity’s modern mobile path. HDRP is rejected as unsuitable for the mobile-first performance budget.

## Consequences
All materials/shaders must be URP compatible; transparency, shadows, overdraw, and post-processing require device profiling.

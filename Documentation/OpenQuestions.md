# Open Questions

## Open owner decisions

1. **Final title/repository name:** `Wayroot Exploration` / `wayroot-exploration` is a working title derived from provisional lore. Decide before branding, bundle IDs, or public asset naming become permanent.
2. **Placeholder character and presentation:** retain primitives only, or approve licensed placeholder art/UI/audio in a separately scoped slice? No unvalidated asset may enter tracking.
3. **iPhone worker and signing:** identify a compatible Mac/Xcode machine, Apple signing owner, supported-device/iOS policy, and secure signing process before the physical-device checkpoint.
4. **Release acceptance:** decide whether the default launch presentation, Unity splash, provisional app identity, and prototype-only visual content are acceptable for a device review; they are not assumed store-ready.
5. **Phase 11 selection:** choose or defer the next bounded vertical slice only after reviewing the completed Phase 10 Wayroot objective. Do not assume a resource-renewal, crafting, quest, or region expansion follows automatically.

## Resolved

- Engine: Unity **6000.5.4f1**, owner-approved and pinned for Phases 0–9.
- Pipeline: URP.
- Input: Unity Input System, version **1.19.0**, with `activeInputHandler: 1`.
- Runtime touch UI: **uGUI**, selected in ADR-007 for its proven pointer/virtual-joystick path.
- Saving: versioned plain save records plus seed/delta world persistence.
- State: ScriptableObjects for static authored definitions and plain C# types for mutable state.
- Phase 9 scope: production-readiness evidence and a decision package only; no automatic Phase 10 implementation.
- Phase 10: one finite Sunmeadow Wayroot objective requires 3 PETAL + 3 TIMBER + 3 STONE + 1 CORE, an acquired Iron Edge upgrade, and a built shelter. Completion is one persistent clearing bloom; RESET returns it to dormant.

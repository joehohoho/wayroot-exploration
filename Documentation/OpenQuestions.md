# Open Questions

## Open owner decisions

1. **Final title/repository name:** `Wayroot Exploration` / `wayroot-exploration` is a working title derived from provisional lore. Rename later before branding, bundle IDs, or public asset naming become permanent.
2. **Placeholder character:** primitives only, or an approved/licensed placeholder asset? Phase 1 uses primitives; no unvalidated asset may enter production tracking.
3. **iPhone worker:** identify compatible Mac/Xcode machine and signing owner before Phase 1’s physical-device checkpoint.
4. **Phase 1 review:** confirm the keyboard/controller/touch prototype feels right after Editor and physical-iPhone testing before approving Phase 2 interaction and gathering.

## Resolved

- Engine: Unity **6000.5.4f1**, owner-approved and pinned for Phase 0/1 because Unity Hub offers no suitable 6.0 LTS update.
- Pipeline: URP.
- Input: Unity Input System, version **1.19.0**, with `activeInputHandler: 1`.
- Runtime Phase 1 touch UI: **uGUI**, selected in ADR-007 for its proven pointer/virtual-joystick path.
- Saving: versioned plain save records plus seed/delta world persistence.
- State: ScriptableObjects for static authored definitions and plain C# types for mutable state.

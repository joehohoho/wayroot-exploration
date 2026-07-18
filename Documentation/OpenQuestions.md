# Open Questions

## Open owner decisions

1. **Final title/repository name:** `Wayroot Exploration` / `wayroot-exploration` is a working title derived from provisional lore. Rename later before branding, bundle IDs, or public asset naming become permanent.
2. **Runtime UI:** Phase 1 needs a narrow, documented uGUI vs UI Toolkit touch-control spike. Default recommendation: Unity UI/uGUI for familiar touch ergonomics.
3. **Placeholder character:** primitives only, or an approved/licensed placeholder asset? No unvalidated asset may enter production tracking.
4. **iPhone worker:** identify compatible Mac/Xcode machine and signing owner before Phase 1’s mobile checkpoint.

## Resolved in Phase 0

- Engine: Unity **6000.5.4f1**, owner-approved and pinned for Phase 0/1 because Unity Hub offers no suitable 6.0 LTS update.
- Pipeline: URP.
- Input: Unity Input System.
- Saving: versioned plain save records plus seed/delta world persistence.
- State: ScriptableObjects for static authored definitions and plain C# types for mutable state.

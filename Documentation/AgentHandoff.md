# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-23 status

- **Phase 30 Mossling Companion Presence is complete and desktop-validated.** Do not begin Phase 31 without explicit owner approval.
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`; pinned Unity **6000.5.4f1**.
- **Implementation:** `MosslingPresenceRules` selects idle/follow/guide/arrival/renewal visual states from existing Mossling guide state only. `MosslingPresencePresentation` adds visual-only unscaled, URP-safe accents and no colliders. Gameplay root, guidance target, follow behavior, controls, progression, persistence, and RESET remain unchanged.
- **Validation:** compile passed; EditMode **78 passed, 0 failed**; PlayMode **33 passed, 0 failed**; Phase 30 Windows review build passed; 8-second smoke passed (expected timeout 124). Visible review player capture had no Unity magenta; manual friendly-state demonstration remains in `Documentation/Phase30ManualTest.md`.
- **Do not stage:** Logs/, TestResults/, Builds/, screenshot captures, Library/, or Unity ProjectSettings formatting churn.

## Next phase

When owner approves Phase 31: inspect status/diff/docs first, define a bounded plan, preserve original-art-only materials and unscaled visual roots, validate before publishing.

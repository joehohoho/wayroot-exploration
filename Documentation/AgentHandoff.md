# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-22 status

- **Phase 25 Exploration Art and Mobile Composition is complete and desktop-validated.** Do not begin Phase 26 without explicit owner approval.
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Implementation:** `ExplorationCompositionRules` defines original region palettes, landmark names, safe dressing positions, and focal-zone protection. `GameBootstrap` uses these profiles to enrich existing Sunmeadow, Restored Grove, Moonlit Glade, and Bloomwell visual composition using only runtime geometry and existing route gates.
- **Scope preserved:** no new region, map, fast travel, NPC, quest, resources, currency, crafting, combat values, unlock requirements, or save-schema changes. Existing actor presentation, HUD, touch controls, gathering/renewal, Mossling, Wayroot/Guardian/Bloomwell gates, restart, and RESET remain intact.
- **Validation:** Unity batch compile passed; EditMode **62 passed, 0 failed**; PlayMode **29 passed, 0 failed**; `Wayroot.Editor.PhaseTwentyFiveBuild.BuildWindowsReviewPlayer` passed with `Build Finished, Result: Success.` The ignored review output is `Builds/Phase25ExplorationArtReview/WayrootPhase25.exe`.
- **Manual review:** `Documentation/Phase25ManualTest.md`. Desktop evidence remains distinct from physical-iPhone validation.
- **Do not stage:** `Logs/`, `TestResults/`, `Builds/`, `Library/`, screenshot captures, or Unity-generated `ProjectSettings` whitespace churn.

## First actions when Phase 26 is approved

1. Inspect `git status --short --branch`, `git diff --check`, Roadmap, OpenQuestions, and this handoff.
2. Define the new scope and acceptance criteria in `Documentation/Phase26ImplementationPlan.md` before implementation.
3. Preserve the existing route and original-art-only constraint unless the approved plan says otherwise.
4. Validate compile, EditMode, relevant PlayMode, Windows review build/smoke, and actual player review before publishing.

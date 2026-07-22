# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-21 status

- **Phase 24 Actor Presentation Cleanup is complete and owner-confirmed.** Phase 25 is owner-authorized, but its scope remains deliberately undefined: create and approve a Phase 25 implementation plan before writing feature code.
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Phase 24 presentation:** existing gameplay roots, colliders, controls, persistence, health bars, and combat behavior remain intact. Procedural player/slime/guardian sprite rigs retain semantic action selection. The player-build path uses the source-controlled `Assets/Game/Resources/ActorSpriteUnlit.mat` material and a per-rig material instance. Compact original fallback silhouettes preserve readable actor presence when renderer compatibility is constrained.
- **Owner review:** owner confirmed the player and enemies are visible again after the correction.
- **Current validation:** Unity batch compile/build succeeded; EditMode **60 passed, 0 failed**; PlayMode **28 passed, 0 failed**. The Windows review output is intentionally ignored at `Builds/Phase24SpritePolishReview/WayrootPhase24.exe`.
- **Known environment warnings:** Unity retains LicenseClient handshake/access-token messages and older nullable-annotation/obsolete API warnings in existing code. Do not stage generated `Logs/`, `TestResults/`, `Builds/`, `Library/`, visual evidence captures, or Unity-generated `ProjectSettings` whitespace churn.

## First actions for Phase 25

1. Inspect `git status --short --branch`, `git diff --check`, Roadmap, OpenQuestions, and this handoff.
2. Define and record the Phase 25 scope, risks, acceptance criteria, non-goals, and tests in `Documentation/Phase25ImplementationPlan.md` before implementation.
3. Preserve the established route, existing mobile controls, persistence, and original-art-only constraint unless the approved plan explicitly changes them.
4. Run compile, EditMode, relevant PlayMode, a Windows review build, and actual player review after source changes.

# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 18 major stylized art and procedural-animation pass is implemented and desktop-validated. **Do not begin Phase 19 without explicit owner approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Owner-approved Phase 18 scope files (`AGENTS.md`, `Documentation/Phase18ImplementationPlan.md`) are intentional and ship with this milestone.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Implementation:** `ProceduralStylizedAnimator` uses only per-frame transform composition on visual child primitives. It does not move gameplay roots/colliders. `GameBootstrap` adds layered player/Mossling/enemy silhouettes, animated water/foliage/Wayroot/Bloomwell pieces, and motion rigs. Existing gather/attack calls pulse player visuals; enemy hit/defeat/respawn pulse their own visual shells.
- **Coverage:** `PhaseEighteenArtAnimationPlayModeTests` checks player/Mossling/slime/creek rig composition, animated child movement while roots stay stable, active enemy collider/label readability, and no duplicated player rig on scene restoration.
- **Tests:** batch compile passed; EditMode **42/42** and PlayMode **18/18** passed.
- **Windows review:** `Wayroot.Editor.PhaseEighteenBuild.BuildWindowsReviewPlayer` passed, creating ignored `Builds/Phase18Review/WayrootPhase18.exe` (667,648 bytes; 158 MB directory). Eight-second headless smoke stayed alive until intentional timeout 124 with no captured application exception, missing-shader, or error text.
- **Manual review:** follow `Documentation/Phase18ManualTest.md`; desktop evidence is not physical-iPhone validation.
- **Known environment warnings:** Unity retains LicenseClient handshake/access-token messages and older obsolete `FindObjectsByType` overload warnings. Do not stage generated `Logs/`, `TestResults/`, `Builds/`, `Library/`, or Unity-generated `ProjectSettings` whitespace churn.

## First actions when resuming

1. Inspect `git status --short --branch`, `git diff --check`, and the staged Phase 18 diff.
2. Commit/push the cohesive Phase 18 milestone, verify `HEAD == origin/main`, then replace this note with the published SHA.
3. Physical-iPhone validation remains independently blocked by the documented Mac/Xcode/signing/device requirements.

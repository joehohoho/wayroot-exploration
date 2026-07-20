# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-20 status

- **Current milestone:** Phase 16 cozy magical procedural soundscape is implemented and desktop-validated. **Do not begin Phase 17 without explicit owner approval.**
- **Repository:** `Wayroot Exploration` / `wayroot-exploration`, branch `main`. Owner-approved Phase 16 scope changes in `AGENTS.md` and `Documentation/Phase16ImplementationPlan.md` are intentional and ship with this milestone.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`). Do not change the project pin without owner approval.
- **Implementation:** `ProceduralSoundscape` makes original runtime PCM clips only: a low-volume looping exploration bed and small warm profile-driven cues. It returns before allocating audio sources/clips in `Application.isBatchMode`. Existing gather/renewal, hit/defeat, player defeat, shelter rest, Wayroot restoration, Mossling befriend, and PAUSE/RESET routes are wired without gameplay changes. `SoundToggleButton` gives a compact safe-area `SOUND ON/OFF` control.
- **Persistence:** Phase-16 `soundEnabled` lives with the versioned gathering save. Migration 7→8 defaults legacy saves to enabled; `PrototypeGatheringSaveService.Reset()` deletes the save, so a subsequent default load is SOUND ON.
- **Tests:** Red-first compile correctly failed for absent `Wayroot.Audio`. After implementation, batch compile passed; EditMode **40/40** and PlayMode **13/13** passed. PlayMode covers sound preference restart persistence, label state, and RESET default restoration.
- **Windows review:** `Wayroot.Editor.PhaseSixteenBuild.BuildWindowsReviewPlayer` passed, creating ignored `Builds/Phase16Review/WayrootPhase16.exe` (667,648 bytes; 158 MB directory). Eight-second headless smoke stayed alive until intentional timeout 124 with no captured application exception, missing-shader, or error text.
- **Manual review:** follow `Documentation/Phase16ManualTest.md` for the audible action route, control persistence, and reset. Windows evidence is not physical-iPhone validation.
- **Known environment warnings:** Unity retains LicenseClient handshake/access-token messages. Batch compile also reports existing obsolete `FindObjectsByType(FindObjectsSortMode)` warnings in the old Mossling PlayMode test. Do not stage generated `Logs/`, `TestResults/`, `Builds/`, `Library/`, or any Unity-generated ProjectSettings whitespace churn.

## First actions when resuming

1. Inspect `git status --short --branch`, `git diff --check`, `git log --oneline -5`, and compare `HEAD` to `origin/main`.
2. Review the full staged Phase 16 diff, keep unrelated generated files out, then commit and push the cohesive milestone.
3. Fetch and verify `HEAD == origin/main`; replace this pending note with the published SHA.
4. Physical iPhone validation remains independently blocked by the documented Mac/Xcode/signing/device requirements.

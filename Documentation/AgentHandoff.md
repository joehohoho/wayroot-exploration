# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-18 status

- **Current milestone:** Phase 0 is complete and verified. Do **not** start Phase 1 until the owner reviews and approves it.
- **Working title/repository:** `Wayroot Exploration` / `wayroot-exploration`; the owner may rename it later.
- **Pinned editor:** Unity **6000.5.4f1** (`d550df8bd089`), owner-approved because Unity Hub did not offer the prior target patch.
- **Resolved packages:** Input System **1.19.0**, URP **17.5.0**, Unity Test Framework **1.7.0**. Earlier Input System 1.11.2/1.13.1/1.14.2 packages fail to compile against Unity 6000.5 because Unity APIs used by those package releases are obsolete errors.
- **Validation completed:** batch compilation and EditMode test runs exit `0`; `ProjectIdentity_UsesConfiguredNamespaceRoot` passes (1 total, 1 passed, 0 failed). `activeInputHandler` is explicitly set to `1` (Input System package), preventing the Unity 6000.5 invalid-setting error.
- **Validation command detail:** Test runs must omit `-quit`; the Test Runner exits itself and writes the XML result. See `AGENTS.md`.
- **Git/GitHub:** inspect `git status --short --branch`, `git log --oneline -5`, and remote state before work. Commit/push the Phase 0 editor-validation changes once final whitespace/source checks pass.

## First actions when resuming

1. Finish/verify the focused Phase 0 validation commit and push it.
2. Request owner review of Phase 0; do not implement movement yet.
3. After explicit approval, use `Documentation/Phase1ImplementationPlan.md` as the Phase 1 scope, inspect the repo, and report plan/risks/files before implementation.

## Resume prompt

> Read AGENTS.md, AgentHandoff, Roadmap, OpenQuestions, and ADRs. Inspect Git and the actual Unity installation. Continue only the owner-approved milestone. Preserve the pinned Unity/package configuration. Update this handoff before stopping.

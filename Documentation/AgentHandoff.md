# Agent / Codex Handoff

Read this file before resuming after a quota reset/session loss, then update it before stopping.

## 2026-07-17 status

- **Current milestone:** Phase 0 only; do not start Phase 1 until owner review.
- **Working title/repository:** `Wayroot Exploration` / `wayroot-exploration`; owner says the name may change later.
- **Pinned project editor:** Unity 6000.0.78f1 (Unity 6.0 LTS).
- **Local editor:** Unity 6000.5.4f1 only. Do not open/migrate this project with it.
- **Verification:** project structure and text files need local validation; Unity compile/Test Framework remains blocked until the pinned editor is installed/licensed.
- **Git/GitHub:** inspect `git status --short --branch`, `git log --oneline -5`, and remote state before work.

## First actions when resuming

1. Confirm Unity 6000.0.78f1 is installed/licensed.
2. Run the exact compile/EditMode commands from `AGENTS.md`.
3. If successful, update this handoff/Roadmap, commit/push Phase 0, and request owner review.
4. Only after that review, execute the Phase 1 plan.

## Resume prompt

> Read AGENTS.md, AgentHandoff, Roadmap, OpenQuestions, and ADRs. Inspect Git and the actual Unity installation. Continue only the approved milestone; run pinned-editor verification before unrelated changes; update this handoff before stopping.

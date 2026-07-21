# Wayroot Exploration — Agent Instructions

## Current scope
- **Current milestone:** Phase 19 Bloomwell restoration finale is owner-approved and underway. Do not begin Phase 20 without approval.
- Unity **6000.5.4f1** is owner-approved and pinned for Phases 0–19. Do not change `ProjectSettings/ProjectVersion.txt` without owner approval.
- Use C#, URP, Unity Input System, TextMeshPro, and Unity Test Framework. The legacy Input Manager is prohibited.
- iPhone/landscape/touch is primary; desktop is development-only.
- No networking, Addressables, accounts, cloud save, analytics, purchases, backend, paid package, or Android adaptation during prototype work unless explicitly scheduled.

## Before each milestone
1. Inspect `git status`, Roadmap, OpenQuestions, AgentHandoff, and relevant ADRs.
2. Report concise plan, dependencies, risks, expected files, and acceptance criteria.
3. Record unresolved design choices in `Documentation/OpenQuestions.md`; do not invent them.
4. Keep each commit focused and do not alter unrelated working systems.

## Code conventions
- Namespace root: `Wayroot`; one primary type per file; explicit access modifiers.
- Use `[SerializeField] private` for inspector wiring. Prefer serialized references, constructors for pure C# code, or a small composition root; avoid singleton-heavy design.
- Static authored data belongs in `Assets/Game/Data`; mutable runtime state and save records are plain C# types, not ScriptableObjects.
- Runtime code is feature-oriented at `Assets/Game/Code/<Feature>`; tests mirror features under `Assets/Game/Tests`.
- Avoid public mutable fields, global static player state, scene-wide searches, frame-path LINQ, major gameplay in `Update`, raw MonoBehaviour persistence, and `async void` except Unity event handlers.

## Do not commit
`Library/`, `Temp/`, `Obj/`, `Logs/`, `UserSettings/`, generated IDE files, `Builds/`, build artifacts, secrets, signing profiles/keys, or `.env` files.

## Validation required after a milestone
```bash
"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" -batchmode -nographics -quit -projectPath "$PWD" -logFile Logs/compile.log
"/c/Program Files/Unity/Hub/Editor/6000.5.4f1/Editor/Unity.exe" -batchmode -nographics -projectPath "$PWD" -runTests -testPlatform EditMode -testResults TestResults/editmode.xml -logFile Logs/editmode-tests.log
```
Also run relevant PlayMode tests, inspect `git diff --check`, update documentation, and report actual errors/warnings. Never claim a feature works without verification. Update `Documentation/AgentHandoff.md` before every stop so a reset Codex session can resume.

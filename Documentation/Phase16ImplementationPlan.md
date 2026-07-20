# Phase 16 Implementation Plan — Cozy Magical Soundscape

**Authorization:** Owner approved the next phase after confirming Phase 15 on 2026-07-20.

## Goal

Add a compact, original audio layer that completes the warm magical-world direction without requiring external asset packs, narration, or a music-generation dependency.

## Locked player contract

- A low-key procedural ambient bed supports exploration without obscuring interaction feedback.
- Distinct, soft feedback sounds communicate gathering, resource renewal, combat hit/defeat, shelter rest, Wayroot restoration, Mossling guidance, and UI actions.
- A visible compact **SOUND** control toggles the whole prototype soundscape and persists the preference through restart; RESET restores default enabled audio.
- All generated audio is original, runtime-produced, brief/mobile-conscious, and runs safely in batch/headless test contexts.

## Presentation

- Tone: cozy, adventurous, magical—gentle wood/leaf/chime-like colors rather than harsh arcade beeps or combat-heavy music.
- Respect the established safe-area UI and never cover action controls/status text.

## Exclusions

- No external audio packs, copyrighted music, voices, generated songs, paid libraries, settings-menu expansion, spatial-audio system, or Phase 17 gameplay work.

## Acceptance criteria

- Key existing actions are audibly distinguishable when sound is enabled.
- Sound toggle is visible, safe-area-aware, persistent, and testable.
- Current gameplay and reset/persistence behavior remain intact.
- Compile, EditMode/PlayMode coverage, Windows build/smoke, documentation, and Git hygiene pass together.

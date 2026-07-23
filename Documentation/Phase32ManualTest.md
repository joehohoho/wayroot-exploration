# Phase 32 Manual Test — Accessibility and Clarity

1. Start from **RESET**. Confirm **SOUND ON**, **FLASH LESS: OFF**, and **MOTION LESS: OFF**. SOUND remains independent from both visual-comfort controls.
2. Tap **FLASH LESS**. Its label must change to **ON**. Attack the existing Practice Slime and let it take a hit: the compact contact and enemy-hit feedback remains visible, but its flash duration and pulse are noticeably reduced. Combat values, range, cooldown, rewards, and controls must be unchanged.
3. Tap **MOTION LESS**. Observe the creek, foliage, motes, landmark accents, Mossling, idle enemies, and player idle presentation. They remain present but use markedly smaller movement. Move, dodge, fight, and gather: required movement and interaction feedback remain available.
4. Restart the app after enabling each preference. Confirm both settings persist independently. Tap **RESET** and restart; both must return to **OFF**, while the existing prototype reset behavior remains intact.
5. Check the compact upper-left settings stack and all HUD cards in landscape: labels are bold/high contrast; FLASH LESS and MOTION LESS do not overlap PAUSE, SOUND, or touch controls. Resource, objective, contextual-prompt, and feedback cards remain legible over the world.
6. Complete a retained-route smoke path: gather one resource, toggle SOUND, engage the Slime, use dodge, and RESET. No input binding, gameplay value, save-schema, objective, or route behavior should change.

## Desktop evidence

- Windows development review player: `Builds/Phase32AccessibilityClarityReview/WayrootPhase32.exe` (ignored build output).
- Focused player capture: `Logs/phase32-player-capture.png` (ignored evidence). The capture shows the game, high-contrast upper-left settings stack, both new controls, and no overlap among the setting buttons or touch controls.

Desktop review is not physical-iPhone validation.

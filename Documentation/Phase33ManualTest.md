# Phase 33 Manual Test — Mobile Readiness and Performance

## Desktop review path

1. Start from **RESET** in a landscape player. Confirm the existing upper-left HUD stack, **PAUSE**, **SOUND**, **FLASH LESS**, **MOTION LESS**, objective cards, joystick, GATHER, ATTACK, and DODGE are readable and non-overlapping.
2. Complete a short retained-route path: move with the joystick, gather one existing resource, engage the Practice Slime, dodge, and use RESET. Player, enemies, touch controls, combat feedback, routes, values, and save behavior must be unchanged.
3. The automatic **Mobile Presentation Safeguard** has no settings/menu control. It starts in full presentation and observes sustained frame pressure only after its three-second warm-up. It does not alter gameplay, UI, input, save data, player/enemy visibility, or core combat feedback.
4. Under sustained constrained frames (30 consecutive frames at or slower than 30 FPS), it disables only the existing decorative Phase 31 ripples, foliage sway, and motes. Landmark/gameplay roots remain present. It restores decoration only after 180 consecutive frames at or faster than 45 FPS, preventing rapid visual toggling.
5. Verify at the spawn/player framing that there is no magenta material, missing geometry, or HUD/touch-control occlusion. The player, Practice Slime, HUD, and both lower touch-control zones must remain visible.

## Physical iPhone review checklist — pending

This Windows validation is **not** iPhone performance evidence. Complete the following on a compatible Mac with the pinned Unity editor and iOS Build Support:

- [ ] Full Xcode installed (`xcodebuild -version` succeeds), Xcode license accepted, and compatible iOS SDK confirmed.
- [ ] Apple Developer signing owner, team, bundle identifier, provisioning profile/certificate, and secure signing access supplied; no signing material is committed.
- [ ] Project source at the published Phase 33 commit is pulled on macOS; `Library/` and generated Xcode output are recreated locally.
- [ ] Unity exports an iOS Xcode project successfully.
- [ ] Xcode compiles the exported project; signing is validated separately from unsigned compilation.
- [ ] A supported physical iPhone installs and launches the signed build in landscape.
- [ ] Safe area is checked on notched and non-notched supported devices: upper HUD remains inside safe area, joystick/action cluster is reachable, and no system gesture conflicts are observed.
- [ ] Fresh RESET route is played with touch only: movement, gather, attack, dodge, pause, sound, FLASH LESS, MOTION LESS, combat feedback, persistence/restart, and reset remain usable.
- [ ] Observe at least one sustained constrained-device scenario: decorative ambience may reduce automatically, but player, enemies, HUD, touch UI, and combat feedback must remain visible/readable; confirm recovery does not flicker.
- [ ] Capture device screenshots/video and record device model, iOS version, build SHA, thermal/power state, duration, and observed frame behavior.

## Evidence boundary

Windows build/smoke and focused player capture establish desktop composition only. Mac/Xcode export, signing, physical-iPhone install, touch ergonomics, thermal behavior, battery impact, and device performance remain pending until the checklist is executed.

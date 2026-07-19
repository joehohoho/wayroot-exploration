# Phase 1 Manual Test Guide

Use this guide after the automated verification recorded in the Phase 1 milestone commit.

## Editor test scene

1. Open the project with Unity **6000.5.4f1**.
2. Open `Assets/Game/Scenes/Bootstrap.unity` and press **Play**.
3. Confirm a green test ground, blue capsule player, brown test tree, fixed-angle camera, joystick, pause button, and development overlay appear.
4. Hold **WASD** or the arrow keys. The player should move horizontally and turn toward travel; it must not jump.
5. If a gamepad is connected, move its left stick. Its movement should match keyboard movement.
6. Drag the virtual joystick. It should move the player without requiring repeated taps; release it and confirm movement stops.
7. Use the mouse wheel. Camera distance should stay within its configured range. In Device Simulator, pinch with two simulated touches and confirm the same clamp.
8. Walk until the brown test tree lies between camera and player. Confirm only that tree fades and restores after it no longer blocks the view.
9. Press **Escape** or tap **PAUSE**. Confirm the overlay reports `PAUSED` and player movement stops. Use the same control to resume.
10. In **Window → Analysis → Profiler**, profile 60 seconds of ordinary keyboard movement and camera following. Confirm no recurring managed-allocation spike is attributed to the movement loop or obstruction raycast path.

## Landscape safe-area / handedness check

1. Open **Window → General → Device Simulator** and select an iPhone landscape preset with a notch/safe area.
2. Confirm joystick and pause control remain within the safe region.
3. Select the runtime `Safe Area` object while playing and call `SetLeftHanded(true)` through the Inspector/debugger only if testing the current prototype. Confirm controls mirror without changing player movement semantics.
4. Return it to `false` before leaving the Editor; persistent preferences are intentionally deferred.

## iPhone handoff (not verified on this Windows workstation)

1. On a Mac, install Unity **6000.5.4f1** with **iOS Build Support** and full Xcode.
2. Pull the exact Git commit being tested; do not copy this workstation's `Library/` directory.
3. Open the project, allow a clean import, then set iOS as the target in **File → Build Profiles**.
4. Set landscape orientation, export an Xcode project, configure the Apple signing team/bundle identifier, and build/install through Xcode.
5. Repeat the joystick, pinch, safe-area, pause, obstruction, and 60-second profiler checks on a physical iPhone.
6. Record device model, iOS version, FPS, thermal observation, and any touch/layout issue before approving Phase 2.

## Known limitations

- The scene uses primitive placeholder geometry and runtime-built prototype uGUI; neither is production art/UI.
- Right/left-handed preference persistence, controller remapping, touch simulation automation, camera rotation, interactions, gathering, combat, and saving are deferred.
- A physical iPhone build has not been produced from this Windows workstation.

# Phase 9 iPhone / Mobile Release Blocker Checklist

**Current result:** **BLOCKED — no iOS build or physical-iPhone evidence was produced in this Windows Phase 9 review.** Do not convert this checklist to a pass based on Desktop, Device Simulator, or configuration inspection alone.

## Repository facts observed in Phase 9

| Item | Current source/configuration fact | Status |
|---|---|---|
| Orientation | Portrait autorotation is disabled; both landscape directions are enabled. | Configured; device verification pending |
| iOS minimum | `iOSTargetOSVersionString: 15.0`. | Configured; owner support policy pending |
| Bundle identifier | `com.joehohoho.wayrootexploration`. | Provisional; working title makes this release-blocking |
| Fullscreen | `uIRequiresFullScreen: 1`. | Configured; device verification pending |
| Signing | Apple team ID and manual provisioning profile ID are empty; automatic signing is disabled. | **Blocked** |
| Launch presentation | Default launch-screen type with no custom landscape/portrait asset or storyboard path. | **Review required** |
| iOS project/device evidence | No tracked Xcode project, provisioning file, IPA, XCArchive, or physical-device result. These are intentionally ignored release artifacts. | **Blocked** |

## Required owner/tooling prerequisites

- [ ] Name the Apple developer-account/signing owner; do not commit team IDs, certificates, profiles, or private keys.
- [ ] Provide a Mac with Unity **6000.5.4f1** plus **iOS Build Support**, full Xcode compatible with the selected device/SDK, and access to the signing account.
- [ ] Confirm a final or explicitly temporary bundle identifier, version/build-number policy, and supported iPhone/iOS matrix. The repository title and current identifier are provisional.
- [ ] Decide whether launch presentation, icons, splash behavior, and the Unity splash are acceptable for a prototype device review. Do not assume they are store-ready.
- [ ] Create the generated Xcode project outside source control, configure signing there or through approved secure CI secrets, archive/sign it, and install on a registered device.

## Physical-iPhone validation (must be recorded)

For each device, record model, screen size/notch class, iOS version, Unity version, build number, thermal/power condition, and tester.

- [ ] Install a signed development build on at least one supported iPhone; launch cold and resume from background.
- [ ] Confirm landscape lock/rotation, safe-area placement, home-indicator/system-gesture interaction, and no clipping on a notched/Dynamic Island device.
- [ ] Run every step in `Phase9FullLoopPlaytest.md` using touch only. A desktop keyboard or Device Simulator outcome is supplemental only.
- [ ] Test pinch zoom, joystick drag/cancel/release, gather/attack press-and-hold, pause/resume, reset confirmation, and combat under real multitouch.
- [ ] Profile one full loop in a Development Build on-device. Record median/worst observed frame pacing, obvious memory growth, renderer count/DEV observations, and thermal state. No performance target has been owner-approved, so record observations rather than inventing a threshold.
- [ ] Verify persistence across force quit/relaunch and reset on-device; inspect the behavior after OS low-memory/background interruption if practical.
- [ ] Inspect player/Xcode logs for exceptions, missing shaders/materials, input errors, and signing/install warnings.
- [ ] Repeat critical safe-area/input checks on a second iPhone size class if the intended support matrix has more than one.

## Release decision blockers after device validation

- [ ] Owner accepts the title/brand and bundle identity.
- [ ] Owner accepts the supported-device/iOS policy and observed performance.
- [ ] Owner accepts prototype placeholders or approves a separate art/UI/audio release scope.
- [ ] A signed archive/install record exists outside Git; secrets and signing profiles remain untracked.
- [ ] A human test result and defect disposition exist for every Phase 9 playtest failure.

## Explicit scope boundary

This checklist does not add iOS signing configuration, iOS build output, App Store metadata, analytics, purchases, or Android work. Those require owner approval and secure platform access.

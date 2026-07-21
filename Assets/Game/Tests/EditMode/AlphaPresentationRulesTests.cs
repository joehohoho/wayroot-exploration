using NUnit.Framework;
using UnityEngine;
using Wayroot.Presentation;

namespace Wayroot.Tests.EditMode
{
    public sealed class AlphaPresentationRulesTests
    {
        [Test]
        public void WorldMarkerProfile_IsCompactAndUsesASingleShortLine()
        {
            WorldMarkerProfile profile = AlphaPresentationRules.CreateMarker("Young Tree");

            Assert.That(profile.DisplayName, Is.EqualTo("YOUNG TREE"));
            Assert.That(profile.DisplayName.Contains("\n"), Is.False);
            Assert.That(profile.CharacterSize, Is.LessThanOrEqualTo(AlphaPresentationRules.MaximumMarkerCharacterSize));
            Assert.That(profile.WorldScale, Is.LessThanOrEqualTo(AlphaPresentationRules.MaximumMarkerWorldScale));
        }

        [Test]
        public void HudColumns_ReserveTheCentralPlayFocusAndKeepCardsSeparate()
        {
            HudLayoutProfile profile = AlphaPresentationRules.DefaultHud;

            Assert.That(profile.LeftWidth + profile.CentralWidth + profile.RightWidth, Is.LessThanOrEqualTo(1f));
            Assert.That(profile.CentralWidth, Is.GreaterThanOrEqualTo(0.28f));
            Assert.That(profile.TopCardHeight, Is.LessThanOrEqualTo(0.16f));
            Assert.That(profile.BottomControlClearance, Is.GreaterThanOrEqualTo(0.18f));
        }

        [Test]
        public void CameraProfile_UsesFocusOffsetAndBoundedReviewZoom()
        {
            CameraPresentationProfile profile = AlphaPresentationRules.DefaultCamera;

            Assert.That(profile.MinimumZoom, Is.GreaterThan(0f));
            Assert.That(profile.MaximumZoom, Is.GreaterThan(profile.MinimumZoom));
            Assert.That(profile.StartingZoom, Is.InRange(profile.MinimumZoom, profile.MaximumZoom));
            Assert.That(profile.FocusOffset.y, Is.GreaterThan(0f));
        }
    }
}

using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Wayroot.Creatures;

namespace Wayroot.Tests.EditMode
{
    public sealed class MosslingGuideRulesTests
    {
        [Test]
        public void Select_NearestAvailableNodeWinsRegardlessOfRenewalDeadline()
        {
            List<MosslingGuideCandidate> candidates = new()
            {
                new("Wildflower", new Vector3(5f, 0f, 0f), true, 0),
                new("Young Tree", new Vector3(2f, 0f, 0f), true, 0),
                new("Stone Outcrop", new Vector3(1f, 0f, 0f), false, DateTime.UtcNow.AddSeconds(2).Ticks)
            };

            MosslingGuideSelection selection = MosslingGuideRules.Select(Vector3.zero, candidates);

            Assert.That(selection.Kind, Is.EqualTo(MosslingGuideKind.Available));
            Assert.That(selection.NodeName, Is.EqualTo("Young Tree"));
        }

        [Test]
        public void Select_AllNodesRenewingShowsNearestRenewalWithoutATargetMarker()
        {
            DateTime now = new(2026, 7, 20, 12, 0, 0, DateTimeKind.Utc);
            List<MosslingGuideCandidate> candidates = new()
            {
                new("Wildflower", new Vector3(6f, 0f, 0f), false, now.AddSeconds(4).Ticks),
                new("Stone Outcrop", new Vector3(2f, 0f, 0f), false, now.AddSeconds(9).Ticks)
            };

            MosslingGuideSelection selection = MosslingGuideRules.Select(Vector3.zero, candidates);

            Assert.That(selection.Kind, Is.EqualTo(MosslingGuideKind.Renewing));
            Assert.That(selection.NodeName, Is.EqualTo("Stone Outcrop"));
            Assert.That(selection.RenewalDeadlineUtcTicks, Is.EqualTo(now.AddSeconds(9).Ticks));
            Assert.That(selection.ShowsTargetMarker, Is.False);
        }

        [Test]
        public void Select_NoNodesReturnsQuietUnavailableStatus()
        {
            MosslingGuideSelection selection = MosslingGuideRules.Select(Vector3.zero, new List<MosslingGuideCandidate>());

            Assert.That(selection.Kind, Is.EqualTo(MosslingGuideKind.None));
            Assert.That(selection.ShowsTargetMarker, Is.False);
        }

        [Test]
        public void Select_UsesStableInputOrderForEqualDistanceCandidates()
        {
            List<MosslingGuideCandidate> candidates = new()
            {
                new("Wildflower", new Vector3(2f, 0f, 0f), true, 0),
                new("Young Tree", new Vector3(-2f, 0f, 0f), true, 0)
            };

            MosslingGuideSelection selection = MosslingGuideRules.Select(Vector3.zero, candidates);

            Assert.That(selection.NodeName, Is.EqualTo("Wildflower"));
        }

        [TestCase(false, MosslingGuideKind.None, 0f, MosslingPresenceState.Idle)]
        [TestCase(true, MosslingGuideKind.Renewing, 6f, MosslingPresenceState.Renewal)]
        [TestCase(true, MosslingGuideKind.Available, 6f, MosslingPresenceState.Guide)]
        [TestCase(true, MosslingGuideKind.Available, 1f, MosslingPresenceState.Arrival)]
        [TestCase(true, MosslingGuideKind.None, 0f, MosslingPresenceState.Follow)]
        [TestCase(true, MosslingGuideKind.None, 3f, MosslingPresenceState.Idle)]
        public void Presence_SelectsOnlyVisualStateFromExistingCompanionAndGuideFacts(bool befriended, MosslingGuideKind guideKind, float targetDistance, MosslingPresenceState expected)
        {
            MosslingGuideSelection selection = new(guideKind, "Wildflower", new Vector3(targetDistance, 0f, 0f), 0);
            Vector3 mosslingPosition = expected == MosslingPresenceState.Follow ? new Vector3(3f, 0f, 0f) : Vector3.zero;

            MosslingPresenceState state = MosslingPresenceRules.Select(befriended, selection, Vector3.zero, mosslingPosition);

            Assert.That(state, Is.EqualTo(expected));
        }
    }
}

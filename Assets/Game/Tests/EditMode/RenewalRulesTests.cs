using System;
using NUnit.Framework;
using UnityEngine;
using Wayroot.Gathering;

namespace Wayroot.Tests.EditMode
{
    public sealed class RenewalRulesTests
    {
        [Test]
        public void CreateDeadline_UsesTheFixedPrototypeInterval()
        {
            DateTime now = new(2026, 7, 20, 12, 0, 0, DateTimeKind.Utc);

            long deadline = RenewalRules.CreateDeadlineUtcTicks(now);

            Assert.That(deadline, Is.EqualTo(now.AddSeconds(RenewalRules.PrototypeIntervalSeconds).Ticks));
            Assert.That(RenewalRules.FormatRemaining(deadline, now), Is.EqualTo("0:20"));
        }

        [Test]
        public void IsDue_ReturnsFalseBeforeAndTrueAtTheRecordedDeadline()
        {
            DateTime now = new(2026, 7, 20, 12, 0, 0, DateTimeKind.Utc);
            long deadline = now.AddSeconds(20).Ticks;

            Assert.That(RenewalRules.IsDue(deadline, now.AddSeconds(19)), Is.False);
            Assert.That(RenewalRules.IsDue(deadline, now.AddSeconds(20)), Is.True);
        }

        [Test]
        public void Normalize_MigratesLegacyDepletedIdsIntoOneDeadlinePerNode()
        {
            DateTime migrationTime = new(2026, 7, 20, 12, 0, 0, DateTimeKind.Utc);
            PrototypeGatheringSave save = JsonUtility.FromJson<PrototypeGatheringSave>("{\"version\":5,\"depletedNodeIds\":[\"wildflower-01\",\"wildflower-01\",\"young-tree-01\"]}");

            PrototypeGatheringSaveMigration.Normalize(save, migrationTime);

            Assert.That(save.version, Is.EqualTo(6));
            Assert.That(save.depletedNodeIds, Is.Empty);
            Assert.That(save.renewalNodes, Has.Count.EqualTo(2));
            Assert.That(save.renewalNodes[0].renewalDeadlineUtcTicks, Is.EqualTo(migrationTime.AddSeconds(20).Ticks));
            Assert.That(save.renewalNodes[1].renewalDeadlineUtcTicks, Is.EqualTo(migrationTime.AddSeconds(20).Ticks));
        }

        [Test]
        public void Normalize_PreservesExistingDeadlineAcrossRestart()
        {
            DateTime now = new(2026, 7, 20, 12, 0, 0, DateTimeKind.Utc);
            long recordedDeadline = now.AddSeconds(7).Ticks;
            PrototypeGatheringSave save = new()
            {
                version = 6,
                renewalNodes = new() { new RenewalNodeSave { nodeId = "stone-outcrop-01", renewalDeadlineUtcTicks = recordedDeadline } }
            };

            PrototypeGatheringSaveMigration.Normalize(save, now.AddSeconds(5));

            Assert.That(save.renewalNodes, Has.Count.EqualTo(1));
            Assert.That(save.renewalNodes[0].renewalDeadlineUtcTicks, Is.EqualTo(recordedDeadline));
            Assert.That(RenewalRules.IsDue(save.renewalNodes[0].renewalDeadlineUtcTicks, now.AddSeconds(7)), Is.True);
        }
    }
}

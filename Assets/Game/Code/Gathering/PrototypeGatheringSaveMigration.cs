using System;
using System.Collections.Generic;

namespace Wayroot.Gathering
{
    /// <summary>Normalizes versioned gathering saves and migrates Phase 10 depleted IDs to Phase 11 renewal deadlines.</summary>
    public static class PrototypeGatheringSaveMigration
    {
        public const int CurrentVersion = 7;

        public static void Normalize(PrototypeGatheringSave save, DateTime utcNow)
        {
            save.depletedNodeIds ??= new List<string>();
            save.renewalNodes ??= new List<RenewalNodeSave>();

            Dictionary<string, RenewalNodeSave> uniqueNodes = new(StringComparer.Ordinal);
            foreach (RenewalNodeSave renewal in save.renewalNodes)
            {
                if (renewal != null && !string.IsNullOrWhiteSpace(renewal.nodeId) && renewal.renewalDeadlineUtcTicks > 0 && !uniqueNodes.ContainsKey(renewal.nodeId))
                {
                    uniqueNodes.Add(renewal.nodeId, renewal);
                }
            }

            long migratedDeadline = RenewalRules.CreateDeadlineUtcTicks(utcNow);
            foreach (string nodeId in save.depletedNodeIds)
            {
                if (!string.IsNullOrWhiteSpace(nodeId) && !uniqueNodes.ContainsKey(nodeId))
                {
                    uniqueNodes.Add(nodeId, new RenewalNodeSave { nodeId = nodeId, renewalDeadlineUtcTicks = migratedDeadline });
                }
            }

            save.renewalNodes = new List<RenewalNodeSave>(uniqueNodes.Values);
            save.depletedNodeIds.Clear();
            save.version = CurrentVersion;
        }
    }
}

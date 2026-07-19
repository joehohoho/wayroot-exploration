using System.Collections.Generic;

namespace Wayroot.Inventory
{
    public enum ResourceType
    {
        WildPetal,
        Timber,
        Stone
    }

    /// <summary>Mutable, bounded runtime inventory; persistence and UI remain separate adapters.</summary>
    public sealed class InventoryState
    {
        public const int MaxStackSize = 99;

        private readonly Dictionary<ResourceType, int> _counts = new();

        public int GetCount(ResourceType resource)
        {
            return _counts.TryGetValue(resource, out int count) ? count : 0;
        }

        public bool TryAdd(ResourceType resource, int quantity, out int accepted, out int remainder)
        {
            accepted = 0;
            remainder = 0;
            if (quantity <= 0)
            {
                return false;
            }

            int current = GetCount(resource);
            accepted = System.Math.Min(quantity, MaxStackSize - current);
            remainder = quantity - accepted;
            if (accepted == 0)
            {
                return false;
            }

            _counts[resource] = current + accepted;
            return true;
        }
    }
}

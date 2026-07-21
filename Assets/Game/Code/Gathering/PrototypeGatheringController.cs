using System;
using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Building;
using Wayroot.Combat;
using Wayroot.Input;
using Wayroot.Inventory;
using Wayroot.UI;
using Wayroot.Wayroot;
using Wayroot.Audio;
using Wayroot.Art;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wayroot.Gathering
{
    public sealed class PrototypeGatheringController : MonoBehaviour
    {
        private const float Range = 2.8f, StepInterval = 0.45f;
        private readonly List<GatheringNode> _nodes = new();
        private PrototypeInputReader _input = null!;
        private PrototypePlayerController _player = null!;
        private InventoryState _inventory = null!;
        private PrototypeGatheringSave _save = null!;
        private float _nextStepTime;
        private ActionFeedbackHud? _feedback;
        private ProceduralSoundscape? _soundscape;

        public GatheringNode? CurrentTarget { get; private set; }
        public IReadOnlyList<GatheringNode> Nodes => _nodes;
        public int WeaponLevel => _save.weaponLevel;
        public bool ShelterBuilt => _save.shelterBuilt;
        public bool HasActiveShelterReturnPoint => _save.activeShelterReturnPoint;
        public bool CreatureBefriended => _save.creatureBefriended;
        public bool WayrootRestored => _save.wayrootRestored;
        public bool MoonlitGladeUnlocked => _save.moonlitGladeUnlocked;
        public bool BloomwellRestored => _save.bloomwellRestored;
        public bool SoundEnabled => _save.soundEnabled;
        public int AttackDamage => WeaponUpgradeRules.GetAttackDamage(WeaponLevel);
        public int GetCount(ResourceType resource) => _inventory.GetCount(resource);
        public string RenewalStatus => GetRenewalStatus(DateTime.UtcNow);

        public void SetFeedback(ActionFeedbackHud feedback) => _feedback = feedback;
        public void SetSoundscape(ProceduralSoundscape soundscape) => _soundscape = soundscape;

        public void SetSoundEnabled(bool enabled)
        {
            if (_save.soundEnabled == enabled) return;
            _save.soundEnabled = enabled;
            SaveInventory();
        }

        public bool TryBuildShelter(out string status)
        {
            if (_save.shelterBuilt)
            {
                status = "SHELTER already built.";
                return false;
            }

            if (!ShelterBuildRules.CanBuild(false, _inventory))
            {
                status = $"Need {ShelterBuildRules.TimberCost} TIMBER + {ShelterBuildRules.StoneCost} STONE.";
                return false;
            }

            _inventory.TrySpend(ResourceType.Timber, ShelterBuildRules.TimberCost);
            _inventory.TrySpend(ResourceType.Stone, ShelterBuildRules.StoneCost);
            _save.shelterBuilt = true;
            SaveInventory();
            status = "SHELTER built: home is ready.";
            return true;
        }

        public bool TryRestAtShelter(out string status)
        {
            if (!ShelterRestRules.CanRest(ShelterBuilt))
            {
                status = "SHELTER must be built before resting.";
                return false;
            }

            _save.activeShelterReturnPoint = true;
            SaveInventory();
            status = "SHELTER RESTED: HEALTH RESTORED — HOME ACTIVE.";
            return true;
        }

        public void BefriendCreature()
        {
            if (_save.creatureBefriended) return;
            _save.creatureBefriended = true;
            SaveInventory();
        }

        public bool TryPurchaseWeaponUpgrade(out string status)
        {
            if (WeaponLevel >= WeaponUpgradeRules.MaximumLevel)
            {
                status = "IRON EDGE already purchased: ATK 2.";
                return false;
            }

            if (!WeaponUpgradeRules.CanPurchase(WeaponLevel, _inventory))
            {
                status = $"Need {WeaponUpgradeRules.PetalCost} PETAL + {WeaponUpgradeRules.SlimeCoreCost} CORE.";
                return false;
            }

            _inventory.TrySpend(ResourceType.WildPetal, WeaponUpgradeRules.PetalCost);
            _inventory.TrySpend(ResourceType.SlimeCore, WeaponUpgradeRules.SlimeCoreCost);
            _save.weaponLevel++;
            SaveInventory();
            status = "IRON EDGE purchased: ATK 1 -> 2.";
            return true;
        }

        public bool TryRestoreWayroot(out string status)
        {
            if (_save.wayrootRestored)
            {
                status = "WAYROOT already restored: Sunmeadow is renewed.";
                return false;
            }

            if (!WayrootRestorationRules.TryRestore(false, WeaponLevel >= WeaponUpgradeRules.MaximumLevel, ShelterBuilt, _inventory))
            {
                status = GetWayrootRequirementStatus();
                return false;
            }

            _save.wayrootRestored = true;
            ResolveRenewals(DateTime.UtcNow);
            SaveInventory();
            status = "WAYROOT RESTORED: resources return every 20 seconds.";
            return true;
        }

        public void AwardCombatCore()
        {
            if (_inventory.TryAdd(ResourceType.SlimeCore, 1, out _, out _))
            {
                SaveInventory();
                _feedback?.Show("SLIME DEFEATED: +1 CORE");
            }
        }

        public bool UnlockMoonlitGlade()
        {
            if (_save.moonlitGladeUnlocked) return false;
            _save.moonlitGladeUnlocked = true;
            SaveInventory();
            return true;
        }

        public bool TryRestoreBloomwell(out string status)
        {
            if (!global::Wayroot.Exploration.BloomwellRestorationRules.TryRestore(MoonlitGladeUnlocked, BloomwellRestored, _inventory))
            {
                status = global::Wayroot.Exploration.BloomwellRestorationRules.GetRequirementStatus(MoonlitGladeUnlocked, BloomwellRestored, _inventory);
                return false;
            }

            _save.bloomwellRestored = true;
            SaveInventory();
            status = global::Wayroot.Exploration.BloomwellRestorationRules.CompleteStatus;
            return true;
        }

        public void RegisterNodes(IEnumerable<GatheringNode> nodes)
        {
            foreach (GatheringNode node in nodes)
            {
                if (_nodes.Contains(node)) continue;
                _nodes.Add(node);
                long deadline = GetRenewalDeadline(node.Id);
                if (deadline > 0) node.RestoreDepleted(deadline);
                node.Completed += OnNodeCompleted;
            }

            ResolveRenewals(DateTime.UtcNow);
        }

        public void ResetPrototype()
        {
            _feedback?.Show("RESET: prototype progress cleared.");
            PrototypeGatheringSaveService.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Configure(PrototypeInputReader input, PrototypePlayerController player, InventoryState inventory, IEnumerable<GatheringNode> nodes)
        {
            _input = input;
            _player = player;
            _inventory = inventory;
            _nodes.AddRange(nodes);
            _save = PrototypeGatheringSaveService.Load();
            _inventory.TryAdd(ResourceType.WildPetal, _save.petals, out _, out _);
            _inventory.TryAdd(ResourceType.Timber, _save.timber, out _, out _);
            _inventory.TryAdd(ResourceType.Stone, _save.stone, out _, out _);
            _inventory.TryAdd(ResourceType.SlimeCore, _save.slimeCores, out _, out _);
            foreach (GatheringNode node in _nodes)
            {
                long deadline = GetRenewalDeadline(node.Id);
                if (deadline > 0) node.RestoreDepleted(deadline);
                node.Completed += OnNodeCompleted;
            }

            // Renewals are an always-on return loop: Wayroot restoration is a separate finite objective.
            ResolveRenewals(DateTime.UtcNow);
        }

        private void Update()
        {
            // Renewals are an always-on return loop: Wayroot restoration is a separate finite objective.
            ResolveRenewals(DateTime.UtcNow);
            CurrentTarget = _player.IsPaused ? null : FindNearest();
            if (CurrentTarget == null || !_input.InteractHeld || Time.time < _nextStepTime) return;
            if (!CurrentTarget.TryGather()) return;
            _nextStepTime = Time.time + StepInterval;
        }

        private void ResolveRenewals(DateTime utcNow)
        {
            bool changed = false;
            foreach (GatheringNode node in _nodes)
            {
                if (node.RefreshRenewal(utcNow))
                {
                    RemoveRenewal(node.Id);
                    _feedback?.Show($"RESOURCE RETURNED: {node.Resource.ToString().ToUpperInvariant()}");
                    _soundscape?.Play(SoundscapeCue.Renewal);
                    changed = true;
                }
            }

            if (changed) SaveInventory();
        }

        private GatheringNode? FindNearest()
        {
            GatheringNode? nearest = null;
            float best = Range * Range;
            foreach (GatheringNode node in _nodes)
            {
                if (!node.IsAvailable) continue;
                float distance = (node.transform.position - _player.transform.position).sqrMagnitude;
                if (distance <= best)
                {
                    best = distance;
                    nearest = node;
                }
            }

            return nearest;
        }

        private void OnNodeCompleted(GatheringNode node)
        {
            _inventory.TryAdd(node.Resource, 1, out _, out _);
            long deadline = RenewalRules.CreateDeadlineUtcTicks(DateTime.UtcNow);
            node.StartRenewal(deadline);
            SetRenewalDeadline(node.Id, deadline);
            _feedback?.Show($"GATHERED: +1 {node.Resource.ToString().ToUpperInvariant()} — RETURNS IN 0:20");
            _soundscape?.Play(SoundscapeCue.Gather);
            _player.GetComponent<ProceduralStylizedAnimator>()?.Emphasize();

            SaveInventory();
        }

        private string GetWayrootRequirementStatus()
        {
            if (WeaponLevel < WeaponUpgradeRules.MaximumLevel) return "WAYROOT needs IRON EDGE weapon upgrade.";
            if (!ShelterBuilt) return "WAYROOT needs a built SHELTER.";
            return $"WAYROOT needs {WayrootRestorationRules.PetalCost} PETAL + {WayrootRestorationRules.TimberCost} TIMBER + {WayrootRestorationRules.StoneCost} STONE + {WayrootRestorationRules.CoreCost} CORE.";
        }

        private long GetRenewalDeadline(string nodeId)
        {
            foreach (RenewalNodeSave renewal in _save.renewalNodes)
            {
                if (renewal.nodeId == nodeId) return renewal.renewalDeadlineUtcTicks;
            }

            return 0;
        }

        private void SetRenewalDeadline(string nodeId, long deadline)
        {
            foreach (RenewalNodeSave renewal in _save.renewalNodes)
            {
                if (renewal.nodeId == nodeId)
                {
                    renewal.renewalDeadlineUtcTicks = deadline;
                    return;
                }
            }

            _save.renewalNodes.Add(new RenewalNodeSave { nodeId = nodeId, renewalDeadlineUtcTicks = deadline });
        }

        private void RemoveRenewal(string nodeId)
        {
            _save.renewalNodes.RemoveAll(renewal => renewal.nodeId == nodeId);
        }

        private string GetRenewalStatus(DateTime utcNow)
        {
            long nextDeadline = 0;
            int renewing = 0;
            foreach (RenewalNodeSave renewal in _save.renewalNodes)
            {
                if (renewal.renewalDeadlineUtcTicks <= 0) continue;
                renewing++;
                if (nextDeadline == 0 || renewal.renewalDeadlineUtcTicks < nextDeadline) nextDeadline = renewal.renewalDeadlineUtcTicks;
            }

            if (renewing == 0) return "RENEWAL: all nodes available";

            return $"RENEWAL: {renewing} returning in {RenewalRules.FormatRemaining(nextDeadline, utcNow)}";
        }

        private void SaveInventory()
        {
            _save.petals = _inventory.GetCount(ResourceType.WildPetal);
            _save.timber = _inventory.GetCount(ResourceType.Timber);
            _save.stone = _inventory.GetCount(ResourceType.Stone);
            _save.slimeCores = _inventory.GetCount(ResourceType.SlimeCore);
            PrototypeGatheringSaveService.Save(_save);
        }

        private void OnDestroy()
        {
            foreach (GatheringNode node in _nodes)
            {
                if (node != null) node.Completed -= OnNodeCompleted;
            }
        }
    }
}

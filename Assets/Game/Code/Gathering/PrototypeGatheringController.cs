using System.Collections.Generic;
using Wayroot.Character;
using Wayroot.Building;
using Wayroot.Combat;
using Wayroot.Input;
using Wayroot.Inventory;
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
        public GatheringNode? CurrentTarget { get; private set; }
        public int WeaponLevel => _save.weaponLevel;
        public bool ShelterBuilt => _save.shelterBuilt;
        public int AttackDamage => WeaponUpgradeRules.GetAttackDamage(WeaponLevel);
        public int GetCount(ResourceType resource) => _inventory.GetCount(resource);
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
        public void AwardCombatCore()
        {
            if (_inventory.TryAdd(ResourceType.SlimeCore, 1, out _, out _)) SaveInventory();
        }
        private void SaveInventory()
        {
            _save.petals = _inventory.GetCount(ResourceType.WildPetal);
            _save.timber = _inventory.GetCount(ResourceType.Timber);
            _save.stone = _inventory.GetCount(ResourceType.Stone);
            _save.slimeCores = _inventory.GetCount(ResourceType.SlimeCore);
            PrototypeGatheringSaveService.Save(_save);
        }
        public void ResetPrototype()
        {
            PrototypeGatheringSaveService.Reset();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        public void Configure(PrototypeInputReader input, PrototypePlayerController player, InventoryState inventory, IEnumerable<GatheringNode> nodes)
        {
            _input = input; _player = player; _inventory = inventory; _nodes.AddRange(nodes); _save = PrototypeGatheringSaveService.Load();
            _inventory.TryAdd(ResourceType.WildPetal, _save.petals, out _, out _); _inventory.TryAdd(ResourceType.Timber, _save.timber, out _, out _); _inventory.TryAdd(ResourceType.Stone, _save.stone, out _, out _); _inventory.TryAdd(ResourceType.SlimeCore, _save.slimeCores, out _, out _);
            foreach (GatheringNode node in _nodes) { if (_save.depletedNodeIds.Contains(node.Id)) node.RestoreDepleted(); node.Completed += OnNodeCompleted; }
        }
        private void Update()
        {
            CurrentTarget = _player.IsPaused ? null : FindNearest();
            if (CurrentTarget == null || !_input.InteractHeld || Time.time < _nextStepTime) return;
            if (!CurrentTarget.TryGather()) return;
            _nextStepTime = Time.time + StepInterval;
        }
        private GatheringNode? FindNearest()
        { GatheringNode? nearest = null; float best = Range * Range; foreach (GatheringNode node in _nodes) { if (!node.IsAvailable) continue; float d = (node.transform.position - _player.transform.position).sqrMagnitude; if (d <= best) { best = d; nearest = node; } } return nearest; }
        private void OnNodeCompleted(GatheringNode node)
        {
            _inventory.TryAdd(node.Resource, 1, out _, out _); _save.depletedNodeIds.Add(node.Id); SaveInventory();
        }
        private void OnDestroy() { foreach (GatheringNode node in _nodes) if (node != null) node.Completed -= OnNodeCompleted; }
    }
}

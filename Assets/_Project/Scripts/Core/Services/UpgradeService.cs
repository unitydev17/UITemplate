using JetBrains.Annotations;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Core;
using UITemplate.Core.Entities;
using UITemplate.Core.Interfaces;
using UITemplate.Events;
using UniRx;

namespace UITemplate.Application.Services
{
    [UsedImplicitly]
    public class UpgradeService : IUpgradeService
    {
        private readonly UpgradeCfg _cfg;
        private readonly PlayerData _playerData;

        public UpgradeService(PlayerData playerData, UpgradeCfg cfg)
        {
            _playerData = playerData;
            _cfg = cfg;
        }

        public void UpdateBuildingValues(ref Building building)
        {
            var maxUpdate = IsLastUpdateReached(building);

            building.nextUpgradeCost = maxUpdate ? 0 : _cfg.GetCost(building.level);
            building.upgradeCompletion = GetUpgradeCompletion(building);
            building.currentIncome = _cfg.GetIncome(building.level);
            building.incomeMultiplier = _cfg.GetIncomeMultiplier(building.level);
            building.nextDeltaIncome = maxUpdate ? 0 : _cfg.GetIncome(building.level + 1) - building.currentIncome;
        }

        private float GetUpgradeCompletion(Building building)
        {
            return (building.level - 1) / ((float) _cfg.upgradesCount - 1);
        }

        public bool TryUpgrade(ref Building building)
        {
            if (IsLastUpdateReached(building))
            {
                UpdateBuildingValues(ref building);
                return false;
            }

            var cost = _cfg.GetCost(building.level);
            var canUpgrade = _playerData.money >= cost;
            if (!canUpgrade) return false;

            _playerData.money -= cost;
            NotifyUI();
            

            building.level++;
            UpdateBuildingValues(ref building);

            return true;
        }
        
        private void NotifyUI()
        {
            MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
        }

        private bool IsLastUpdateReached(Building building)
        {
            return building.level > _cfg.upgradesCount - 1;
        }
    }
}
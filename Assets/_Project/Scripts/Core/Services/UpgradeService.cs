using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.DomainEntities.Mappers;
using UITemplate.Core.Interfaces;
using UITemplate.Common.Events;
using UniRx;
using UnityEngine;

namespace UITemplate.Application.Services
{
    [UsedImplicitly]
    public class UpgradeService : IUpgradeService
    {
        private readonly UpgradeCfg _cfg;
        private readonly PlayerData _playerData;
        private readonly GameData _gameData;

        public UpgradeService(PlayerData playerData, GameData gameData, UpgradeCfg cfg)
        {
            _playerData = playerData;
            _gameData = gameData;
            _cfg = cfg;
        }

        public void UpdateBuildingsInfo()
        {
            for (var i = 0; i < _gameData.buildings.Count; i++)
            {
                var value = _gameData.buildings[i];
                UpdateBuildingRelativeValues(ref value);
                _gameData.buildings[i] = value;
            }
        }

        private void UpdateBuildingRelativeValues(ref Building building)
        {
            var maxUpdate = IsLastUpdateReached(building);

            building.nextUpgradeCost = maxUpdate ? 0 : _cfg.GetCost(building.upgradeLevel);
            building.upgradeProgress = GetUpgradeCompletion(building);
            building.currentIncome = _cfg.GetIncome(building.upgradeLevel);
            building.incomeMultiplier = _cfg.GetIncomeMultiplier(building.upgradeLevel);
            building.nextDeltaIncome = maxUpdate ? 0 : _cfg.GetIncome(building.upgradeLevel + 1) - building.currentIncome;
        }

        private float GetUpgradeCompletion(Building building)
        {
            return (Mathf.Max(building.upgradeLevel - 1, 0)) / ((float) _cfg.upgradesCount - 1);
        }

        public bool TryUpgrade(ref Building building)
        {
            if (IsLastUpdateReached(building))
            {
                UpdateBuildingRelativeValues(ref building);
                return false;
            }

            var cost = _cfg.GetCost(building.upgradeLevel);
            var canUpgrade = _playerData.money >= cost;
            if (!canUpgrade) return false;

            _playerData.money -= cost;
            NotifyUI();


            building.upgradeLevel++;
            UpdateBuildingRelativeValues(ref building);

            return true;
        }

        private void NotifyUI()
        {
            MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
        }

        private bool IsLastUpdateReached(Building building)
        {
            return building.upgradeLevel > _cfg.upgradesCount - 1;
        }
    }
}
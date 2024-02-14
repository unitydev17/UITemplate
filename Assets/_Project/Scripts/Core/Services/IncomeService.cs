using JetBrains.Annotations;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.DomainEntities.Mappers;
using UITemplate.Core.Interfaces;
using UITemplate.Events;
using UniRx;
using UnityEngine;

namespace UITemplate.Application.Services
{
    [UsedImplicitly]
    public class IncomeService : IIncomeService
    {
        private readonly PlayerData _playerData;
        private readonly UpgradeCfg _cfg;
        private readonly GameData _gameData;

        public IncomeService(PlayerData playerData, GameData gameData, UpgradeCfg cfg)
        {
            _playerData = playerData;
            _gameData = gameData;
            _cfg = cfg;
        }

        public void Process()
        {
            foreach (var building in _gameData.buildings) ProcessItem(building);
        }

        private void ProcessItem(Building building)
        {
            if (IsBuildingClose(building)) return;

            UpdateIncome(building);

            if (building.incomeCompletion < 1) return;
            building.incomeCompletion = 0;

            _playerData.money += building.currentIncome;

            NotifyBoard(building);
            NotifyUI();
        }

        private void NotifyUI()
        {
            MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
        }

        private static void NotifyBoard(Building building)
        {
            MessageBroker.Default.Publish(new IncomeEvent(building.ToDto()));
        }

        private void UpdateIncome(Building building)
        {
            var completeChunk = building.incomeSpeed * building.incomeMultiplier * Time.deltaTime;
            completeChunk *= _cfg.globalSpeedMultiplier;
            completeChunk *= _playerData.speedUp ? 2 : 1;

            building.incomeCompletion += completeChunk;
        }

        private static bool IsBuildingClose(Building building)
        {
            return building.level < 1;
        }
    }
}
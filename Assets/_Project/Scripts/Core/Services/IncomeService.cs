using System;
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
        private readonly GameData _gameData;
        private readonly UpgradeCfg _cfg;

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

        public (int, double) AccruePassiveIncome()
        {
            var now = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            var passiveTime = now - _playerData.gameExitTime;

            double speedUpTime = 0;
            double normalTime = passiveTime;


            if (_playerData.speedUp)
            {
                var rightBorder = Math.Min(_playerData.speedUpStartTime + _playerData.speedUpDuration, now);
                speedUpTime =  rightBorder - _playerData.gameExitTime;
                normalTime = Math.Abs(passiveTime - speedUpTime);
            }

            double overallIncome = 0;

            foreach (var building in _gameData.buildings)
            {
                var income = CountIncome(building) * normalTime;
                income += CountIncome(building, _cfg.speedUpMultiplier) * speedUpTime;
                overallIncome += income;
            }

            return ((int) overallIncome, passiveTime);
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
            var speedUpMultiplier = _playerData.speedUp ? _cfg.speedUpMultiplier : 1;
            var income = CountIncome(building, speedUpMultiplier) * Time.deltaTime;
            building.incomeCompletion += income;
        }

        private static float CountIncome(Building building, int speedUpMultiplier = 1)
        {
            return building.incomeSpeed * building.incomeMultiplier * speedUpMultiplier;
        }

        private static bool IsBuildingClose(Building building)
        {
            return building.level < 1;
        }
    }
}
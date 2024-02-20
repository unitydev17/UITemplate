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
    public class IncomeService : IIncomeService
    {
        private readonly PlayerData _playerData;
        private readonly GameData _gameData;
        private readonly UpgradeCfg _cfg;
        private readonly ITimerService _timerService;

        public IncomeService(PlayerData playerData, GameData gameData, UpgradeCfg cfg, ITimerService timerService)
        {
            _playerData = playerData;
            _gameData = gameData;
            _cfg = cfg;
            _timerService = timerService;
        }

        public void Process()
        {
            foreach (var building in _gameData.buildings) ProcessItem(building);
        }

        public void AccruePassiveIncome()
        {
            var passiveTime = _timerService.CountTimePassed(out var speedUpTime, out var normalTime);

            var overallIncome = CountOverallIncome(normalTime, speedUpTime);

            _playerData.passiveIncome = (int) overallIncome;
            _playerData.passiveTime = passiveTime;
        }


        private double CountOverallIncome(double normalTime, double speedUpTime)
        {
            double overallIncome = 0;
            foreach (var building in _gameData.buildings)
            {
                var incomeMultiplier = CountIncome(building) * normalTime;
                incomeMultiplier += CountIncome(building, _cfg.speedUpMultiplier) * speedUpTime;
                overallIncome += incomeMultiplier * building.currentIncome;
            }

            return overallIncome;
        }


        private void ProcessItem(Building building)
        {
            if (IsBuildingClose(building)) return;

            UpdateIncomeProgress(building);

            building.incomeReceived = building.incomeProgress >= 1;

            if (!building.incomeReceived) return;
            building.incomeProgress = 0;

            _playerData.money += building.currentIncome;
            CheckLevelCompleted();


            NotifyUI();
        }

        private void CheckLevelCompleted()
        {
            if (_playerData.levelCompleted) return;
            if (_playerData.money < _cfg.coinsToCompleteLevel) return;

            _playerData.levelCompleted = true;
            _playerData.countingEnabled = false;
            _playerData.levelIndex++;


            _timerService.Pause();

            MessageBroker.Default.Publish(new LevelCompletedEvent());
        }


        private void NotifyUI()
        {
            MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
        }

        private void UpdateIncomeProgress(Building building)
        {
            var speedUpMultiplier = _playerData.timer.active ? _cfg.speedUpMultiplier : 1;
            var income = CountIncome(building, speedUpMultiplier);
            building.incomeProgress += income * Time.fixedDeltaTime;
        }

        private static float CountIncome(Building building, int speedUpMultiplier = 1)
        {
            return building.incomePerSecond * building.incomeMultiplier * speedUpMultiplier;
        }

        private static bool IsBuildingClose(Building building)
        {
            return building.upgradeLevel < 1;
        }

        public void ClaimPassiveIncome(float multiplier)
        {
            _playerData.money += _playerData.passiveIncome * multiplier;
        }
    }
}
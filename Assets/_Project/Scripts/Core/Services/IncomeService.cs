using System;
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

        public void AccruePassiveIncome()
        {
            var now = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            var passiveTime = now - _playerData.gameExitTime;

            double speedUpTime = 0;
            var normalTime = passiveTime;


            if (_playerData.speedUp)
            {
                var speedUpTargetStopTime = Math.Min(_playerData.speedUpStartTime + _playerData.speedUpDuration, now);
                CheckStopSpeedUp(now, speedUpTargetStopTime);

                speedUpTime = speedUpTargetStopTime - _playerData.gameExitTime;
                normalTime = Math.Abs(passiveTime - speedUpTime);
            }

            var overallIncome = CountOverallIncome(normalTime, speedUpTime);

            _playerData.passiveIncome = (int) overallIncome;
            _playerData.passiveTime = passiveTime;
        }

        private void CheckStopSpeedUp(double now, double rightBorder)
        {
            if (now - rightBorder >= 0) _playerData.speedUp = false;
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


            NotifyUI();
        }

        private void NotifyUI()
        {
            MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
        }

        private void UpdateIncomeProgress(Building building)
        {
            var speedUpMultiplier = _playerData.speedUp ? _cfg.speedUpMultiplier : 1;
            var income = CountIncome(building, speedUpMultiplier);
            building.incomeProgress += income * Time.fixedDeltaTime;
        }

        private static float CountIncome(Building building, int speedUpMultiplier = 1)
        {
            return building.incomePerSecond * building.incomeMultiplier * speedUpMultiplier;
        }

        private static bool IsBuildingClose(Building building)
        {
            return building.level < 1;
        }
    }
}
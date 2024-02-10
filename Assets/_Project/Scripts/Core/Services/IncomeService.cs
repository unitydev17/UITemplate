using JetBrains.Annotations;
using UITemplate.Core;
using UITemplate.Core.Entities;
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

        public IncomeService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Process(Building building)
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
            var dto = BuildingDtoMapper.GetDto(building);
            MessageBroker.Default.Publish(new IncomeEvent(dto));
        }

        private static void UpdateIncome(Building building)
        {
            building.incomeCompletion += building.incomeSpeed * building.incomeMultiplier * Time.deltaTime;
        }

        private static bool IsBuildingClose(Building building)
        {
            return building.level < 1;
        }
    }
}
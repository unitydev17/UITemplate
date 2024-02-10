using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Common.Dto;
using UITemplate.Core.Entities;
using UITemplate.Core.Interfaces;
using UITemplate.Events;
using UITemplate.Utils;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Core.Controller
{
    [UsedImplicitly]
    public class GameManager : Registrable, IInitializable
    {
        private readonly PlayerData _playerData;
        private readonly IWorldService _worldService;
        private readonly IUpgradeService _upgradeService;
        private readonly IIncomeService _incomeService;

        private List<Building> _buildings = new List<Building>();


        public GameManager(IWorldService worldService, PlayerData playerData, IUpgradeService upgradeService, IIncomeService incomeService)
        {
            _worldService = worldService;
            _playerData = playerData;
            _upgradeService = upgradeService;
            _incomeService = incomeService;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<UpgradeRequestEvent>(), HandleUpgradeEvent);
            Register(Observable.EveryUpdate(), UpdateBuildingTimer);
        }

        public void Run()
        {
            InitializeBuildings();
            InitializePlayerData();
        }

        private void UpdateBuildingTimer()
        {
            foreach (var building in _buildings)
            {
                _incomeService.Process(building);
            }
            
            var dtoList = GetDtoList();
            _worldService.UpdateBuildingViews(dtoList);
        }

        private void InitializePlayerData()
        {
            _playerData.money = 1000;
        }

        private void InitializeBuildings()
        {
            _buildings = _worldService.FetchBuildingsFromScene();

            for (var i = 0; i < _buildings.Count; i++)
            {
                var value = _buildings[i];
                _upgradeService.UpdateBuildingValues(ref value);
                _buildings[i] = value;
            }

            var dtoList = GetDtoList();
            _worldService.UpdateBuildingViews(dtoList);
        }

        private IEnumerable<BuildingDto> GetDtoList()
        {
            return _buildings.Select(BuildingDtoMapper.GetDto).ToList();
        }

        private void HandleUpgradeEvent(UpgradeRequestEvent data)
        {
            var building = GetBuilding(data.id);
            var upgradeSuccess = _upgradeService.TryUpgrade(ref building);
            

            var dto = BuildingDtoMapper.GetDto(building);
            MessageBroker.Default.Publish(new UpgradeResponseEvent(dto));
        }

        private Building GetBuilding(int id)
        {
            return _buildings.Single(b => b.id == id);
        }
    }
}
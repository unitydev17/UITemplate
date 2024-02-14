using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Common.Dto;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.DomainEntities.Mappers;
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
        private readonly GameData _gameData;
        private readonly PlayerData _playerData;

        private readonly ISceneService _sceneService;
        private readonly IUpgradeService _upgradeService;
        private readonly IIncomeService _incomeService;
        private readonly IPersistenceService _persistenceService;


        public GameManager(ISceneService sceneService, PlayerData playerData, IUpgradeService upgradeService, IIncomeService incomeService, IPersistenceService persistenceService, GameData gameData)
        {
            _gameData = gameData;
            _playerData = playerData;

            _sceneService = sceneService;
            _upgradeService = upgradeService;
            _incomeService = incomeService;
            _persistenceService = persistenceService;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<UpgradeRequestEvent>(), HandleUpgradeRequestEvent);
            Register(MessageBroker.Default.Receive<UISpeedUpRequestEvent>(), HandleSpeedUpRequestEvent);
            Register(Observable.EveryUpdate(), UpdateBuildingTimer);
        }

        public void Run()
        {
            InitializeBuildings();
            InitializePlayerData();
        }

        private void HandleSpeedUpRequestEvent(UISpeedUpRequestEvent data)
        {
            _playerData.speedUp = data.enable;
        }

        private void UpdateBuildingTimer()
        {
            _incomeService.Process();
            _sceneService.UpdateBuildingViews(buildingsDtoList);
        }

        private void InitializePlayerData()
        {
            _playerData.money = 15;

            MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
        }

        private void InitializeBuildings()
        {
            _gameData.buildings = _sceneService.FetchBuildingsFromScene();
            _upgradeService.UpdateBuildingsInfo();
            _sceneService.UpdateBuildingViews(buildingsDtoList);
        }

        private IEnumerable<BuildingDto> buildingsDtoList => _gameData.buildings.Select(BuildingDtoMapper.GetDto).ToList();

        private void HandleUpgradeRequestEvent(UpgradeRequestEvent data)
        {
            var building = GetBuilding(data.id);
            if (!_upgradeService.TryUpgrade(ref building)) return;

            MessageBroker.Default.Publish(new UpgradeResponseEvent(building.ToDto()));
        }

        private Building GetBuilding(int id)
        {
            return _gameData.buildings.Single(b => b.id == id);
        }

        public void SaveGameState()
        {
            _persistenceService.SaveGameState(_gameData.buildings);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Interfaces;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.DomainEntities.Mappers;
using UITemplate.Core.Interfaces;
using UITemplate.Common.Events;
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
        private readonly UpgradeCfg _cfg;

        private readonly ISceneService _sceneService;
        private readonly IUpgradeService _upgradeService;
        private readonly IIncomeService _incomeService;
        private readonly IPersistenceService _persistenceService;


        public GameManager(ISceneService sceneService,
            PlayerData playerData,
            IUpgradeService upgradeService,
            IIncomeService incomeService,
            GameData gameData,
            IPersistenceService persistenceService,
            UpgradeCfg cfg)
        {
            _gameData = gameData;
            _playerData = playerData;

            _sceneService = sceneService;
            _upgradeService = upgradeService;
            _incomeService = incomeService;
            _persistenceService = persistenceService;
            _cfg = cfg;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<UpgradeRequestEvent>(), HandleUpgradeRequestEvent);
            Register(MessageBroker.Default.Receive<UISpeedUpRequestEvent>(), HandleSpeedUpRequestEvent);
            Register(MessageBroker.Default.Receive<CloseStartingPopupEvent>(), HandleCloseStartingPopupEvent);
            Register(Observable.EveryFixedUpdate(), UpdateBuildingTimer);
        }

        public void Run()
        {
            InitializeBuildings();
            InitializePlayerData();
        }

        private void HandleCloseStartingPopupEvent(CloseStartingPopupEvent data)
        {
            if (data.claimPressed)
            {
                _playerData.money += _playerData.passiveIncome;
                MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
            }

            _playerData.passiveIncome = 0;
        }

        private void HandleSpeedUpRequestEvent(UISpeedUpRequestEvent data)
        {
            _playerData.speedUp = data.enable;
            if (_playerData.speedUp == false) return;

            _playerData.speedUpStartTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            _playerData.speedUpDuration = data.duration;
        }

        private void UpdateBuildingTimer()
        {
            _incomeService.Process();
            _sceneService.UpdateBuildingViews(buildingsDtoList);
        }

        private void InitializePlayerData()
        {
            var isFirstRun = _persistenceService.LoadPlayerData() == false;
            if (isFirstRun)
            {
                InitPlayerData();
                MessageBroker.Default.Publish(new WelcomeEvent());
                return;
            }

            _incomeService.AccruePassiveIncome();

            MessageBroker.Default.Publish(new PassiveIncomeNotifyEvent(_playerData.passiveIncome, _playerData.passiveTime));
            MessageBroker.Default.Publish(new UpdateOnInitEvent(_playerData.ToDto()));
        }

        private void InitPlayerData()
        {
            _playerData.money = _cfg.playerStartCoins;
            _playerData.speedUp = false;
        }

        private void InitializeBuildings()
        {
            _persistenceService.LoadSettingsData();

            _gameData.buildings = BuildingDtoMapper.ToEntityList(_sceneService.FetchBuildingsFromScene());
            _persistenceService.LoadSceneData();

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
    }
}
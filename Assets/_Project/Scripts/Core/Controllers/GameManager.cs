using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
        private readonly ITimerService _timerService;

        public GameManager(ISceneService sceneService,
            PlayerData playerData,
            IUpgradeService upgradeService,
            IIncomeService incomeService,
            GameData gameData,
            IPersistenceService persistenceService,
            UpgradeCfg cfg,
            ITimerService timerService)
        {
            _gameData = gameData;
            _playerData = playerData;

            _sceneService = sceneService;
            _upgradeService = upgradeService;
            _incomeService = incomeService;
            _persistenceService = persistenceService;
            _cfg = cfg;
            _timerService = timerService;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<UpgradeRequestEvent>(), UpgradeRequestEventHandler);

            Register(MessageBroker.Default.Receive<CloseStartingPopupEvent>(), CloseStartingPopupEventHandler);
            RegisterAsync(MessageBroker.Default.Receive<NextLevelRequestEvent>(), value => NextLevelRequestEventHandler());
            Register(Observable.EveryFixedUpdate(), UpdateBuildingProgress);
        }

        public async UniTask Run()
        {
            InitializePlayerData();
            await LoadLevel();
            InitializeLevel();
        }

        private void InitializePlayerData()
        {
            var isFirstRun = _persistenceService.LoadPlayerData() == false;
            if (isFirstRun)
            {
                ResetPlayerData();
                MessageBroker.Default.Publish(new WelcomeEvent());
                return;
            }

            _incomeService.AccruePassiveIncome();

            MessageBroker.Default.Publish(new PassiveIncomeNotifyEvent(_playerData.passiveIncome, _playerData.passiveTime));
            MessageBroker.Default.Publish(new UpdateOnInitEvent(_playerData.ToDto()));
        }

        private void ResetPlayerData()
        {
            _playerData.money = _cfg.playerStartCoins;
        }

        private async UniTask LoadLevel()
        {
            await _sceneService.LoadLevel(_playerData.levelIndex);
        }

        private void InitializeLevel()
        {
            _persistenceService.LoadSettingsData();

            _gameData.buildings = FetchBuildingsFromScene();
            _persistenceService.LoadSceneData();
            _upgradeService.UpdateBuildingsInfo();
            _sceneService.UpdateBuildingViews(buildingsDtoList);

            StartCountingProcess();
        }

        private List<Building> FetchBuildingsFromScene()
        {
            return BuildingDtoMapper.ToEntityList(_sceneService.FetchBuildingsFromScene());
        }

        private void StartCountingProcess()
        {
            _playerData.levelCompleted = false;
        }

        private void UpdateBuildingProgress()
        {
            if (_playerData.levelCompleted) return;
            _incomeService.Process();
            _sceneService.UpdateBuildingViews(buildingsDtoList);
        }

        private void UpgradeRequestEventHandler(UpgradeRequestEvent data)
        {
            var building = GetBuilding(data.id);
            if (!_upgradeService.TryUpgrade(ref building)) return;

            MessageBroker.Default.Publish(new UpgradeResponseEvent(building.ToDto()));
        }

        private IEnumerable<BuildingDto> buildingsDtoList => _gameData.buildings.Select(BuildingDtoMapper.GetDto).ToList();

        private Building GetBuilding(int id)
        {
            return _gameData.buildings.Single(b => b.id == id);
        }

        private void CloseStartingPopupEventHandler(CloseStartingPopupEvent data)
        {
            if (data.claimPressed)
            {
                _playerData.money += _playerData.passiveIncome;
                MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
            }

            _playerData.passiveIncome = 0;
        }

        private async UniTask NextLevelRequestEventHandler()
        {
            await LoadLevel();
            UpdateSceneData();

            ResetPlayerData();
            StartCountingProcess();

            MessageBroker.Default.Publish(new UpdateOnInitEvent(_playerData.ToDto()));
        }

        private void UpdateSceneData()
        {
            _gameData.buildings = BuildingDtoMapper.ToEntityList(_sceneService.FetchBuildingsFromScene());
            _upgradeService.UpdateBuildingsInfo();
            _sceneService.UpdateBuildingViews(buildingsDtoList);
        }
    }
}
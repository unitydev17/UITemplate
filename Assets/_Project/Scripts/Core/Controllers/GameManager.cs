using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private enum GameState
        {
            Welcome,
            Starting,
            Completed
        }

        private GameState _state;


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
            Register(Observable.EveryFixedUpdate(), UpdateBuildingProgress);
            Register(MessageBroker.Default.Receive<StartCountingEvent>(), StartCountingEventHandler);
            RegisterAsync(MessageBroker.Default.Receive<NextLevelRequestEvent>(), NextLevelStartEvent);
        }

        public async UniTask Run()
        {
            InitializeSettings();
            InitializePlayerData();
            await SelectStartPath();
        }

        private async Task SelectStartPath()
        {
            switch (_state)
            {
                case GameState.Welcome:
                    await WelcomeStart();
                    break;
                case GameState.Starting:
                    await StartOrContinueGame();
                    break;
                case GameState.Completed:
                    await StartNextLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task StartNextLevel()
        {
            await NextLevelStartEvent();
        }

        private async Task StartOrContinueGame()
        {
            await LoadLevel();
            InitializeLevel();

            _incomeService.AccruePassiveIncome();
            if (_playerData.passiveIncome > 0)
            {
                _timerService.PauseOnReturnToGame();
                MessageBroker.Default.Publish(new PassiveIncomeNotifyEvent(_playerData.ToDto()));
            }
            else
            {
                StartCountingEventHandler();
            }
        }

        private async Task WelcomeStart()
        {
            await LoadLevel();
            InitializeLevel();

            SetStartPlayerData();
            MessageBroker.Default.Publish(new WelcomeEvent(_playerData.ToDto()));
        }

        private void InitializeSettings()
        {
            _persistenceService.LoadSettingsData();
        }

        private void InitializePlayerData()
        {
            var isFirstRun = _persistenceService.LoadPlayerData() == false;
            if (isFirstRun)
            {
                _state = GameState.Welcome;
                return;
            }

            if (_playerData.levelCompleted)
            {
                _state = GameState.Completed;
                return;
            }

            _state = GameState.Starting;
        }

        private void SetStartPlayerData()
        {
            _playerData.money = _cfg.playerStartCoins;
            _playerData.timer.duration = _cfg.speedUpDuration;
        }

        private void ResetPlayerData()
        {
            _playerData.money = _cfg.playerStartCoins;
            _playerData.levelCompleted = false;
        }

        private async UniTask LoadLevel()
        {
            await _sceneService.LoadLevel(_playerData.levelIndex);
        }

        private void InitializeLevel()
        {
            _gameData.buildings = FetchBuildingsFromScene();
            _persistenceService.LoadSceneData();
            _upgradeService.UpdateBuildingsInfo();
            _sceneService.UpdateBuildingViews(buildingsDtoList, true);
        }

        private List<Building> FetchBuildingsFromScene()
        {
            return BuildingDtoMapper.ToEntityList(_sceneService.FetchBuildingsFromScene());
        }

        private void StartCountingProcess()
        {
            _playerData.countingEnabled = true;
        }

        private void UpdateBuildingProgress()
        {
            if (!_playerData.countingEnabled) return;
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
            var claimed = data.claimPressed || data.claimX2Pressed;
            if (claimed)
            {
                var multiplier = data.claimX2Pressed ? _cfg.claimMultiplier : 1;

                _incomeService.ClaimPassiveIncome(multiplier);
                MessageBroker.Default.Publish(new UpdatePlayerDataEvent(_playerData.ToDto()));
                return;
            }

            _incomeService.ClaimPassiveIncome(0);
        }

        private async UniTask NextLevelStartEvent()
        {
            await LoadLevel();
            UpdateSceneData();
            ResetPlayerData();

            StartCountingEventHandler();
        }

        private void StartCountingEventHandler()
        {
            _timerService.UnPause();
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
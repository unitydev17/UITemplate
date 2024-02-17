using System;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Events;
using UITemplate.UI.MVP.Presenter;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace UITemplate.UI.Windows.Hud
{
    [UsedImplicitly]
    public class HudPresenter : WindowPresenter<HudView, HudModel>, IInitializable, IStartable
    {
        private readonly UpgradeCfg _cfg;
        private readonly HudSpeedUpCommand _timeCommand;

        public HudPresenter(UpgradeCfg cfg, HudSpeedUpCommand timeCommand)
        {
            _cfg = cfg;
            _timeCommand = timeCommand;
        }

        public HudPresenter()
        {
        }

        public void Initialize()
        {
            RegisterStubList();
            Register(view.onSettingsBtnClick, OpenSettingsPopup);
            Register(MessageBroker.Default.Receive<UpdateOnInitEvent>(), UpdateOnInitEventHandler);
            Register(MessageBroker.Default.Receive<UpdatePlayerDataEvent>(), UpdatePlayerDataEventHandler);
            Register(MessageBroker.Default.Receive<LevelCompletedEvent>(), LevelCompletedEventHandler);
            Register(view.onSpeedBtnClick, ActivateSpeed);
        }

        public void Start()
        {
            ResetSpeedUpButton();
        }

        private void ResetSpeedUpButton()
        {
            view.UpdateSpeedUpTimer(_cfg.speedUpDuration, 1);
        }

        private void ActivateSpeed()
        {
            if (model.timerEnabled) return;

            _timeCommand.SetInitData(view, model);
            _timeCommand.Execute();
        }

        private void RegisterStubList()
        {
            foreach (var observable in view.onStubBtnClicks)
            {
                Register(observable, OpenStubPopup);
            }
        }

        private static void OpenStubPopup()
        {
            MessageBroker.Default.Publish(new StubOpenEvent());
        }

        private static void OpenSettingsPopup()
        {
            MessageBroker.Default.Publish(new SettingsOpenEvent());
        }

        private void UpdatePlayerDataEventHandler(UpdatePlayerDataEvent data)
        {
            UpdateCoins(data.dto);
        }

        private void UpdateCoins(PlayerDto data)
        {
            view.UpdateCoins(data.money);
        }

        private void UpdateOnInitEventHandler(UpdateOnInitEvent data)
        {
            var dto = data.dto;

            UpdateCoins(dto);
            SetupTimer(dto);
        }

        private void SetupTimer(PlayerDto dto)
        {
            if (dto.timer.speedUp == false) return;

            var timerPaused = dto.timer.timerPaused;
            var pauseTime = dto.timer.timerPauseTime;

            
            var elapsedTime = (timerPaused ? pauseTime : new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds) - dto.timer.speedUpStartTime;
            
            Debug.Log(timerPaused + "   " + elapsedTime + "   " + dto.timer.speedUpStartTime);
            
            if (CheckStopTimer(dto, elapsedTime)) return;

            _timeCommand.SetInitData(view, model, true, (float) elapsedTime);
            _timeCommand.Execute();
        }

        private static bool CheckStopTimer(PlayerDto dto, double elapsedTime)
        {
            if (elapsedTime < dto.timer.speedUpDuration) return false;
            
            MessageBroker.Default.Publish(new SpeedUpRequestEvent(false));
            return true;
        }

        private void LevelCompletedEventHandler()
        {
            _timeCommand?.ForceStopTimer();
        }
    }

    internal class StubOpenEvent
    {
    }

    internal class SettingsOpenEvent
    {
    }
}
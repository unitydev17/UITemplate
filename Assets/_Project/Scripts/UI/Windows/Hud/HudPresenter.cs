using System;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Events;
using UITemplate.UI.Command;
using UITemplate.UI.MVP.Presenter;
using UniRx;
using VContainer.Unity;

namespace UITemplate.UI.Windows.Hud
{
    [UsedImplicitly]
    public class HudPresenter : WindowPresenter<HudView, HudModel>, IInitializable
    {
        private readonly HudTimerCommand _timeCommand;

        public HudPresenter(UpgradeCfg cfg, HudTimerCommand timeCommand)
        {
            _timeCommand = timeCommand;
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
            UpdateCoins(data.dto.money);
        }

        private void UpdateCoins(float money)
        {
            view.UpdateCoins(money);
        }

        private void UpdateOnInitEventHandler(UpdateOnInitEvent data)
        {
            var dto = data.dto;

            UpdateCoins(dto.money);
            UpdateTimer(dto);

            RunTimer(dto);
        }

        private static bool CheckStopTimer(PlayerDto dto, double elapsedTime)
        {
            if (elapsedTime < dto.timer.duration) return false;

            MessageBroker.Default.Publish(new StopTimerEvent());
            return true;
        }

        private void LevelCompletedEventHandler()
        {
            _timeCommand?.ForceStopTimer();
        }

        private void RunTimer(PlayerDto dto)
        {
            if (!dto.timer.active) return;

            var wasTimerPaused = dto.timer.timerPaused;
            var pauseTime = dto.timer.timerPauseTime;
            var elapsedTime = (wasTimerPaused ? pauseTime : new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds) - dto.timer.startTime;

            if (CheckStopTimer(dto, elapsedTime)) return;

            _timeCommand.SetInitData(view, model, true, (float) elapsedTime, dto.timer.duration);
            _timeCommand.Execute();
        }

        private void UpdateTimer(PlayerDto dto)
        {
            var (timeRemain, progress) = TimerCommand.GetProgress((float) dto.elapsedTime, dto.timer.duration);
            view.UpdateTimer(timeRemain, progress);
        }

        public void UpdateView(PlayerDto dto)
        {
            UpdateCoins(dto.money);
            UpdateTimer(dto);
            view.SetSpeedButtonActive(dto.timer.active);
        }
    }

    internal class StubOpenEvent
    {
    }

    internal class SettingsOpenEvent
    {
    }
}
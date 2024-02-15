using System;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Events;
using UITemplate.UI.MVP.Presenter;
using UniRx;
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
            Register(MessageBroker.Default.Receive<UpdateOnInitEvent>(), UpdateOnInit);
            Register(MessageBroker.Default.Receive<UpdatePlayerDataEvent>(), UpdatePlayerData);
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

        private void UpdatePlayerData(UpdatePlayerDataEvent data)
        {
            UpdateCoins(data.dto);
        }

        private void UpdateCoins(PlayerDto data)
        {
            view.UpdateCoins(data.money);
        }

        private void UpdateOnInit(UpdateOnInitEvent data)
        {
            UpdateCoins(data.dto);
            if (data.dto.speedUp == false) return;

            var leftTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds - data.dto.speedUpStartTime;
            if (leftTime >= data.dto.speedUpDuration)
            {
                MessageBroker.Default.Publish(new UISpeedUpRequestEvent(false));
                return;
            }

            _timeCommand.SetInitData(view, model, true, (float) leftTime);
            _timeCommand.Execute();
        }
    }

    internal class StubOpenEvent
    {
    }

    internal class SettingsOpenEvent
    {
    }
}
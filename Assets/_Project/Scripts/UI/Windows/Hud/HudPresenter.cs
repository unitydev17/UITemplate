using JetBrains.Annotations;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Events;
using UITemplate.Presentation.MVP.Presenter;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Presentation.Windows.Hud
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
            Register(MessageBroker.Default.Receive<UpdatePlayerDataEvent>(), UpdateIncome);
            Register(view.onSpeedBtnClick, ActivateSpeed);
        }

        public void Start()
        {
            ResetSpeedUpButton();
        }

        private void ResetSpeedUpButton()
        {
            view.UpdateSpeedUpTimer((int) _cfg.speedUpDuration, 1);
        }

        private void ActivateSpeed()
        {
            if (model.timerEnabled) return;

            _timeCommand.SetViewModel(view, model);
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
            MessageBroker.Default.Publish(new HudStubOpenEvent());
        }

        private static void OpenSettingsPopup()
        {
            MessageBroker.Default.Publish(new HudSettingsOpenEvent());
        }

        private void UpdateIncome(UpdatePlayerDataEvent data)
        {
            view.UpdateCoins(data.dto.money);
        }
    }

    internal class HudStubOpenEvent
    {
    }

    internal class HudSettingsOpenEvent
    {
    }
}
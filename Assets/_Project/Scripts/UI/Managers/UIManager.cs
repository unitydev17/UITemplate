using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Events;
using UITemplate.UI.Factory;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Starting;
using UITemplate.UI.Windows.Hud;
using UITemplate.Utils;
using UniRx;
using VContainer.Unity;

namespace UITemplate.UI.Managers
{
    [UsedImplicitly]
    public class UIManager : Registrable, IInitializable
    {
        private readonly IWindowFactory _windowFactory;
        private HudPresenter _hudPresenter;

        public UIManager(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<CloseStartingPopupEvent>(), OnCloseStartingPopup);
            Register(MessageBroker.Default.Receive<ChefPackInfoOpenEvent>(), OpenPromoInfoPopup);
            Register(MessageBroker.Default.Receive<SettingsOpenEvent>(), OpenSettingsPopup);
            Register(MessageBroker.Default.Receive<StubOpenEvent>(), OpenStubPopup);
            Register(MessageBroker.Default.Receive<BoostRequestEvent>(), OpenStubPopup);
            RegisterAsync(MessageBroker.Default.Receive<PassiveIncomeNotifyEvent>(), OpenStartingPopup);
            RegisterAsync(MessageBroker.Default.Receive<WelcomeEvent>(), OpenWelcomePopup);
            Register(MessageBroker.Default.Receive<LevelCompletedEvent>(), OpenLevelCompletePopup);
        }


        private async void OpenLevelCompletePopup()
        {
            var presenter = await _windowFactory.GetSimpleMessagePopup();
            presenter.Setup(Constants.LevelComplete, Constants.Congratulations, () => MessageBroker.Default.Publish(new NextLevelRequestEvent()));
        }

        public async UniTask Run()
        {
            _hudPresenter = await _windowFactory.GetHudWindow();
        }

        private async UniTask OpenWelcomePopup(WelcomeEvent data)
        {
            _hudPresenter.UpdateView(data.dto);

            var presenter = await _windowFactory.GetWelcomePopup();
            presenter.onClosePopup = () => MessageBroker.Default.Publish(new StartCountingEvent());
        }

        private async UniTask OpenStartingPopup(PassiveIncomeNotifyEvent data)
        {
            _hudPresenter.UpdateView(data.dto);

            var presenter = await _windowFactory.GetStartingPopup();

            var dto = data.dto;
            presenter.Setup(dto.passiveIncome, dto.passiveTime);
        }

        private async void OnCloseStartingPopup(CloseStartingPopupEvent evt)
        {
            if (evt.claimPressed) await OpenPromoPopup();
            if (evt.claimX2Pressed) MessageBroker.Default.Publish(new StartCountingEvent());
        }

        private void OpenStubPopup()
        {
            _windowFactory.GetStubPopup();
        }

        private void OpenSettingsPopup()
        {
            _windowFactory.GetSettingsPopup();
        }


        private async UniTask OpenPromoPopup()
        {
            var presenter = await _windowFactory.GetPromoPopup();
            presenter.onClosePopup = () => MessageBroker.Default.Publish(new StartCountingEvent());
        }

        private void OpenPromoInfoPopup()
        {
            _windowFactory.GetPromoInfoPopup();
        }
    }
}
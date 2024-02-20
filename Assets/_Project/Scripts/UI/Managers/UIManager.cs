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
            Register(MessageBroker.Default.Receive<LevelCompletedEvent>(), OpenLevelCompletePopup);

            RegisterAsync(MessageBroker.Default.Receive<ChefPackInfoOpenEvent>(), OpenPromoInfoPopup);
            RegisterAsync(MessageBroker.Default.Receive<SettingsOpenEvent>(), OpenSettingsPopup);
            RegisterAsync(MessageBroker.Default.Receive<StubOpenEvent>(), OpenStubPopup);
            RegisterAsync(MessageBroker.Default.Receive<BoostRequestEvent>(), OpenStubPopup);
            RegisterAsync(MessageBroker.Default.Receive<PassiveIncomeNotifyEvent>(), OpenStartingPopup);
            RegisterAsync(MessageBroker.Default.Receive<WelcomeEvent>(), OpenWelcomePopup);
        }


        private async void OpenLevelCompletePopup()
        {
            var presenter = await _windowFactory.GetSimpleMessagePopup();

            presenter.SetHeader(Constants.LevelComplete)
                .SetMessage(Constants.Congratulations)
                .SetCloseAction(() => MessageBroker.Default.Publish(new NextLevelRequestEvent()))
                .ShowView();
        }

        public async UniTask Run()
        {
            _hudPresenter = await _windowFactory.GetHudWindow();
            _hudPresenter.ShowView();
        }

        private async UniTask OpenWelcomePopup(WelcomeEvent data)
        {
            _hudPresenter.UpdateView(data.dto);

            var presenter = await _windowFactory.GetWelcomePopup();
            presenter.onClosePopup = () => MessageBroker.Default.Publish(new StartCountingEvent());
            presenter.ShowView();
        }

        private async UniTask OpenStartingPopup(PassiveIncomeNotifyEvent data)
        {
            _hudPresenter.UpdateView(data.dto);

            var presenter = await _windowFactory.GetStartingPopup();
            presenter.Setup(data.dto.passiveIncome, data.dto.passiveTime)
                .ShowView();
        }

        private async void OnCloseStartingPopup(CloseStartingPopupEvent evt)
        {
            if (evt.claimPressed) await OpenPromoPopup();
            if (evt.claimX2Pressed) MessageBroker.Default.Publish(new StartCountingEvent());
        }

        private async UniTask OpenStubPopup()
        {
            var presenter = await _windowFactory.GetStubPopup();
            presenter.ShowView();
        }

        private async UniTask OpenSettingsPopup()
        {
            var presenter = await _windowFactory.GetSettingsPopup();
            presenter.ShowView();
        }


        private async UniTask OpenPromoPopup()
        {
            var presenter = await _windowFactory.GetPromoPopup();
            presenter.onClosePopup = () => MessageBroker.Default.Publish(new StartCountingEvent());
            presenter.ShowView();
        }

        private async UniTask OpenPromoInfoPopup()
        {
            var presenter = await _windowFactory.GetPromoInfoPopup();
            presenter.ShowView();
        }
    }
}
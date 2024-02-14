using System;
using JetBrains.Annotations;
using UITemplate.Events;
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
            Register(MessageBroker.Default.Receive<PassiveIncomeNotifyEvent>(), OpenStartingPopup);
            Register(MessageBroker.Default.Receive<WelcomeEvent>(), OpenWelcomePopup);
        }

        public void Run(Action callback)
        {
            _windowFactory.GetHudWindow(presenter => { callback?.Invoke(); });
        }

        private void OpenWelcomePopup()
        {
            _windowFactory.GetWelcomePopup();
        }

        private void OpenStartingPopup(PassiveIncomeNotifyEvent data)
        {
            _windowFactory.GetStartingPopup(presenter => { presenter.Setup(data.sum, data.time); });
        }

        private void OpenStubPopup()
        {
            _windowFactory.GetStubPopup();
        }

        private void OpenSettingsPopup()
        {
            _windowFactory.GetSettingsPopup();
        }


        private void OnCloseStartingPopup(CloseStartingPopupEvent evt)
        {
            if (evt.claimPressed) OpenPromoPopup();
        }

        private void OpenPromoPopup()
        {
            _windowFactory.GetPromoPopup();
        }

        private void OpenPromoInfoPopup()
        {
            _windowFactory.GetPromoInfoPopup();
        }
    }
}
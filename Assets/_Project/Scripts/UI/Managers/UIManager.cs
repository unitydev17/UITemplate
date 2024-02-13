using JetBrains.Annotations;
using UITemplate.UI.Factory;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Starting;
using UITemplate.UI.Windows.Hud;
using UniRx;
using VContainer.Unity;

namespace UITemplate.UI.Managers
{
    [UsedImplicitly]
    public class UIManager : IInitializable
    {
        private readonly IWindowFactory _windowFactory;

        public UIManager(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        public void Initialize()
        {
            MessageBroker.Default.Receive<CloseStartingPopupEvent>().Subscribe(OnCloseStartingPopup);
            MessageBroker.Default.Receive<ChefPackInfoOpenEvent>().Subscribe(_ => OpenPromoInfoPopup());
            MessageBroker.Default.Receive<HudSettingsOpenEvent>().Subscribe(_ => OpenSettingsPopup());
            MessageBroker.Default.Receive<HudStubOpenEvent>().Subscribe(_ => OpenStubPopup());
        }

        public void Run()
        {
            _windowFactory.GetHudWindow(presenter => { OpenStartingPopup(); });
        }

        private void OpenStubPopup()
        {
            _windowFactory.GetStubPopup();
        }

        private void OpenSettingsPopup()
        {
            _windowFactory.GetSettingsPopup();
        }

        private void OpenStartingPopup()
        {
            _windowFactory.GetStartingPopup(presenter => presenter.Setup());
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
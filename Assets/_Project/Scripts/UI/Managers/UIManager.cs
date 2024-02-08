using JetBrains.Annotations;
using UITemplate.Presentation.MVP.Factory;
using UITemplate.Presentation.Windows.Popups.Promo;
using UITemplate.Presentation.Windows.Popups.Starting;
using UITemplate.Presentation.Windows.Hud;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Presentation
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
        }

        public void Run()
        {
            _windowFactory.GetHud();
            OpenStartingPopup();
        }

        private void OpenSettingsPopup()
        {
            _windowFactory.GetSettingsPopup();
        }

        private void OpenStartingPopup()
        {
            var presenter = _windowFactory.GetStartingPopup();
            presenter.Setup();
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
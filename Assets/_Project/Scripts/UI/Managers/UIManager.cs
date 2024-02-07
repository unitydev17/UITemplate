using JetBrains.Annotations;
using UITemplate.Presentation.Model;
using UITemplate.Presentation.MVP.Factory;
using UITemplate.Presentation.Windows.Popups.Promo;
using UITemplate.Presentation.Windows.Popups.Settings;
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
            MessageBroker.Default.Receive<ChefPackInfoOpenEvent>().Subscribe(_ => OpenChefPackInfoPopup());
            MessageBroker.Default.Receive<HudSettingsOpenEvent>().Subscribe(_ => OpenSettingsPopup());
        }

        public void Run()
        {
            _windowFactory.Create<HudPresenter, HudView, HudModel>().OpenView();
            OpenStartingPopup();
        }

        private void OpenSettingsPopup()
        {
            _windowFactory.Create<SettingsPopupPresenter, SettingsPopupView, SettingsPopupModel>().OpenView();
        }

        private void OpenStartingPopup()
        {
            var presenter = _windowFactory.Create<StartingPopupPresenter, StartingPopupView, StartingPopupModel>();
            presenter.Setup();
            presenter.OpenView();
        }

        private void OnCloseStartingPopup(CloseStartingPopupEvent evt)
        {
            if (evt.claimPressed) OpenChefPackPopup();
        }

        private void OpenChefPackPopup()
        {
            _windowFactory.Create<PromoPopupPresenter, PromoPopupView, BaseModel>().OpenView();
        }

        private void OpenChefPackInfoPopup()
        {
            _windowFactory.Create<PromoInfoPopupPresenter, PromoInfoPopupView, BaseModel>().OpenView();
        }
    }
}
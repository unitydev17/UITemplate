using JetBrains.Annotations;
using UITemplate.Presentation.Factories;
using UITemplate.Presentation.Model;
using UITemplate.Presentation.Windows.Popups.Promo;
using UITemplate.Presentation.Windows.Popups.Settings;
using UITemplate.Presentation.Windows.Popups.Starting;
using UITemplate.Presentation.Windows.Hud;
using UniRx;
using VContainer;
using VContainer.Unity;

namespace UITemplate.Presentation
{
    [UsedImplicitly]
    public class UIManager : IInitializable
    {
        private readonly IObjectResolver _container;

        public UIManager(IObjectResolver container)
        {
            _container = container;
        }

        public void Initialize()
        {
            MessageBroker.Default.Receive<CloseStartingPopupEvent>().Subscribe(OnCloseStartingPopup);
            MessageBroker.Default.Receive<ChefPackInfoOpenEvent>().Subscribe(_ => OpenChefPackInfoPopup());
            MessageBroker.Default.Receive<HudSettingsOpenEvent>().Subscribe(_ => OpenSettingsPopup());
        }

        public void Run()
        {
            WindowFactory.Create<HudPresenter, HudView, HudModel>(_container).OpenView();
            OpenStartingPopup();
        }

        private void OpenSettingsPopup()
        {
            WindowFactory.Create<SettingsPopupPresenter, SettingsPopupView, SettingsPopupModel>(_container).OpenView();
        }

        private void OpenStartingPopup()
        {
            var presenter = WindowFactory.Create<StartingPopupPresenter, StartingPopupView, StartingPopupModel>(_container);
            presenter.Setup();
            presenter.OpenView();
        }

        private void OnCloseStartingPopup(CloseStartingPopupEvent evt)
        {
            if (evt.claimPressed) OpenChefPackPopup();
        }

        private void OpenChefPackPopup()
        {
            WindowFactory.Create<PromoPopupPresenter, PromoPopupView, BaseModel>(_container).OpenView();
        }

        private void OpenChefPackInfoPopup()
        {
            WindowFactory.Create<PromoInfoPopupPresenter, PromoInfoPopupView, BaseModel>(_container).OpenView();
        }
    }
}
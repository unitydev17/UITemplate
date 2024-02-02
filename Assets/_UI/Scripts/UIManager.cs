using JetBrains.Annotations;
using UITemplate.Presenter;
using UITemplate.Presenter.Windows;
using UniRx;
using VContainer.Unity;

namespace UITemplate
{
    [UsedImplicitly]
    public class UIManager : IInitializable
    {
        private readonly StartingPopupPresenter _startingPopupPresenter;
        private readonly ChefPackPopupPresenter _chefPackPopupPresenter;
        private readonly ChefPackInfoPopupPresenter _chefPackInfoPopupPresenter;
        private readonly SettingsPopupPresenter _settingsPopupPresenter;
        private readonly HudPresenter _hudPresenter;

        public UIManager(StartingPopupPresenter startingPopupPresenter,
            ChefPackPopupPresenter chefPackPopupPresenter,
            ChefPackInfoPopupPresenter chefPackInfoPopupPresenter,
            SettingsPopupPresenter settingsPopupPresenter,
            HudPresenter hudPresenter)
        {
            _startingPopupPresenter = startingPopupPresenter;
            _chefPackPopupPresenter = chefPackPopupPresenter;
            _chefPackInfoPopupPresenter = chefPackInfoPopupPresenter;
            _settingsPopupPresenter = settingsPopupPresenter;
            _hudPresenter = hudPresenter;
        }

        public void Initialize()
        {
            MessageBroker.Default.Receive<CloseStartingPopupEvent>().Subscribe(OnCloseStartingPopup);
            MessageBroker.Default.Receive<ChefPackInfoOpenEvent>().Subscribe(_ => OpenChefPackInfoPopup());
            MessageBroker.Default.Receive<HudSettingsOpenEvent>().Subscribe(_ => OpenSettingsPopup());
        }

        public void Run()
        {
            _hudPresenter.OpenView();
            OpenStartingPopup();
        }

        private void OpenSettingsPopup()
        {
            _settingsPopupPresenter.OpenView();
        }

        private void OpenStartingPopup()
        {
            _startingPopupPresenter.Setup();
            _startingPopupPresenter.OpenView();
        }

        private void OnCloseStartingPopup(CloseStartingPopupEvent evt)
        {
            if (evt.claimPressed) OpenChefPackPopup();
        }

        private void OpenChefPackPopup()
        {
            _chefPackPopupPresenter.OpenView();
        }

        private void OpenChefPackInfoPopup()
        {
            _chefPackInfoPopupPresenter.OpenView();
        }
    }
}
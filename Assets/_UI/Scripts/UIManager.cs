using JetBrains.Annotations;
using UITemplate.Events;
using UITemplate.Presenter;
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

        public UIManager(StartingPopupPresenter startingPopupPresenter, ChefPackPopupPresenter chefPackPopupPresenter, ChefPackInfoPopupPresenter chefPackInfoPopupPresenter)
        {
            _startingPopupPresenter = startingPopupPresenter;
            _chefPackPopupPresenter = chefPackPopupPresenter;
            _chefPackInfoPopupPresenter = chefPackInfoPopupPresenter;
        }

        public void Initialize()
        {
            MessageBroker.Default.Receive<ChefPackInfoOpenEvent>().Subscribe(_ => OpenChefPackInfoPopup());
        }

        private void OpenChefPackInfoPopup()
        {
            _chefPackInfoPopupPresenter.OpenView();
        }

        public void Run()
        {
            OpenStartingPopup();
        }

        private void OpenStartingPopup()
        {
            _startingPopupPresenter.Setup(OnClaimPressed);
            _startingPopupPresenter.OpenView();
        }

        private void OnClaimPressed()
        {
            OpenChefPackPopup();
        }

        private void OpenChefPackPopup()
        {
            _chefPackPopupPresenter.OpenView();
        }
    }
}
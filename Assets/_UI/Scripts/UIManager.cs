using UITemplate.Presenter;

namespace UITemplate
{
    public class UIManager
    {
        private readonly StartingPopupPresenter _startingPopupPresenter;
        private readonly ChefPackPopupPresenter _chefPackPopupPresenter;

        public UIManager(StartingPopupPresenter startingPopupPresenter, ChefPackPopupPresenter chefPackPopupPresenter)
        {
            _startingPopupPresenter = startingPopupPresenter;
            _chefPackPopupPresenter = chefPackPopupPresenter;
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
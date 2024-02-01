using UITemplate.Presenter;

namespace UITemplate
{
    public class UIManager
    {
        private readonly StartingWindowPresenter _startingWindowPresenter;
        private readonly ChefPackWindowPresenter _chefPackWindowPresenter;

        public UIManager(StartingWindowPresenter startingWindowPresenter, ChefPackWindowPresenter chefPackWindowPresenter)
        {
            _startingWindowPresenter = startingWindowPresenter;
            _chefPackWindowPresenter = chefPackWindowPresenter;
        }

        public void Run()
        {
            OpenStartingPopup();
        }

        private void OpenStartingPopup()
        {
            _startingWindowPresenter.Setup(OnClaimPressed);
            _startingWindowPresenter.OpenView();
        }

        private void OnClaimPressed()
        {
            OpenChefPackPopup();
        }

        private void OpenChefPackPopup()
        {
            _chefPackWindowPresenter.OpenView();
        }
    }
}
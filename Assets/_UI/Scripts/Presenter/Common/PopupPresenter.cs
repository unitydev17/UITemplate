using UITemplate.View;
using VContainer.Unity;

namespace UITemplate.Presenter
{
    public abstract class PopupPresenter<TV, TM> : WindowPresenter<TV, TM>, IInitializable where TV : PopupView
    {
        protected PopupPresenter(TV view, TM model) : base(view, model)
        {
        }

        public virtual void Initialize()
        {
            Register(view.onSkipBtnClick, OnSkipClick);
            Register(view.onCloseBtnClick, OnCloseClick);
        }

        private void OnSkipClick()
        {
            CloseView(CloseAction);
        }

        private void OnCloseClick()
        {
            CloseView(CloseAction);
        }
    }
}
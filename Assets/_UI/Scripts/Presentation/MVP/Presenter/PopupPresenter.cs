using UITemplate.View;
using VContainer.Unity;

namespace UITemplate.Presentation.Presenters.Common
{
    public abstract class PopupPresenter<TView, TModel> : WindowPresenter<TView, TModel>, IInitializable where TView : PopupView
    {
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
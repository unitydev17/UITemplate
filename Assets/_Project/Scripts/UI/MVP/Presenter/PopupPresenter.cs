using UITemplate.View;
using VContainer.Unity;

namespace UITemplate.Presentation.MVP.Presenter
{
    public abstract class PopupPresenter<TView, TModel> : WindowPresenter<TView, TModel>, IInitializable, IStartable where TView : PopupView
    {
        public virtual void Initialize()
        {
            Register(view.onSkipBtnClick, OnSkipClick);
            Register(view.onCloseBtnClick, OnCloseClick);
        }

        public void Start()
        {
            OpenView();
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
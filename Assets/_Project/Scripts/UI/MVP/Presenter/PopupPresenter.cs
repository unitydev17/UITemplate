using System;
using UITemplate.UI.MVP.View;
using UniRx;
using VContainer.Unity;

namespace UITemplate.UI.MVP.Presenter
{
    public abstract class PopupPresenter<TView, TModel> : WindowPresenter<TView, TModel>, IInitializable, IStartable where TView : PopupView
    {
        public virtual void Initialize()
        {
            Register(view.onSkipBtnClick, OnSkipClick);
            Register(view.onCloseBtnClick, OnCloseClick);
        }

        protected void RegisterCloser(IObservable<Unit> closer)
        {
            Register(closer, OnCloseClick);
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
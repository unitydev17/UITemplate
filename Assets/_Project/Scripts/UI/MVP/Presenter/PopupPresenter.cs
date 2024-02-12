using System;
using UITemplate.View;
using UniRx;
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

        protected void OnCloseClick()
        {
            CloseView(CloseAction);
        }
    }
}
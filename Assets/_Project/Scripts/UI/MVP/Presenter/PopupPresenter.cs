using System;
using UITemplate.UI.MVP.View;
using UniRx;
using VContainer.Unity;

namespace UITemplate.UI.MVP.Presenter
{
    public abstract class PopupPresenter<TView, TModel> : WindowPresenter<TView, TModel>, IInitializable, IStartable where TView : PopupView
    {
        public Action onClosePopup;

        public virtual void Initialize()
        {
            Register(view.onSkipBtnClick, CloseClick);
            Register(view.onCloseBtnClick, CloseClick);
        }

        protected void RegisterCloser(IObservable<Unit> closer)
        {
            Register(closer, CloseClick);
        }

        public void Start()
        {
            OpenView();
        }

        protected void CloseClick()
        {
            CloseView(CloseAction);
            onClosePopup?.Invoke();
        }
    }
}
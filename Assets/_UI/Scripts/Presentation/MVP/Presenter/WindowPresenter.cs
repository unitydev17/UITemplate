using System;
using UITemplate.View;
using VContainer;

namespace UITemplate.Presentation.Presenters.Common
{
    public abstract class WindowPresenter<TView, TModel> : BasePresenter<TView, TModel> where TView : WindowView
    {
        protected WindowPresenter(TView view, TModel model, IObjectResolver container) : base(view, model, container)
        {
        }

        protected WindowPresenter()
        {
        }

        public void OpenView()
        {
            view.gameObject.SetActive(true);
        }

        protected void CloseView(Action callback)
        {
            view.Close(callback);
        }

        protected void CloseAction()
        {
        }
    }
}
using System;
using UITemplate.UI.MVP.View;

namespace UITemplate.UI.MVP.Presenter
{
    public abstract class WindowPresenter<TView, TModel> : BasePresenter<TView, TModel> where TView : WindowView
    {
        protected void OpenView()
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
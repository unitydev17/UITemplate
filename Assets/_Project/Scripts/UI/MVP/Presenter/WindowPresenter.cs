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

        protected void CloseView(Action callback = null)
        {
            view.Close(callback);
        }

        protected virtual void CloseAction()
        {
        }

        public void ShowView()
        {
            view.gameObject.SetActive(true);
        }
    }
}
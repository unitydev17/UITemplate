using System;
using UITemplate.View;

namespace UITemplate.Presentation.MVP.Presenter
{
    public abstract class WindowPresenter<TView, TModel> : BasePresenter<TView, TModel> where TView : WindowView
    {
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
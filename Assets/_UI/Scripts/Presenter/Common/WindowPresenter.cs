using System;
using UITemplate.View;

namespace UITemplate.Presenter
{
    public abstract class WindowPresenter<TV, TM> : BasePresenter<TV, TM> where TV : WindowView
    {

        protected WindowPresenter(TV view, TM model) : base(view, model)
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

        protected virtual void CloseAction()
        {
        }
    }
}
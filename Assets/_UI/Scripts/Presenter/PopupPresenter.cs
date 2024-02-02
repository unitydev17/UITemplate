using UITemplate.View;
using UnityEngine;
using VContainer.Unity;

namespace UITemplate.Presenter
{
    public abstract class PopupPresenter<TV, TM> : WindowPresenter<TV, TM>, IInitializable where TV : PopupView
    {
        protected PopupPresenter(TV view, TM model) : base(view, model)
        {
        }

        public virtual void Initialize()
        {
            Debug.Log($"PopupPresenter Start  {view}");
            Register(view.onCloseBtnClick, OnCloseClick);
        }

        private void OnCloseClick()
        {
            CloseView(CloseAction);
        }
    }
}
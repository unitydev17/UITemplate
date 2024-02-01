using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;
using UnityEngine;
using VContainer.Unity;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class ChefPackWindowPresenter : WindowPresenter<ChefPackWindowView, ChefPackWindowModel>, IInitializable
    {
        public ChefPackWindowPresenter(ChefPackWindowView view, ChefPackWindowModel model) : base(view, model)
        {
        }

        public void Initialize()
        {
            Register(view.onCloseBtnClick, OnCloseClick);
        }

        private void OnCloseClick()
        {
            CloseView(() => Debug.Log("Close click processed"));
        }
    }
}
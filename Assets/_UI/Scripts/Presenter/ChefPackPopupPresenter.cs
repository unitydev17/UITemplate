using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;
using UnityEngine;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class ChefPackPopupPresenter : PopupPresenter<ChefPackPopupView, ChefPackPopupModel>
    {
        public ChefPackPopupPresenter(ChefPackPopupView view, ChefPackPopupModel model) : base(view, model)
        {
        }

        protected override void CloseAction()
        {
            Debug.Log("Close chefpackpopup (presenter)");
        }
    }
}
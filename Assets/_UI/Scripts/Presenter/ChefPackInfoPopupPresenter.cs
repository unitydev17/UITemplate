using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;
using UnityEngine;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class ChefPackInfoPopupPresenter : PopupPresenter<ChefPackPopupView, ChefPackPopupModel>
    {
        public ChefPackInfoPopupPresenter(ChefPackPopupView view, ChefPackPopupModel model) : base(view, model)
        {
        }


        protected override void CloseAction()
        {
            Debug.Log("Close chef pack info popup (presenter)");
        }
    }
}
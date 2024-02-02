using JetBrains.Annotations;
using UITemplate.Events;
using UITemplate.Model;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class ChefPackPopupPresenter : PopupPresenter<ChefPackPopupView, ChefPackPopupModel>
    {
        public ChefPackPopupPresenter(ChefPackPopupView view, ChefPackPopupModel model) : base(view, model)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Register(view.onInfoClick, () => MessageBroker.Default.Publish(new ChefPackInfoOpenEvent()));
        }

        protected override void CloseAction()
        {
            Debug.Log("Close chef pack popup (presenter)");
        }
    }
}
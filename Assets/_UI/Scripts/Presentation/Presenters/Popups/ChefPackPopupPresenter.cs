using JetBrains.Annotations;
using UITemplate.Presentation.Model;
using UITemplate.Presentation.Presenters.Common;
using UITemplate.View;
using UniRx;

namespace UITemplate.Presentation.Presenters.Popups
{
    [UsedImplicitly]
    public class ChefPackPopupPresenter : PopupPresenter<ChefPackPopupView, BaseModel>
    {
        public ChefPackPopupPresenter(ChefPackPopupView view, BaseModel model) : base(view, model)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Register(view.onInfoClick, () => MessageBroker.Default.Publish(new ChefPackInfoOpenEvent()));
        }
    }

    internal class ChefPackInfoOpenEvent
    {
    }
}
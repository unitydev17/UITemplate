using JetBrains.Annotations;
using UITemplate.UI.MVP.Model;
using UITemplate.UI.MVP.Presenter;
using UniRx;

namespace UITemplate.UI.Windows.Popups.Promo
{
    [UsedImplicitly]
    public class PromoPopupPresenter : PopupPresenter<PromoPopupView, BaseModel>
    {
        public override void Initialize()
        {
            base.Initialize();
            Register(view.onInfoClick, () => MessageBroker.Default.Publish(new ChefPackInfoOpenEvent()));
            Register(view.onStubClick, CloseClick);
        }
    }

    internal class ChefPackInfoOpenEvent
    {
    }
}
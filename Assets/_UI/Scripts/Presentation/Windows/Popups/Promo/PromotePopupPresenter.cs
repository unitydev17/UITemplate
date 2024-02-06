using JetBrains.Annotations;
using UITemplate.Presentation.Model;
using UITemplate.Presentation.Presenters.Common;
using UniRx;

namespace UITemplate.Presentation.Windows.Popups.Promo
{
    [UsedImplicitly]
    public class PromoPopupPresenter : PopupPresenter<PromoPopupView, BaseModel>
    {
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
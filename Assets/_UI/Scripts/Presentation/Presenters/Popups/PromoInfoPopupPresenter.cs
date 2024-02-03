using JetBrains.Annotations;
using UITemplate.Presentation.Model;
using UITemplate.Presentation.Presenters.Common;
using UITemplate.View;

namespace UITemplate.Presentation.Presenters.Popups
{
    [UsedImplicitly]
    public class PromoInfoPopupPresenter : PopupPresenter<PromoInfoPopupView, BaseModel>
    {
        public PromoInfoPopupPresenter(PromoInfoPopupView view, BaseModel model) : base(view, model)
        {
        }
    }
}
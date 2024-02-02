using JetBrains.Annotations;
using UITemplate.Presentation.Model;
using UITemplate.Presentation.Presenters.Common;
using UITemplate.View;

namespace UITemplate.Presentation.Presenters.Popups
{
    [UsedImplicitly]
    public class ChefPackInfoPopupPresenter : PopupPresenter<ChefPackInfoPopupView, BaseModel>
    {
        public ChefPackInfoPopupPresenter(ChefPackInfoPopupView view, BaseModel model) : base(view, model)
        {
        }
    }
}
using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class ChefPackInfoPopupPresenter : PopupPresenter<ChefPackInfoPopupView, BaseModel>
    {
        public ChefPackInfoPopupPresenter(ChefPackInfoPopupView view, BaseModel model) : base(view, model)
        {
        }
    }
}
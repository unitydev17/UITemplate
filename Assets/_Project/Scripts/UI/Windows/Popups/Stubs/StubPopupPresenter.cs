using JetBrains.Annotations;
using UITemplate.UI.MVP.Model;
using UITemplate.UI.MVP.Presenter;


namespace UITemplate.UI.Windows.Popups.Settings
{
    [UsedImplicitly]
    public class StubPopupPresenter : PopupPresenter<StubPopupView, BaseModel>
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterCloser(view.onClosePopupClick);
        }
    }
}
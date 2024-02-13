using JetBrains.Annotations;
using UITemplate.UI.MVP.Presenter;


namespace UITemplate.UI.Windows.Popups.Settings
{
    [UsedImplicitly]
    public class StubPopupPresenter : PopupPresenter<StubPopupView, StubPopupModel>
    {
        public override void Initialize()
        {
            base.Initialize();
            RegisterCloser(view.onClosePopupClick);
        }
    }
}
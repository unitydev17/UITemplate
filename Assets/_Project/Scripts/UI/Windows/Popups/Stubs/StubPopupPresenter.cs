using JetBrains.Annotations;
using UITemplate.Presentation.MVP.Presenter;


namespace UITemplate.Presentation.Windows.Popups.Settings
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
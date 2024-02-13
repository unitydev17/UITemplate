using JetBrains.Annotations;
using UITemplate.UI.MVP.Presenter;
using UniRx;

namespace UITemplate.UI.Windows.Popups.Starting
{
    [UsedImplicitly]
    public class StartingPopupPresenter : PopupPresenter<StartingPopupView, StartingPopupModel>
    {
        public override void Initialize()
        {
            base.Initialize();
            Register(view.onClaimBtnClick, OnClaimClick);
        }

        private void OnClaimClick(Unit value)
        {
            CloseView(() => MessageBroker.Default.Publish(new CloseStartingPopupEvent {claimPressed = true}));
        }

        public void Setup()
        {
            model.timeAbsent = "11h 22m";
            view.Refresh(model);
        }
    }

    internal class CloseStartingPopupEvent
    {
        public bool claimPressed;
    }
}
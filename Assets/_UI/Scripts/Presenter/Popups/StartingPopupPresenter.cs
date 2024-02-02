using JetBrains.Annotations;
using UITemplate.Model;
using UITemplate.View;
using UniRx;

namespace UITemplate.Presenter
{
    [UsedImplicitly]
    public class StartingPopupPresenter : PopupPresenter<StartingPopupView, StartingPopupModel>
    {
        public StartingPopupPresenter(StartingPopupView view, StartingPopupModel model) : base(view, model)
        {
        }

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
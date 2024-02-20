using System;
using JetBrains.Annotations;
using UITemplate.Common.Events;
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
            Register(view.onClaimX2BtnClick, OnClaimX2Click);
            Register(view.onBoostBtnClick, OnBoostClick);
        }

        private static void OnBoostClick()
        {
            MessageBroker.Default.Publish(new BoostRequestEvent());
        }

        private void OnClaimClick(Unit value)
        {
            CloseView(() => MessageBroker.Default.Publish(new CloseStartingPopupEvent {claimPressed = true}));
        }

        private void OnClaimX2Click(Unit value)
        {
            CloseView(() => MessageBroker.Default.Publish(new CloseStartingPopupEvent {claimX2Pressed = true}));
        }

        public void Setup(int passiveIncome, double seconds)
        {
            model.passiveIncome = passiveIncome;

            var timeSpan = TimeSpan.FromSeconds(seconds);
            var days = timeSpan.Days;
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            var sec = timeSpan.Seconds;
            model.timeAbsent = (days > 0 ? $"{days} days " : "") +
                               (hours > 0 ? $"{hours} hours " : "") +
                               (minutes > 0 ? $"{minutes} minutes " : "") +
                               (sec > 0 ? $"{sec} seconds " : "");

            view.Refresh(model);
        }
    }

    internal class BoostRequestEvent
    {
    }
}
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

        public StartingPopupPresenter Setup(int passiveIncome, double seconds)
        {
            model.passiveIncome = passiveIncome;

            var timeSpan = TimeSpan.FromSeconds(seconds);
            var days = timeSpan.Days;
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;
            var sec = timeSpan.Seconds;

            if (days > 0) model.timeAbsent = $"{days} days {hours} hours";
            else if (hours > 0) model.timeAbsent = $"{hours} hours {minutes} minutes";
            else if (minutes > 0) model.timeAbsent = $"{minutes} minutes {sec} seconds";
            else model.timeAbsent = $"{sec} seconds";

            view.Refresh(model);
            return this;
        }
    }

    internal class BoostRequestEvent
    {
    }
}
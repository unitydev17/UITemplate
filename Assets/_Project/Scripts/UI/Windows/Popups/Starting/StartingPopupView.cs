using System;
using TMPro;
using UITemplate.Application;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Windows.Popups.Starting
{
    public class StartingPopupView : PopupView
    {
        [SerializeField] private TMP_Text _timeAbsent;
        [SerializeField] private ButtonWidget _claimButtonWidget;
        [SerializeField] private ButtonWidget _claimX2ButtonWidget;
        [SerializeField] private ButtonWidget _boostClockButtonWidget;

        public IObservable<Unit> onClaimBtnClick => _claimButtonWidget.onClick.AsObservable();
        public IObservable<Unit> onClaimX2BtnClick => _claimX2ButtonWidget.onClick.AsObservable();
        public IObservable<Unit> onBoostBtnClick => _boostClockButtonWidget.onClick.AsObservable();

        public void Refresh(StartingPopupModel model)
        {
            var message = string.Format(Constants.TimeAbsentTime, model.timeAbsent);
            if (model.passiveIncome > 0) message += string.Format(Constants.TimeAbsentEarnings, model.passiveIncome);
            _timeAbsent.SetText(message);
        }
    }
}
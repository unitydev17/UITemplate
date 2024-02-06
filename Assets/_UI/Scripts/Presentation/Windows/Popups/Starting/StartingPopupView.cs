using System;
using TMPro;
using UITemplate.Application;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presentation.Windows.Popups.Starting
{
    public class StartingPopupView : PopupView
    {
        [SerializeField] private TMP_Text _timeAbsent;
        [SerializeField] private ButtonWidget claimButtonWidget;

        public IObservable<Unit> onClaimBtnClick => claimButtonWidget.onClick.AsObservable();

        public void Refresh(StartingPopupModel model)
        {
            _timeAbsent.SetText(string.Format(Constants.TimeAbsentEarnings, model.timeAbsent));
        }
    }
}
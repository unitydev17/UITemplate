using System;
using TMPro;
using UITemplate.Model;
using UniRx;
using UnityEngine;

namespace UITemplate.View
{
    public sealed class StartingWindowView : WindowView
    {
        [SerializeField] private TMP_Text _timeAbsent;
        [SerializeField] private ButtonView _claimButtonView;

        public IObservable<Unit> onClaimBtnClick => _claimButtonView.onClick.AsObservable();

        public void Refresh(StartingWindowModel model)
        {
            _timeAbsent.SetText(string.Format(Constants.TimeAbsentEarnings, model.timeAbsent));
        }
    }
}
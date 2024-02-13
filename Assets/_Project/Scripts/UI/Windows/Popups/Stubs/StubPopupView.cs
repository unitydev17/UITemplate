using System;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Windows.Popups.Settings
{
    public class StubPopupView : PopupView
    {
        [SerializeField] public ButtonWidget _closerBtn;

        public IObservable<Unit> onClosePopupClick => _closerBtn.onClick.AsObservable();
    }
}
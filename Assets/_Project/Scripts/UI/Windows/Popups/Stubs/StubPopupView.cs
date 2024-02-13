using System;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Windows.Popups.Settings
{
    public class StubPopupView : PopupView
    {
        [SerializeField] public ButtonWidget _closeBtn;

        public IObservable<Unit> onClosePopupClick => _closeBtn.onClick.AsObservable();
    }
}
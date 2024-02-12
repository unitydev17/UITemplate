using System;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presentation.Windows.Popups.Settings
{
    public class StubPopupView : PopupView
    {
        [SerializeField] public ButtonWidget _closeBtn;

        public IObservable<Unit> onClosePopupClick => _closeBtn.onClick.AsObservable();
    }
}
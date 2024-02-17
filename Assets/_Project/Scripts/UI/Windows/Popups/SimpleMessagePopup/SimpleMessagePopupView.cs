using System;
using TMPro;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Windows.Popups.Settings
{
    public class SimpleMessagePopupView : PopupView
    {
        [SerializeField] public ButtonWidget _closerBtn;
        [SerializeField] public TMP_Text _headerText;
        [SerializeField] public TMP_Text _text;

        public IObservable<Unit> onClosePopupClick => _closerBtn.onClick.AsObservable();


        public void SetHeader(string header)
        {
            _headerText.text = header;
        }

        public void SetMessage(string message)
        {
            _text.text = message;
        }
    }
}
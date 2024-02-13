using System;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Windows.Popups.Promo
{
    public class PromoPopupView : PopupView
    {
        [SerializeField] private ButtonWidget _infoBtn;

        [SerializeField] private ButtonWidget _stubBtn;

        public IObservable<Unit> onInfoClick => _infoBtn.onClick.AsObservable();
        public IObservable<Unit> onStubClick => _stubBtn.onClick.AsObservable();
    }
}
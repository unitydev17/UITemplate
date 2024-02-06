using System;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presentation.Windows.Popups.Promo
{
    public class PromoPopupView : PopupView
    {
        [SerializeField] private ButtonWidget _infoBtn;

        public IObservable<Unit> onInfoClick => _infoBtn.onClick.AsObservable();
    }
}
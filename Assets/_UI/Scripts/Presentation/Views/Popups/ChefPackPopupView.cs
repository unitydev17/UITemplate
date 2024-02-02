using System;
using UniRx;
using UnityEngine;

namespace UITemplate.View
{
    public class ChefPackPopupView : PopupView
    {
        [SerializeField] private ButtonWidget _infoBtn;

        public IObservable<Unit> onInfoClick => _infoBtn.onClick.AsObservable();
    }
}
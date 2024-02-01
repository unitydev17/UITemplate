using System;
using UniRx;
using UnityEngine;

namespace UITemplate.View
{
    public class ChefPackWindowView : WindowView
    {
        [SerializeField] private ButtonView _closeButtonView;

        public IObservable<Unit> onCloseBtnClick => _closeButtonView.onClick.AsObservable();
    }
}
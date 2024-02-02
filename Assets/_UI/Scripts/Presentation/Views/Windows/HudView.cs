using System;
using UniRx;
using UnityEngine;

namespace UITemplate.View.Windows
{
    public class HudView : WindowView
    {
        [SerializeField] private ButtonWidget _settingsBtn;

        public IObservable<Unit> onSettingsBtnClick => _settingsBtn.onClick.AsObservable();
    }
}
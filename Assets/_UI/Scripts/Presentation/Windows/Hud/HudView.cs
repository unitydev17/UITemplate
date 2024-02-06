using System;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presentation.Windows.Hud
{
    public class HudView : WindowView
    {
        [SerializeField] private ButtonWidget _settingsBtn;

        public IObservable<Unit> onSettingsBtnClick => _settingsBtn.onClick.AsObservable();
    }
}
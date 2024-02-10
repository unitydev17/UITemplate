using System;
using TMPro;
using UITemplate.View;
using UniRx;
using UnityEngine;

namespace UITemplate.Presentation.Windows.Hud
{
    public class HudView : WindowView
    {
        [SerializeField] private ButtonWidget _settingsBtn;
        [SerializeField] private TMP_Text _coinsTxt;

        public IObservable<Unit> onSettingsBtnClick => _settingsBtn.onClick.AsObservable();

        public void UpdateCoins(int value)
        {
            _coinsTxt.text = value.ToString();
        }
    }
}
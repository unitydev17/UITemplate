using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UITemplate.UI.Windows.Hud
{
    public class HudView : WindowView
    {
        [SerializeField] private ButtonWidget _settingsBtn;
        [SerializeField] private TMP_Text _coinsTxt;
        [SerializeField] private ButtonWidget[] _stubBtnList;
        [SerializeField] private ColorButtonWidget _speedBtn;
        [SerializeField] private Image _progressSpeedUpImg;
        [SerializeField] private TMP_Text _progressTxt;

        public IObservable<Unit> onSettingsBtnClick => _settingsBtn.onClick.AsObservable();
        public IObservable<Unit> onSpeedBtnClick => _speedBtn.onClick.AsObservable();

        public void UpdateCoins(int value)
        {
            _coinsTxt.text = value.ToString();
        }

        public IEnumerable<IObservable<Unit>> onStubBtnClicks => _stubBtnList.Select(btn => btn.onClick.AsObservable()).AsEnumerable();

        public void UpdateSpeedUpTimer(in int valueRemain, in float progress)
        {
            _progressSpeedUpImg.fillAmount = progress;
            _progressTxt.text = $"{valueRemain} sec";
        }

        public void SetSpeedButtonActive(bool active)
        {
            _speedBtn.SetActive(active);
        }
    }
}
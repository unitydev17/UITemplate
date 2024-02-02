using System;
using UITemplate.Presentation.Model.Popups;
using UniRx;
using UnityEngine;

namespace UITemplate.View
{
    public class SettingsPopupView : PopupView
    {
        [SerializeField] private CheckBoxWidget _musicWgt;
        [SerializeField] private CheckBoxWidget _soundWgt;
        [SerializeField] private CheckBoxWidget _vibroWgt;
        [SerializeField] private ButtonWidget _privacyBtn;

        public IObservable<Unit> onChangeMusic => _musicWgt.onClick.AsObservable();
        public IObservable<Unit> onChangeSound => _soundWgt.onClick.AsObservable();
        public IObservable<Unit> onChangeVibro => _vibroWgt.onClick.AsObservable();
        
        public IObservable<Unit> onClickPrivacy => _privacyBtn.onClick.AsObservable();

        public void UpdateSettings(SettingsPopupModel model)
        {
            _musicWgt.UpdateState(model.musicOn);
            _soundWgt.UpdateState(model.soundOn);
            _vibroWgt.UpdateState(model.vibroOn);
        }
    }
}
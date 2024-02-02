using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UITemplate.View
{
    public class PopupView : WindowView
    {
        private Transform _popupTr;
        private Image _bg;
        private ButtonWidget _closeBtn;

        public IObservable<Unit> onCloseBtnClick => GetCloseBtnObservable();

        private IObservable<Unit> GetCloseBtnObservable()
        {
            if (!_closeBtn) _closeBtn = GetComponentInChildren<ButtonWidget>();
            return _closeBtn.onClick.AsObservable();
        }

        private void Awake()
        {
            Debug.Log("PopupView awake");
            _bg = GetComponent<Image>();
            _popupTr = transform.GetChild(0);
        }

        protected override void Appear()
        {
            _bg.enabled = true;

            _popupTr.DOKill();
            _popupTr.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBack);
        }

        protected override void Disappear(Action callback = null)
        {
            _popupTr.DOKill();
            _popupTr.DOScale(0, 0.15f).SetEase(Ease.InBack).OnComplete(() =>
            {
                _bg.enabled = false;
                base.Disappear(callback);
            });
        }
    }
}
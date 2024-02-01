using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UITemplate.View
{
    public class PopupWindow : WindowView
    {
        private Transform _popupTr;
        private Image _bg;

        private void Awake()
        {
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
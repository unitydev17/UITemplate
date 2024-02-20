using System;
using DG.Tweening;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UITemplate.UI.MVP.View
{
    public class PopupView : WindowView, IPointerDownHandler
    {
        [SerializeField] private bool _skippable;
        private Transform _popupTr;
        private ButtonWidget _closeBtn;

        public readonly Subject<Unit> onSkipBtnClick = new Subject<Unit>();
        public IObservable<Unit> onCloseBtnClick => GetCloseBtnObservable();

        private IObservable<Unit> GetCloseBtnObservable()
        {
            if (!_closeBtn) _closeBtn = GetComponentInChildren<CloseButtonWidget>();
            return _closeBtn ? _closeBtn.onClick.AsObservable() : null;
        }

        public void Awake()
        {
            _popupTr = transform.GetChild(0);
        }

        protected override void Appear()
        {
            SetBg(true);

            _popupTr.DOKill();
            _popupTr.DOScale(1, 0.5f).From(0.5f).SetEase(Ease.OutBack);
        }

        protected override void Disappear(Action callback = null)
        {
            _popupTr.DOKill();
            _popupTr.DOScale(0.5f, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
            {
                SetBg(false);
                base.Disappear(callback);
            });
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_skippable) return;
            onSkipBtnClick.OnNext(Unit.Default);
        }
    }
}
using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UITemplate.View
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

        public override void Awake()
        {
            base.Awake();
            _popupTr = transform.GetChild(0);
        }

        protected override void Appear()
        {
            SetBg(true);

            _popupTr.DOKill();
            _popupTr.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBack);
        }

        protected override void Disappear(Action callback = null)
        {
            _popupTr.DOKill();
            _popupTr.DOScale(0, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
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
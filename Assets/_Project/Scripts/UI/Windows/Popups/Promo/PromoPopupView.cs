using System;
using DG.Tweening;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Windows.Popups.Promo
{
    public class PromoPopupView : PopupView
    {
        [SerializeField] private Transform _promoIconTr;

        [SerializeField] private ButtonWidget _infoBtn;

        [SerializeField] private ButtonWidget _stubBtn;

        public IObservable<Unit> onInfoClick => _infoBtn.onClick.AsObservable();
        public IObservable<Unit> onStubClick => _stubBtn.onClick.AsObservable();

        private Sequence _seq;

        protected override void Appear()
        {
            base.Appear();
            StartAnimation();
        }

        protected override void Disappear(Action callback = null)
        {
            StopAnimation();
            base.Disappear(callback);
        }

        private void StopAnimation()
        {
            _seq?.Kill();
        }

        private void StartAnimation()
        {
            _seq = DOTween.Sequence();
            var from = new Vector3(0, 0, 10);
            var to = new Vector3(0, 0, -10);

            const float duration = 0.5f;
            _seq.Append(_promoIconTr.DOLocalRotate(to, duration).SetEase(Ease.Linear))
                .Append(_promoIconTr.DOLocalRotate(from, duration).SetEase(Ease.Linear)).SetLoops(-1);
            _seq.Play();
        }
    }
}
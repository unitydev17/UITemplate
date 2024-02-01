using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UITemplate.View
{
    public abstract class WindowView : MonoBehaviour, IPointerDownHandler
    {
        public readonly Subject<Unit> onSkipBtnClick = new Subject<Unit>();

        public void OnEnable()
        {
            Appear();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onSkipBtnClick.OnNext(Unit.Default);
        }

        public void Close(Action callback)
        {
            Disappear(callback);
        }

        private void Appear()
        {
            transform.DOKill();
            transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBack);
        }

        private void Disappear(Action callback = null)
        {
            transform.DOKill();
            transform.DOScale(0, 0.15f).SetEase(Ease.InBack).OnComplete(() =>
            {
                callback?.Invoke();
                gameObject.SetActive(false);
            });
        }
    }
}
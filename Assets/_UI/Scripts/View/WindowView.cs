using System;
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

        protected virtual void Appear()
        {
        }

        protected virtual void Disappear(Action callback = null)
        {
            callback?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
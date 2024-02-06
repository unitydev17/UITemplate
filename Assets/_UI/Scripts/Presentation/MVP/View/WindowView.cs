using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UITemplate.View
{
    public abstract class WindowView : MonoBehaviour, IPointerDownHandler, ISortedView
    {
        [SerializeField] private bool _skippable;

        public readonly Subject<Unit> onSkipBtnClick = new Subject<Unit>();

        private Image _bg;
        private Canvas _canvas;

        public virtual void Awake()
        {
            _bg = GetComponent<Image>();
            _canvas = GetComponent<Canvas>();
        }

        protected void SetBg(bool enable)
        {
            _bg.enabled = enable;
        }

        public void OnEnable()
        {
            Appear();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_skippable) return;
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
            GameObject go;
            (go = gameObject).SetActive(false);
            Destroy(go);
        }

        public void SetSortingOrder(int value)
        {
            _canvas.sortingOrder = value;
        }
    }
}
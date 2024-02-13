using System;
using UnityEngine;
using UnityEngine.UI;

namespace UITemplate.UI.MVP.View
{
    public abstract class WindowView : MonoBehaviour, ISortedView
    {
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
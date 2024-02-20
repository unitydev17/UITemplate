using System;
using UnityEngine;
using UnityEngine.UI;

namespace UITemplate.UI.MVP.View
{
    public abstract class WindowView : MonoBehaviour, ISortedView
    {
        private Image _bg;
        private Canvas _canvas;

        private Canvas canvas
        {
            get
            {
                if (_canvas == null) _canvas = GetComponent<Canvas>();
                return _canvas;
            }
        }

        private Image bg
        {
            get
            {
                if (_bg == null) _bg = GetComponent<Image>();
                return _bg;
            }
        }

        protected void SetBg(bool enable)
        {
            bg.enabled = enable;
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
            canvas.sortingOrder = value;
        }
    }
}
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UITemplate.View
{
    public class ButtonView : Button, IDragHandler
    {
        public new ButtonClickedEvent onClick;
        private bool _tap;
        private Vector2 _tapPosition;


        public override void OnPointerDown(PointerEventData eventData)
        {
            _tap = true;
            _tapPosition = eventData.position;
            AnimatePress();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!_tap) return;
            _tap = false;

            AnimateRelease();

            var moveDistance = Vector2.Distance(eventData.position, _tapPosition);

            if (Math.Abs(moveDistance) > 1 && eventData.pointerCurrentRaycast.gameObject != gameObject) return;

            onClick?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_tap) return;

            if (eventData.pointerCurrentRaycast.gameObject == gameObject) return;

            AnimateRelease();
            _tap = false;
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
        }

        private void AnimatePress()
        {
            transform.DOKill();
            transform.DOScale(0.8f, 0);
        }

        private void AnimateRelease()
        {
            transform.DOKill();
            transform.DOScale(1, 0.15f).From(0.8f).SetEase(Ease.OutBack);
        }
    }
}
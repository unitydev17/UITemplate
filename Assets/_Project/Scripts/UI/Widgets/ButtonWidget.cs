using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UITemplate.UI.Widgets
{
    public class ButtonWidget : Button, IDragHandler
    {
        public new ButtonClickedEvent onClick;
        private bool _tap;


        public override void OnPointerDown(PointerEventData eventData)
        {
            _tap = true;
            AnimatePress();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!_tap) return;
            _tap = false;

            AnimateRelease();

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
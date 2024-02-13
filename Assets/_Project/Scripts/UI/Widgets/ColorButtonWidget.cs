using UnityEngine;
using UnityEngine.UI;

namespace UITemplate.UI.Widgets
{
    public class ColorButtonWidget : ButtonWidget
    {
        [SerializeField] private Color _idleColor;
        [SerializeField] private Color _activeColor;

        private Image _bgImg;

        protected override void Awake()
        {
            base.Awake();
            _bgImg = GetComponent<Image>();
        }

        public void SetActive(bool active)
        {
            _bgImg.color = active ? _activeColor : _idleColor;
        }
    }
}
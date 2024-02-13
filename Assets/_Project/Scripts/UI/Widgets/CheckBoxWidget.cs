using System;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Widgets
{
    public class CheckBoxWidget : MonoBehaviour
    {
        [SerializeField] private GameObject _checked;
        [SerializeField] private GameObject _unchecked;
        [SerializeField] private ButtonWidget _buttonWidget;

        public IObservable<Unit> onClick => _buttonWidget.onClick.AsObservable();

        public void UpdateState(bool state)
        {
            _checked.SetActive(state);
            _unchecked.SetActive(!state);
        }
    }
}
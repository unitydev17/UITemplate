using System;
using UITemplate.View;
using UniRx;
using UnityEngine;

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
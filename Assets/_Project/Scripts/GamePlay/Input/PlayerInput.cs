using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    public static event Action<Vector3> OnDragMove;
    public static event Action<Vector3> OnSlide;

    private bool _tap;
    private Vector3 _position;


    private void Start()
    {
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(_ => OnPointerDown()).AddTo(this);
        Observable.EveryUpdate().Where(_ => Input.GetMouseButton(0)).Subscribe(_ => OnDrag()).AddTo(this);
        Observable.EveryUpdate().Where(_ => Input.GetMouseButtonUp(0)).Subscribe(_ => OnPointerUp()).AddTo(this);
    }

    private void OnPointerDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _tap = true;
        _position = Input.mousePosition;
    }

    private void OnDrag()
    {
        if (!_tap) return;

        var deltaPos = (Input.mousePosition - _position).normalized;
        _position = Input.mousePosition;

        OnDragMove?.Invoke(deltaPos);
    }

    private void OnPointerUp()
    {
        if (!_tap) return;
        _tap = false;

        var deltaPos = (Input.mousePosition - _position).normalized;
        OnSlide?.Invoke(deltaPos);
    }
    
}
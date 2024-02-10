using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static event Action<Vector2> OnDragMove;

    private bool _tap;
    [SerializeField] private Vector2 _position;

    public void OnPointerDown(PointerEventData eventData)
    {
        _tap = true;
        _position = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_tap) return;

        var deltaPos = (eventData.position - _position).normalized;
        _position = eventData.position;
        
        OnDragMove?.Invoke(deltaPos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _tap = false;
    }
}
using UniRx;
using UnityEngine;

public class RaycastClicker : MonoBehaviour
{
    private const int ClickThreshold = 1;
    private Vector3 _mousePos;
    private Camera _camera;
    private readonly RaycastHit2D[] _hits = new RaycastHit2D[5];

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => HandlePressed())
            .AddTo(this);

        Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonUp(0))
            .Subscribe(_ => HandleReleased())
            .AddTo(this);
    }

    private void HandleReleased()
    {
        var deltaPos = Input.mousePosition - _mousePos;
        if (deltaPos.magnitude > ClickThreshold) return;

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics2D.RaycastNonAlloc(ray.origin, ray.direction, _hits);

        foreach (var hit in _hits)
        {
            if (hit.collider != null) ProcessHit(hit);
        }
    }

    private static void ProcessHit(RaycastHit2D hit)
    {
        // to smth
    }

    private void HandlePressed()
    {
        _mousePos = Input.mousePosition;
    }
}
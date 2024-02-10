using UniRx;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _dragVelocity;
    [SerializeField] private float _slideVelocity;
    [SerializeField] private float _damping;

    private Vector3 _velocity;

    private void OnEnable()
    {
        PlayerInput.OnDragMove += DragCamera;
        PlayerInput.OnSlide += SlideCamera;
    }

    private void OnDisable()
    {
        PlayerInput.OnDragMove -= DragCamera;
        PlayerInput.OnSlide -= SlideCamera;
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => UpdatePosition()).AddTo(this);
    }

    private void UpdatePosition()
    {
        transform.position += -_velocity * Time.deltaTime;

        _velocity *= _damping;
        if (_velocity.magnitude < 0.1f) _velocity = Vector3.zero;
    }

    private void SlideCamera(Vector3 deltaPos)
    {
        _velocity += deltaPos * _slideVelocity;
    }

    private void DragCamera(Vector3 deltaPos)
    {
        _velocity = deltaPos * _dragVelocity;
    }
}
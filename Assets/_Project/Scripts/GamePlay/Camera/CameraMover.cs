using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField] private float _speed;
    private void OnEnable()
    {
        PlayerInput.OnDragMove += MoveCamera;
    }

    private void OnDisable()
    {
        PlayerInput.OnDragMove -= MoveCamera;
    }

    private void MoveCamera(Vector2 deltaPos)
    {
        transform.Translate(-deltaPos * (Time.deltaTime * _speed));
    }
}
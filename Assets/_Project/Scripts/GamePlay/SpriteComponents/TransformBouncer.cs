using System;
using DG.Tweening;
using UnityEngine;

public class TransformBouncer : MonoBehaviour
{
    [SerializeField] private bool _bounceOnEnable;
    private Vector3 _startScale;
    private Transform _tr;

    private void OnEnable()
    {
        if (_bounceOnEnable) Appear();
    }

    private void Awake()
    {
        _tr = transform;
        _startScale = _tr.localScale;
    }


    public void Hide(Action callback)
    {
        _tr.DOKill();
        _tr.DOScale(0.25f, 0f).SetEase(Ease.OutSine).OnComplete(() => callback?.Invoke());
    }

    private void Bounce()
    {
        _tr.DOKill();
        _tr.DOScale(_startScale, 0.25f).From(_startScale * 0.8f).SetEase(Ease.OutBack);
    }
    
    private void Appear()
    {
        _tr.DOKill();
        _tr.DOScale(_startScale, 0.25f).From(0).SetEase(Ease.OutBack);
    }
}
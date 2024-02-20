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
        Bounce();
    }

    private Transform tr
    {
        get
        {
            if (_tr == null)
            {
                _tr = transform;
            }

            return _tr;
        }
    }

    private Vector3 startScale
    {
        get
        {
            if (_startScale == Vector3.zero)
            {
                _startScale = tr.localScale;
            }

            return _startScale;
        }
    }


    public void Hide(Action callback, bool immediate = false)
    {
        if (immediate)
        {
            tr.localScale = Vector3.zero;
            callback?.Invoke();
            return;
        }

        tr.DOKill();
        tr.DOScale(0.25f, 0f).SetEase(Ease.OutSine).OnComplete(() => callback?.Invoke());
    }

    private void Bounce()
    {
        tr.DOKill();
        tr.DOScale(startScale, 0.25f).From(_startScale * 0.8f).SetEase(Ease.OutBack);
    }

    public void Appear(bool immediate = false)
    {
        if (immediate)
        {
            tr.localScale = startScale;
            return;
        }

        tr.DOKill();
        tr.DOScale(startScale, 0.25f).From(0).SetEase(Ease.OutBack);
    }
}
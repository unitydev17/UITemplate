using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] private Image _img;

    public void Animate()
    {
        transform.DOKill();
        transform.DOScale(2, 0.5f).From(1).SetEase(Ease.OutSine);

        _img.DOKill();
        _img.DOFade(0, 0.35f).From(0.25f).SetEase(Ease.OutSine);
    }
}
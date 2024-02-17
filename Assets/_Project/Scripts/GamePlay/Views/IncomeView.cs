using DG.Tweening;
using TMPro;
using UnityEngine;

public class IncomeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueTxt;

    public void Activate(int value)
    {
        _valueTxt.text = $"+ {value}";

        transform.DOKill();
        transform.DOLocalMoveY(1.6f, 2).From(0.6f).SetEase(Ease.OutSine).OnComplete(() => { gameObject.SetActive(false); });
    }

    public void Deactivate()
    {
        transform.DOKill();
    }
}
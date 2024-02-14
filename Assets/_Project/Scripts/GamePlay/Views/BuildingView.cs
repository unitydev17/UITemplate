using TMPro;
using UITemplate.Common.Dto;
using UITemplate.Events;
using UITemplate.UI.Widgets;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BuildingView : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private TransformBouncer _buyArea;
    [SerializeField] private ButtonWidget _buyBtn;
    [SerializeField] private TMP_Text _buyCostTxt;


    [SerializeField] private GameObject _upgradeArea;
    [SerializeField] private ButtonWidget _upgradeBtn;
    [SerializeField] private TMP_Text _upgradeCostTxt;

    [SerializeField] private Image _upgradeProgressImg;
    [SerializeField] private Image _incomeProgressImg;

    [SerializeField] private IncomeView _incomeView;
    [SerializeField] private TMP_Text _incomeHelperTxt;

    [SerializeField] private ClickEffect _clickEffect;
    public int id => _id;

    private void Start()
    {
        _buyBtn.onClick.AsObservable().Subscribe(_ => HandleUpgradeClick()).AddTo(this);
        _upgradeBtn.onClick.AsObservable().Subscribe(_ => HandleUpgradeClick()).AddTo(this);

        MessageBroker.Default.Receive<UpgradeResponseEvent>().Subscribe(HandleUpgradeResponseEvent).AddTo(this);
        MessageBroker.Default.Receive<IncomeEvent>().Subscribe(HandleIncomeEvent).AddTo(this);
    }

    private void HandleIncomeEvent(IncomeEvent data)
    {
        var dto = data.dto;
        if (dto.id != gameObject.GetInstanceID()) return;

        _incomeView.gameObject.SetActive(true);
        _incomeView.Activate(dto.currentIncome);

        UpdateInfo(dto);
    }

    private void HandleUpgradeClick()
    {
        MessageBroker.Default.Publish(new UpgradeRequestEvent(_id));
        _clickEffect.Animate();
    }


    private void HandleUpgradeResponseEvent(UpgradeResponseEvent data)
    {
        var dto = data.dto;
        if (dto.id != _id) return;

        if (IsBuildingOpen(dto))
        {
            UpdateInfo(dto);
            return;
        }

        OpenBuilding(dto);
    }

    private static bool IsBuildingOpen(BuildingDto dto) => dto.level > 1;

    private void OpenBuilding(BuildingDto dto)
    {
        _buyArea.Hide(() =>
        {
            _buyArea.gameObject.SetActive(false);
            _upgradeArea.SetActive(true);
            UpdateInfo(dto);
        });
    }

    public void UpdateInfo(BuildingDto dto)
    {
        _upgradeCostTxt.text = dto.nextUpgradeCost == 0 ? "Max" : dto.nextUpgradeCost.ToString();
        _buyCostTxt.text = dto.nextUpgradeCost.ToString();

        _upgradeProgressImg.fillAmount = dto.upgradeCompletion;
        _incomeProgressImg.fillAmount = dto.incomeCompletion;

        _incomeHelperTxt.text = dto.nextDeltaIncome == 0 ? string.Empty : $"+{dto.nextDeltaIncome}";
    }
}
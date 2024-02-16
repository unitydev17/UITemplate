using TMPro;
using UITemplate.Common.Dto;
using UITemplate.Common.Events;
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
        _buyBtn.onClick.AsObservable().Subscribe(_ => UpgradeClickHandler()).AddTo(this);
        _upgradeBtn.onClick.AsObservable().Subscribe(_ => UpgradeClickHandler()).AddTo(this);

        MessageBroker.Default.Receive<UpgradeResponseEvent>().Subscribe(UpgradeResponseEventHandler).AddTo(this);
    }

    private void UpgradeClickHandler()
    {
        MessageBroker.Default.Publish(new UpgradeRequestEvent(_id));
        _clickEffect.Animate();
    }


    private void UpgradeResponseEventHandler(UpgradeResponseEvent data)
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


    public void UpdateInfo(BuildingDto dto)
    {
        _upgradeCostTxt.text = dto.nextUpgradeCost == 0 ? "Max" : dto.nextUpgradeCost.ToString();
        _buyCostTxt.text = dto.nextUpgradeCost.ToString();

        _upgradeProgressImg.fillAmount = dto.upgradeProgress;
        _incomeProgressImg.fillAmount = dto.incomeProgress;

        _incomeHelperTxt.text = dto.nextDeltaIncome == 0 ? string.Empty : $"+{dto.nextDeltaIncome}";

        ReceiveIncome(dto);
        OpenBuilding(dto);
    }

    private void OpenBuilding(BuildingDto dto)
    {
        if (dto.level < 1) return;

        _buyArea.Hide(() =>
        {
            OpenBuilding();
            UpdateInfo(dto);
        });
    }

    private void OpenBuilding()
    {
        _buyArea.gameObject.SetActive(false);
        _upgradeArea.SetActive(true);
    }

    private void ReceiveIncome(BuildingDto dto)
    {
        if (dto.incomeReceived == false) return;
        _incomeView.gameObject.SetActive(true);
        _incomeView.Activate(dto.currentIncome);
    }
}
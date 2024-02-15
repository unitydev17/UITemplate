using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.Interfaces;
using UnityEngine;

[UsedImplicitly]
public class SceneService : ISceneService
{
    private readonly Dictionary<int, BuildingView> _buildings = new Dictionary<int, BuildingView>();
    private readonly UpgradeCfg _cfg;


    public SceneService(UpgradeCfg cfg)
    {
        _cfg = cfg;
    }

    public List<Building> FetchBuildingsFromScene()
    {
        var buildingViews = Object.FindObjectsOfType<BuildingView>();
        var result = new List<Building>();

        foreach (var view in buildingViews)
        {
            _buildings.Add(view.id, view);

            result.Add(new Building
            {
                id = view.id,
                incomePerSecond = _cfg.baseIncomePerSecond
            });
        }

        return result;
    }

    public void UpdateBuildingViews(IEnumerable<BuildingDto> dtoList)
    {
        foreach (var dto in dtoList)
        {
            var viewKey = _buildings.Keys.Single(id => id == dto.id);
            var view = _buildings.GetValueOrDefault(viewKey);

            if (dto.level > 0) view.OpenBuilding();
            view.UpdateInfo(dto);
        }
    }
}
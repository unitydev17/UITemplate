using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Common.Dto;
using UITemplate.Core.Entities;
using UITemplate.Core.Interfaces;
using UnityEngine;

[UsedImplicitly]
public class WorldService : IWorldService
{
    private readonly Dictionary<int, BuildingView> _buildings = new Dictionary<int, BuildingView>();

    public List<Building> FetchBuildingsFromScene()
    {
        var buildingViews = Object.FindObjectsOfType<BuildingView>();
        var result = new List<Building>();

        foreach (var view in buildingViews)
        {
            var go = view.gameObject;
            _buildings.Add(go.GetInstanceID(), view);

            result.Add(new Building
            {
                id = go.GetInstanceID(),
                level = 0,
                currentIncome = 0,
                nextUpgradeCost = 11,
                incomeSpeed = 0.1f,
                incomeCompletion = 0,
                upgradeCompletion = 0
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
            view.UpdateInfo(dto);
        }
    }
}
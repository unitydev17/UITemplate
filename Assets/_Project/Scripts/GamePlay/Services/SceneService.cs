using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Interfaces;
using UnityEngine;

namespace UITemplate.GamePlay.Services
{
    [UsedImplicitly]
    public class SceneService : ISceneService
    {
        private readonly Dictionary<int, BuildingView> _buildings = new Dictionary<int, BuildingView>();
        private readonly UpgradeCfg _cfg;


        public SceneService(UpgradeCfg cfg)
        {
            _cfg = cfg;
        }

        public IEnumerable<BuildingDto> FetchBuildingsFromScene()
        {
            var buildingViews = Object.FindObjectsOfType<BuildingView>();
            var result = new List<BuildingDto>();

            foreach (var view in buildingViews)
            {
                _buildings.Add(view.id, view);

                result.Add(new BuildingDto()
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
                view.UpdateInfo(dto);
            }
        }
    }
}
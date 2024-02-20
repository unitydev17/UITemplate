using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Interfaces;
using UITemplate.GamePlay.Factory;
using UnityEngine;

namespace UITemplate.GamePlay.Services
{
    [UsedImplicitly]
    public class SceneService : ISceneService
    {
        private readonly Dictionary<int, BuildingView> _buildings = new Dictionary<int, BuildingView>();
        private readonly UpgradeCfg _cfg;
        private readonly LevelFactory _levelFactory;
        private GameObject _level;


        public SceneService(UpgradeCfg cfg, LevelFactory levelFactory)
        {
            _cfg = cfg;
            _levelFactory = levelFactory;
        }

        public IEnumerable<BuildingDto> FetchBuildingsFromScene()
        {
            var buildingViews = Object.FindObjectsOfType<BuildingView>(true);
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

        public void UpdateBuildingViews(IEnumerable<BuildingDto> dtoList, bool initStart = false)
        {
            foreach (var dto in dtoList)
            {
                var viewKey = _buildings.Keys.Single(id => id == dto.id);
                var view = _buildings.GetValueOrDefault(viewKey);
                view.UpdateInfo(dto, initStart);
            }
        }


        public async UniTask LoadLevel(int index)
        {
            ClearLevel();

            _levelFactory.SetLevelIndex(index);
            _level = await _levelFactory.Create();
        }

        public void ActivateLevel()
        {
            _level.gameObject.SetActive(true);
        }

        private void ClearLevel()
        {
            if (_buildings.Count > 0)
            {
                foreach (var view in _buildings.Values)
                {
                    view.Release();
                    view.gameObject.SetActive(false);
                    Object.Destroy(view);
                }
            }

            _buildings.Clear();

            if (!_level) return;

            _level.gameObject.SetActive(false);
            Object.Destroy(_level);
            _level = null;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Dto;
using UITemplate.Common.Interfaces;
using UITemplate.Infrastructure.Interfaces;
using UITemplate.UI.Managers;
using UnityEngine;

namespace UITemplate.GamePlay.Services
{
    [UsedImplicitly]
    public class SceneService : ISceneService
    {
        private readonly Dictionary<int, BuildingView> _buildings = new Dictionary<int, BuildingView>();
        private readonly UpgradeCfg _cfg;
        private readonly IPrefabLoadService _prefabLoadService;
        private GameObject _level;
        private readonly UIManager _ui;


        public SceneService(UpgradeCfg cfg, IPrefabLoadService prefabLoadService, UIManager ui)
        {
            _cfg = cfg;
            _prefabLoadService = prefabLoadService;
            _ui = ui;
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


        public async UniTask LoadLevel(int index)
        {
            ClearLevel();

            var normalizedLevel = _cfg.NormalizedLevel(index);
            _level = await _prefabLoadService.LoadLevelPrefab(normalizedLevel);
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
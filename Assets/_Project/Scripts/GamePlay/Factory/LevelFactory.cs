using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Interfaces;
using UITemplate.Infrastructure.Interfaces;
using UnityEngine;

namespace UITemplate.GamePlay.Factory
{
    [UsedImplicitly]
    public class LevelFactory : IFactory<GameObject>
    {
        private readonly IPrefabLoadService _prefabLoadService;
        private readonly UpgradeCfg _cfg;
        private int _levelIndex;

        public LevelFactory(IPrefabLoadService prefabLoadService, UpgradeCfg cfg)
        {
            _prefabLoadService = prefabLoadService;
            _cfg = cfg;
        }

        public void SetLevelIndex(int index)
        {
            _levelIndex = _cfg.NormalizedLevel(index);
        }

        public async UniTask<GameObject> Create()
        {
            var prefab = await _prefabLoadService.LoadLevelPrefab(_levelIndex);
            var gameObject = Object.Instantiate(prefab);
            gameObject.SetActive(false);
            return gameObject;
        }
    }
}
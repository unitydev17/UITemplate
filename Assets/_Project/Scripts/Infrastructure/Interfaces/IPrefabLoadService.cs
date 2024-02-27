using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UITemplate.Infrastructure.Interfaces
{
    public interface IPrefabLoadService
    {
        public UniTask<GameObject> LoadUIPrefab<T>();
        public UniTask<GameObject> LoadLevelPrefab(int index);
        public void ReleaseAsset<TView>();
        public void ReleaseLevelAsset(int index);
    }
}
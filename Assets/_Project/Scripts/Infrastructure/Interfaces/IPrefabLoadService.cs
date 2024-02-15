using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UITemplate.Infrastructure.Interfaces
{
    public interface IPrefabLoadService
    {
        public UniTask<GameObject> LoadUIPrefab<TView>();
    }
}
using UnityEngine;

namespace UITemplate.Core.Interfaces
{
    public interface XIPrefabLoadService
    {
        public GameObject GetPrefab<TView>();
    }
}
using UnityEngine;

namespace UITemplate.Core.Interfaces
{
    public interface IPrefabLoadService
    {
        public GameObject GetPrefab<TView>();
    }
}
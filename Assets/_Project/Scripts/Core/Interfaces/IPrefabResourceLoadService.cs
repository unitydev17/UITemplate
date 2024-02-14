using UnityEngine;

namespace UITemplate.Core.Interfaces
{
    public interface IPrefabResourceLoadService
    {
        public GameObject GetPrefab<TView>();
    }
}
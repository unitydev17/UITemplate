using UnityEngine;

namespace UITemplate.Infrastructure.Interfaces
{
    public interface IPrefabLoadService
    {
        public GameObject GetPrefab<TView>();
    }
}
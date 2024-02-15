using UnityEngine;

namespace UITemplate.Infrastructure.Interfaces
{
    public interface IPrefabResourceLoadService
    {
        public GameObject GetPrefab<TView>();
    }
}
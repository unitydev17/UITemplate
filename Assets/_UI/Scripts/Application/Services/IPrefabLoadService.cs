using UnityEngine;

namespace UITemplate.Application.Services
{
    public interface IPrefabLoadService
    {
        public GameObject GetPrefab<TView>();
    }
}
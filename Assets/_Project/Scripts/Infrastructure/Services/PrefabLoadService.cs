using System;
using UITemplate.Infrastructure.Interfaces;
using UnityEngine;

namespace UITemplate.Infrastructure.Services
{
    public class PrefabLoadService : IPrefabLoadService
    {
        public GameObject GetPrefab<TView>()
        {
            try
            {
                var path = typeof(TView).ToString().Split(".");
                return Resources.Load<GameObject>(path[^1]);
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            throw new Exception("Prefab not found");
        }
    }
}
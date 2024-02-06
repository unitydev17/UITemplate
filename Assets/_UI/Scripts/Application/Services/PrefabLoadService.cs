using System;
using UnityEngine;

namespace UITemplate.Application.Services
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
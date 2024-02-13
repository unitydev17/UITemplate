using System;
using UITemplate.Core.Interfaces;
using UnityEngine;

namespace UITemplate.Infrastructure.Services
{
    public class XPrefabLoadService : XIPrefabLoadService
    {
        public GameObject GetPrefab<TView>()
        {
            try
            {
                var path = typeof(TView).ToString().Split(".");
                return Resources.Load<GameObject>($"UI/{path[^1]}");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            throw new Exception("Prefab not found");
        }
    }
}
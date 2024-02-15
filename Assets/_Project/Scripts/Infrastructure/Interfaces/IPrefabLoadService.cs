using System;
using UnityEngine;

namespace UITemplate.Infrastructure.Interfaces
{
    public interface IPrefabLoadService
    {
        public void LoadUIPrefab<TView>(Action<GameObject> returnPrefabCallback);
    }
}
using System;
using UnityEngine;

namespace UITemplate.Core.Interfaces
{
    public interface IPrefabLoadService
    {
        public void LoadUIPrefab<TView>(Action<GameObject> returnPrefabCallback);
    }
}
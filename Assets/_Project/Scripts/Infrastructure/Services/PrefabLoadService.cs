using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Infrastructure.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UITemplate.Infrastructure.Services
{
    [UsedImplicitly]
    public class PrefabLoadService : IPrefabLoadService
    {
        public async UniTask<GameObject> LoadUIPrefab<TView>()
        {
            var address = GetAddress<TView>();
            return await GetPrefab(address);
        }

        private static string GetAddress<TView>()
        {
            var path = typeof(TView).ToString().Split(".");
            var address = $"UI/{path[^1]}";
            return address;
        }

        private static async UniTask<GameObject> GetPrefab(string address)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle;
            if (handle.Status == AsyncOperationStatus.Succeeded) return handle.Result;

            throw new Exception($"Can not load addressables prefab with address:{address}");
        }
    }
}
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
            var address = GetAddress<TView>("UI");
            return await GetPrefab(address);
        }

        private static string GetAddress<T>(string prefix)
        {
            var path = typeof(T).ToString().Split(".");
            return $"{prefix}/{path[^1]}";
        }

        private static async UniTask<GameObject> GetPrefab(string address)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle;

            if (handle.Status != AsyncOperationStatus.Succeeded) throw new Exception($"Can not load addressable prefab with address:{address}");

            var result = handle.Result;
            Addressables.Release(handle);
            return result;
        }

        private static async UniTask<GameObject> InstantiatePrefab(string address)
        {
            var handle = Addressables.InstantiateAsync(address);
            await handle;
            if (handle.Status == AsyncOperationStatus.Succeeded) return handle.Result;

            throw new Exception($"Can not instantiate addressable prefab with address:{address}");
        }

        public async UniTask<GameObject> LoadLevelPrefab(int index)
        {
            var address = $"Levels/Level_{index}";
            return await InstantiatePrefab(address);
        }
    }
}
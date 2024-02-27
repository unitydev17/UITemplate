using System;
using System.Collections.Generic;
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
        private readonly Dictionary<Type, AsyncOperationHandle> _handlersUI = new Dictionary<Type, AsyncOperationHandle>();
        private readonly Dictionary<string, AsyncOperationHandle> _handlersByAddress = new Dictionary<string, AsyncOperationHandle>();

        public async UniTask<GameObject> LoadUIPrefab<TView>()
        {
            var address = GetAddress<TView>("UI");
            return await GetPrefab<TView>(address);
        }

        private static string GetAddress<T>(string prefix)
        {
            var path = typeof(T).ToString().Split(".");
            return $"{prefix}/{path[^1]}";
        }

        private async UniTask<GameObject> GetPrefab<TView>(string address)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle;

            if (handle.Status != AsyncOperationStatus.Succeeded) throw new Exception($"Can not load addressable prefab with address:{address}");

            var result = handle.Result;
            _handlersUI.TryAdd(typeof(TView), handle);
            return result;
        }

        private async UniTask<GameObject> GetPrefab(string address)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            await handle;

            if (handle.Status != AsyncOperationStatus.Succeeded) throw new Exception($"Can not load addressable prefab with address:{address}");

            var result = handle.Result;
            _handlersByAddress.TryAdd(address, handle);
            return result;
        }

        public void ReleaseAsset<TView>()
        {
            var type = typeof(TView);

            if (!_handlersUI.TryGetValue(type, out var handle)) return;
            _handlersUI.Remove(type);

            Addressables.Release(handle);
        }

        private static string LevelAddress(int index) => $"Levels/Level_{index}";

        public async UniTask<GameObject> LoadLevelPrefab(int index)
        {
            var address = LevelAddress(index);
            return await GetPrefab(address);
        }

        public void ReleaseLevelAsset(int index)
        {
            var address = LevelAddress(index);

            if (!_handlersByAddress.TryGetValue(address, out var handle)) return;
            _handlersByAddress.Remove(address);

            Addressables.Release(handle);
        }
    }
}
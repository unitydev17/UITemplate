using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Core.Interfaces;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UITemplate.Infrastructure.Services
{
    [UsedImplicitly]
    public class PrefabLoadServiceAsync : IPrefabLoadServiceAsync
    {
        public async Task<GameObject> GetPrefab<TView>()
        {
            var path = typeof(TView).ToString().Split(".");
            var address = $"UI/{path[^1]}";

            var tcs = new TaskCompletionSource<GameObject>();

            var handle = Addressables.LoadAssetAsync<GameObject>(address);
            handle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    tcs.TrySetResult(op.Result);
                }
                else
                {
                    tcs.TrySetException(new Exception($"Failed to load prefab with address: {address}"));
                }
            };
            await tcs.Task;
            return tcs.Task.Result;
        }
    }
}
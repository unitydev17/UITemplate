using System;
using JetBrains.Annotations;
using UITemplate.Core.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UITemplate.Infrastructure.Services
{
    [UsedImplicitly]
    public class PrefabLoadService : IPrefabLoadService
    {
        public void LoadUIPrefab<TView>(Action<GameObject> returnPrefabCallback)
        {
            var address = GetAddress<TView>();
            GetPrefab(address).Subscribe(returnPrefabCallback);
        }

        private static string GetAddress<TView>()
        {
            var path = typeof(TView).ToString().Split(".");
            var address = $"UI/{path[^1]}";
            return address;
        }

        private static IObservable<GameObject> GetPrefab(string address)
        {
            return Observable.Create<GameObject>(observer =>
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(address);
                handle.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        observer.OnNext(op.Result);
                        observer.OnCompleted();
                    }
                    else
                    {
                        observer.OnError(new Exception($"Failed to load asset with address: {address}"));
                    }
                };

                return Disposable.Empty;
            });
        }
    }
}
using System;
using JetBrains.Annotations;
using UITemplate.Infrastructure.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UITemplate.Infrastructure.Services
{
    [UsedImplicitly]
    public class PrefabLoadService : IPrefabLoadService
    {
        private static readonly CompositeDisposable Disposable = new CompositeDisposable();
        public void LoadUIPrefab<TView>(Action<GameObject> returnPrefabCallback)
        {
            var address = GetAddress<TView>();
            GetPrefabAsObservable(address).Subscribe(returnPrefabCallback).AddTo(Disposable);
        }

        private static string GetAddress<TView>()
        {
            var path = typeof(TView).ToString().Split(".");
            var address = $"UI/{path[^1]}";
            return address;
        }

        private static IObservable<GameObject> GetPrefabAsObservable(string address)
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
                        Disposable.Dispose();
                    }
                    else
                    {
                        observer.OnError(new Exception($"Failed to load asset with address: {address}"));
                        Disposable.Dispose();
                    }
                };

                return UniRx.Disposable.Empty;
            });
        }
    }
}
using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace UITemplate.Utils
{
    public abstract class Registrable : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected void RegisterAsync<T>(IObservable<T> observable, Func<T, UniTask> handler)
        {
            if (observable == null) return;
            _disposables.Add(observable.Subscribe(async value => await handler(value)));
        }

        protected void Register<T>(IObservable<T> observable, Action handler)
        {
            if (observable == null) return;
            _disposables.Add(observable.Subscribe(value => handler?.Invoke()));
        }

        protected void Register<T>(IObservable<T> observable, Action<T> handler)
        {
            if (observable == null) return;
            _disposables.Add(observable.Subscribe(value => handler?.Invoke(value)));
        }

        protected void Register(IObservable<Unit> observable, Action handler)
        {
            if (observable == null) return;
            _disposables.Add(observable.Subscribe(value => handler?.Invoke()));
        }

        protected void Register(IObservable<Unit> observable, Action<Unit> handler)
        {
            _disposables.Add(observable.Subscribe(handler));
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        protected static IDisposable RegisterTemporary<T>(IObservable<T> observable, Action handler)
        {
            return observable.Subscribe(value => handler?.Invoke());
        }

        protected static void Dispose(IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
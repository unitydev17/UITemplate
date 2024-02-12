using System;
using UniRx;

namespace UITemplate.Utils
{
    public abstract class Registrable : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

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

        protected IDisposable RegisterTemporary<T>(IObservable<T> observable, Action handler)
        {
            var disposable = observable.Subscribe(value => handler?.Invoke());
            _disposables.Add(disposable);
            return disposable;
        }

        public void Dispose(IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
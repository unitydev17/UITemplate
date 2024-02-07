using System;
using UniRx;

namespace UITemplate.Presentation.MVP.Presenter
{
    public abstract class Registrable : IDisposable
    {
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

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
    }
}
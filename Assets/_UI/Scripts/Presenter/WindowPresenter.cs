using System;
using UITemplate.View;
using UniRx;

namespace UITemplate.Presenter
{
    public abstract class WindowPresenter<TV, TM> : IDisposable where TV : WindowView
    {
        protected readonly TV view;
        protected readonly TM model;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        protected void Register(IObservable<Unit> observable, Action handler)
        {
            _disposables.Add(observable.Subscribe(value => handler?.Invoke()));
        }
        
        protected void Register(IObservable<Unit> observable, Action<Unit> handler)
        {
            _disposables.Add(observable.Subscribe(handler));
        }

        protected WindowPresenter(TV view, TM model)
        {
            this.view = view;
            this.model = model;
        }

        public void OpenView()
        {
            view.gameObject.SetActive(true);
        }

        protected void CloseView(Action callback)
        {
            view.Close(callback);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
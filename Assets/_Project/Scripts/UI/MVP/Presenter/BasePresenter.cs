using VContainer;

namespace UITemplate.Presentation.MVP.Presenter
{
    public abstract class BasePresenter<TView, TModel> : Registrable
    {
        public IObjectResolver container;
        public TView view { get; set; }
        public TModel model { get; set; }

        protected TService Resolve<TService>()
        {
            return container.Resolve<TService>();
        }
    }
}
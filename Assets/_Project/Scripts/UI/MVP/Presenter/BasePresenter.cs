using UITemplate.Utils;
using VContainer;

namespace UITemplate.UI.MVP.Presenter
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
using UITemplate.Utils;

namespace UITemplate.UI.MVP.Presenter
{
    public abstract class BasePresenter<TView, TModel> : Registrable
    {
        public TView view { get; set; }
        public TModel model { get; set; }
    }
}
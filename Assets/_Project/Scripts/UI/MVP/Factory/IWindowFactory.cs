using UITemplate.Presentation.MVP.Presenter;
using UITemplate.View;

namespace UITemplate.Presentation.MVP.Factory
{
    public interface IWindowFactory
    {
        public TPresenter Create<TPresenter, TView, TModel>() where TModel : new() where TPresenter : BasePresenter<TView, TModel>, new() where TView : ISortedView;
    }
}
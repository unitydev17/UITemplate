namespace UITemplate.Presenter
{
    public abstract class BasePresenter<TV, TM> : Registrable
    {
        protected readonly TV view;
        protected readonly TM model;


        protected BasePresenter(TV view, TM model)
        {
            this.view = view;
            this.model = model;
        }
    }
}
using JetBrains.Annotations;
using UITemplate.Infrastructure.Interfaces;
using UITemplate.Presentation.MVP.Presenter;
using UITemplate.View;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace UITemplate.Presentation.MVP.Factory
{
    [UsedImplicitly]
    public class WindowFactory : IWindowFactory
    {
        private static int _order;

        private readonly IObjectResolver _container;
        private readonly IPrefabLoadService _prefabLoadService;

        public WindowFactory(IObjectResolver container, IPrefabLoadService prefabLoadService)
        {
            _container = container;
            _prefabLoadService = prefabLoadService;
        }

        public TPresenter Create<TPresenter, TView, TModel>() where TModel : new() where TPresenter : BasePresenter<TView, TModel>, new() where TView : ISortedView
        {
            var prefab = _prefabLoadService.GetPrefab<TView>();
            var view = Object.Instantiate(prefab).GetComponent<TView>();
            view.SetSortingOrder(_order++);

            var model = new TModel();
            var presenter = new TPresenter {view = view, model = model, container = _container};

            (presenter as IInitializable)?.Initialize();
            return presenter;
        }
    }
}
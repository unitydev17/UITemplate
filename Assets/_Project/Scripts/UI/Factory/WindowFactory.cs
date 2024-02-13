using JetBrains.Annotations;
using UITemplate.Core.Interfaces;
using UITemplate.UI.MVP.Model;
using UITemplate.UI.MVP.Presenter;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Windows.Hud;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Settings;
using UITemplate.UI.Windows.Popups.Starting;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace UITemplate.UI.Factory
{
    [UsedImplicitly]
    public class WindowFactory : IWindowFactory
    {
        private static int _order;

        private readonly IObjectResolver _container;
        private readonly IPrefabLoadService _prefabLoadService;
        private IPrefabLoadServiceAsync _prefabLoadServiceAsync;

        public WindowFactory(IObjectResolver container, IPrefabLoadService prefabLoadService, IPrefabLoadServiceAsync prefabLoadServiceAsync)
        {
            _container = container;
            _prefabLoadService = prefabLoadService;
            _prefabLoadServiceAsync = prefabLoadServiceAsync;
        }

        private TPresenter Create<TPresenter, TView, TModel>() where TModel : new() where TPresenter : BasePresenter<TView, TModel>, new() where TView : ISortedView
        {
            var prefab = _prefabLoadService.GetPrefab<TView>();
            var view = Object.Instantiate(prefab).GetComponent<TView>();
            view.SetSortingOrder(_order++);

            var model = new TModel();

            var presenter = _container.Resolve<TPresenter>();
            presenter.view = view;
            presenter.model = model;
            presenter.container = _container;

            (presenter as IInitializable)?.Initialize();
            (presenter as IStartable)?.Start();

            return presenter;
        }

        public StartingPopupPresenter GetStartingPopup()
        {
            return Create<StartingPopupPresenter, StartingPopupView, StartingPopupModel>();
        }

        public SettingsPopupPresenter GetSettingsPopup()
        {
            return Create<SettingsPopupPresenter, SettingsPopupView, SettingsPopupModel>();
        }

        public HudPresenter GetHud()
        {
            return Create<HudPresenter, HudView, HudModel>();
        }

        public StubPopupPresenter GetStubPopup()
        {
            return Create<StubPopupPresenter, StubPopupView, StubPopupModel>();
        }

        public PromoPopupPresenter GetPromoPopup()
        {
            return Create<PromoPopupPresenter, PromoPopupView, BaseModel>();
        }

        public PromoInfoPopupPresenter GetPromoInfoPopup()
        {
            return Create<PromoInfoPopupPresenter, PromoInfoPopupView, BaseModel>();
        }
    }
}
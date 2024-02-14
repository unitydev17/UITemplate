using System;
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
        private static int order;

        private readonly IObjectResolver _container;
        private readonly IPrefabLoadService _prefabLoadService;

        public WindowFactory(IObjectResolver container, IPrefabLoadService prefabLoadService)
        {
            _container = container;
            _prefabLoadService = prefabLoadService;
            _prefabLoadService = prefabLoadService;
        }

        private void Create<TPresenter, TView, TModel>(Action<TPresenter> callback) where TModel : new() where TPresenter : BasePresenter<TView, TModel>, new() where TView : ISortedView
        {
            _prefabLoadService.LoadUIPrefab<TView>(prefab =>
            {
                var view = Object.Instantiate(prefab).GetComponent<TView>();
                view.SetSortingOrder(order++);

                var model = new TModel();

                var presenter = _container.Resolve<TPresenter>();
                presenter.view = view;
                presenter.model = model;
                presenter.container = _container;

                (presenter as IInitializable)?.Initialize();
                (presenter as IStartable)?.Start();

                callback?.Invoke(presenter);
            });
        }


        public void GetStartingPopup(Action<StartingPopupPresenter> callback)
        {
            Create<StartingPopupPresenter, StartingPopupView, StartingPopupModel>(callback);
        }

        public void GetSettingsPopup(Action<SettingsPopupPresenter> callback)
        {
            Create<SettingsPopupPresenter, SettingsPopupView, SettingsPopupModel>(callback);
        }

        public void GetPromoPopup(Action<PromoPopupPresenter> callback)
        {
            Create<PromoPopupPresenter, PromoPopupView, BaseModel>(callback);
        }

        public void GetPromoInfoPopup(Action<PromoInfoPopupPresenter> callback)
        {
            Create<PromoInfoPopupPresenter, PromoInfoPopupView, BaseModel>(callback);
        }

        public void GetHudWindow(Action<HudPresenter> callback)
        {
            Create<HudPresenter, HudView, HudModel>(callback);
        }

        public void GetStubPopup(Action<StubPopupPresenter> callback)
        {
            Create<StubPopupPresenter, StubPopupView, StubPopupModel>(callback);
        }

        public void GetWelcomePopup(Action<WelcomePopupPresenter> callback = null)
        {
            Create<WelcomePopupPresenter, WelcomePopupView, BaseModel>(callback);
        }
    }
}
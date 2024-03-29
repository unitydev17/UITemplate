using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UITemplate.Infrastructure.Interfaces;
using UITemplate.UI.MVP.Model;
using UITemplate.UI.MVP.Presenter;
using UITemplate.UI.MVP.View;
using UITemplate.UI.Windows.Hud;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Settings;
using UITemplate.UI.Windows.Popups.Starting;
using UnityEngine;
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
        }

        private async UniTask<TPresenter> Create<TPresenter, TView, TModel>() where TModel : new() where TPresenter : BasePresenter<TView, TModel> where TView : ISortedView, IDestructable
        {
            var prefab = await _prefabLoadService.LoadUIPrefab<TView>();
            prefab.SetActive(false);

            var view = Object.Instantiate(prefab).GetComponent<TView>();
            view.SetSortingOrder(order++);
            view.SetDestructor(_prefabLoadService.ReleaseAsset<TView>);

            var model = new TModel();

            var presenter = _container.Resolve<TPresenter>();
            presenter.view = view;
            presenter.model = model;

            (presenter as IInitializable)?.Initialize();
            (presenter as IStartable)?.Start();

            return presenter;
        }

        public async UniTask<StartingPopupPresenter> GetStartingPopup()
        {
            return await Create<StartingPopupPresenter, StartingPopupView, StartingPopupModel>();
        }

        public async UniTask<SettingsPopupPresenter> GetSettingsPopup()
        {
            return await Create<SettingsPopupPresenter, SettingsPopupView, SettingsPopupModel>();
        }

        public async UniTask<PromoPopupPresenter> GetPromoPopup()
        {
            return await Create<PromoPopupPresenter, PromoPopupView, BaseModel>();
        }

        public async UniTask<PromoInfoPopupPresenter> GetPromoInfoPopup()
        {
            return await Create<PromoInfoPopupPresenter, PromoInfoPopupView, BaseModel>();
        }

        public async UniTask<HudPresenter> GetHudWindow()
        {
            return await Create<HudPresenter, HudView, HudModel>();
        }

        public async UniTask<StubPopupPresenter> GetStubPopup()
        {
            return await Create<StubPopupPresenter, StubPopupView, BaseModel>();
        }

        public async UniTask<WelcomePopupPresenter> GetWelcomePopup()
        {
            return await Create<WelcomePopupPresenter, WelcomePopupView, BaseModel>();
        }

        public async UniTask<SimpleMessagePopupPresenter> GetSimpleMessagePopup()
        {
            return await Create<SimpleMessagePopupPresenter, SimpleMessagePopupView, BaseModel>();
        }
    }
}
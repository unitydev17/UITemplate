using UITemplate.Application;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Core.Interfaces;
using UITemplate.Application.Services;
using UITemplate.Core.Entities;
using UITemplate.Core.Controller;
using UITemplate.Infrastructure.Services;
using UITemplate.UI.Factory;
using UITemplate.UI.Managers;
using UITemplate.UI.Windows.Hud;
using UITemplate.UI.Windows.Popups.Promo;
using UITemplate.UI.Windows.Popups.Settings;
using UITemplate.UI.Windows.Popups.Starting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UITemplate.Infrastructure.Installers
{
    public class GameScope : LifetimeScope
    {
        [SerializeField] private UpgradeCfg upgradeCfg;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterCommands(builder);

            RegisterUIViews(builder);

            RegisterScriptableObjects(builder);

            RegisterEntryPoint(builder);

            RegisterGameCore(builder);

            RegisterServices(builder);

            RegisterFactories(builder);

            RegisterUIManager(builder);
        }

        private void RegisterCommands(IContainerBuilder builder)
        {
            builder.Register<HudSpeedUpCommand>(Lifetime.Scoped);
        }

        private static void RegisterUIViews(IContainerBuilder builder)
        {
            builder.Register<HudPresenter>(Lifetime.Scoped);
            builder.Register<StartingPopupPresenter>(Lifetime.Scoped);
            builder.Register<SettingsPopupPresenter>(Lifetime.Scoped);
            builder.Register<PromoPopupPresenter>(Lifetime.Scoped);
            builder.Register<PromoInfoPopupPresenter>(Lifetime.Scoped);
            builder.Register<StubPopupPresenter>(Lifetime.Scoped);
        }

        private void RegisterScriptableObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(upgradeCfg);
        }

        private static void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AppBoot>();
        }

        private static void RegisterGameCore(IContainerBuilder builder)
        {
            builder.Register<PlayerData>(Lifetime.Scoped);
            builder.Register<GameManager>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<Settings>(Lifetime.Scoped);
        }

        private static void RegisterUIManager(IContainerBuilder builder)
        {
            builder.Register<UIManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<IWindowFactory, WindowFactory>(Lifetime.Scoped);
        }

        private static void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<IIncomeService, IncomeService>(Lifetime.Scoped);
            builder.Register<IUpgradeService, UpgradeService>(Lifetime.Scoped);
            builder.Register<ISettingsService, SettingsService>(Lifetime.Scoped);
            builder.Register<IWebService, WebService>(Lifetime.Scoped);
            builder.Register<IPrefabLoadService, PrefabLoadService>(Lifetime.Scoped);
            builder.Register<IPrefabLoadServiceAsync, PrefabLoadServiceAsync>(Lifetime.Scoped);
            builder.Register<IWorldService, WorldService>(Lifetime.Scoped);
        }
    }
}
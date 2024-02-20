using UITemplate.Application;
using UITemplate.Core.Interfaces;
using UITemplate.Application.Services;
using UITemplate.Common;
using UITemplate.Common.Interfaces;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.Controller;
using UITemplate.Core.Services;
using UITemplate.GamePlay.Factory;
using UITemplate.GamePlay.Services;
using UITemplate.Infrastructure;
using UITemplate.Infrastructure.Interfaces;
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

namespace UITemplate.Installers
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
            builder.Register<HudTimerCommand>(Lifetime.Scoped);
        }

        private static void RegisterUIViews(IContainerBuilder builder)
        {
            builder.Register<HudPresenter>(Lifetime.Scoped);
            builder.Register<StartingPopupPresenter>(Lifetime.Transient);
            builder.Register<SettingsPopupPresenter>(Lifetime.Transient);
            builder.Register<PromoPopupPresenter>(Lifetime.Transient);
            builder.Register<PromoInfoPopupPresenter>(Lifetime.Transient);
            builder.Register<StubPopupPresenter>(Lifetime.Transient);
            builder.Register<WelcomePopupPresenter>(Lifetime.Transient);
            builder.Register<SimpleMessagePopupPresenter>(Lifetime.Transient);
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
            builder.Register<GameData>(Lifetime.Scoped);
            builder.Register<GameManager>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
            builder.Register<SettingsData>(Lifetime.Scoped);
        }

        private static void RegisterUIManager(IContainerBuilder builder)
        {
            builder.Register<UIManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<IWindowFactory, WindowFactory>(Lifetime.Scoped);
            builder.Register<LevelFactory>(Lifetime.Scoped);
        }

        private static void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<IIncomeService, IncomeService>(Lifetime.Scoped);
            builder.Register<IUpgradeService, UpgradeService>(Lifetime.Scoped);
            builder.Register<ISettingsService, SettingsService>(Lifetime.Scoped);
            builder.Register<IWebService, WebService>(Lifetime.Scoped);
            builder.Register<IPrefabLoadService, PrefabLoadService>(Lifetime.Scoped);
            builder.Register<ISceneService, SceneService>(Lifetime.Scoped);
            builder.Register<IPersistenceService, PersistenceService>(Lifetime.Scoped);
            builder.Register<ITimerService, TimerService>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
        }
    }
}
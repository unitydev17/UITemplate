using UITemplate.Application;
using UITemplate.Application.Interfaces;
using UITemplate.Application.Services;
using UITemplate.Domain.Model;
using UITemplate.Infrastructure.Interfaces;
using UITemplate.Infrastructure.Services;
using UITemplate.Presentation;
using UITemplate.Presentation.MVP.Factory;
using VContainer;
using VContainer.Unity;

namespace UITemplate.Infrastructure.Installers
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<AppBoot>();
            builder.Register<AppModel>(Lifetime.Scoped);
            
            RegisterFactories(builder);
            RegisterServices(builder);
            RegisterUIManager(builder);
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
            builder.Register<ISettingsService, SettingsService>(Lifetime.Scoped);
            builder.Register<IWebService, WebService>(Lifetime.Scoped);
            builder.Register<IPrefabLoadService, PrefabLoadService>(Lifetime.Scoped);
        }
    }
}
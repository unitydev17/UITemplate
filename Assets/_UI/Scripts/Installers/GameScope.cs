using UITemplate.Application.Services;
using UITemplate.Domain.Model;
using UITemplate.Infrastructure.Services;
using UITemplate.Presentation;
using UITemplate.Presentation.Factories;
using VContainer;
using VContainer.Unity;

namespace UITemplate.Infrastructure.Installers
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterFactories(builder);
            RegisterServices(builder);

            builder.Register<AppModel>(Lifetime.Scoped);
            builder.RegisterEntryPoint<AppBoot>();
            builder.Register<UIManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private static void RegisterFactories(IContainerBuilder builder)
        {
            builder.Register<WindowFactory>(Lifetime.Scoped);
        }

        private static void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<ISettingsService, SettingsService>(Lifetime.Scoped);
            builder.Register<IWebService, WebService>(Lifetime.Scoped);
            builder.Register<IPrefabLoadService, PrefabLoadService>(Lifetime.Scoped);
        }
    }
}
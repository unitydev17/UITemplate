using UITemplate;
using UITemplate.Model;
using UITemplate.Presenter;
using UITemplate.View;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        RegisterStartingWindow(builder);
        RegisterChefPackWindow(builder);

        builder.RegisterEntryPoint<AppBoot>();
        builder.Register<UIManager>(Lifetime.Singleton);
    }

    private static void RegisterStartingWindow(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<StartingWindowView>();
        builder.Register<StartingWindowModel>(Lifetime.Singleton);
        builder.Register<StartingWindowPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }

    private static void RegisterChefPackWindow(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<ChefPackWindowView>();
        builder.Register<ChefPackWindowModel>(Lifetime.Singleton);
        builder.Register<ChefPackWindowPresenter>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }
}
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
        RegisterStartingPopup(builder);
        RegisterChefPackPopup(builder);
        RegisterChefPackInfoPopup(builder);

        builder.RegisterEntryPoint<AppBoot>();
        builder.Register<UIManager>(Lifetime.Singleton);
    }

    private static void RegisterStartingPopup(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<StartingPopupView>();
        builder.Register<StartingPopupModel>(Lifetime.Scoped);
        builder.Register<StartingPopupPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }

    private static void RegisterChefPackPopup(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<ChefPackPopupView>();
        builder.Register<ChefPackPopupModel>(Lifetime.Scoped);
        builder.Register<ChefPackPopupPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }

    private static void RegisterChefPackInfoPopup(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<ChefPackInfoPopupView>();
        builder.Register<ChefPackInfoPopupModel>(Lifetime.Scoped);
        builder.Register<ChefPackInfoPopupPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }
}
using UITemplate;
using UITemplate.Model;
using UITemplate.Model.Application;
using UITemplate.Model.Windows;
using UITemplate.Presenter;
using UITemplate.Presenter.Windows;
using UITemplate.Services;
using UITemplate.View;
using UITemplate.View.Windows;
using VContainer;
using VContainer.Unity;

public class GameScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<AppModel>(Lifetime.Scoped);
        builder.Register<BaseModel>(Lifetime.Scoped);

        builder.Register<ISettingsService, SettingsService>(Lifetime.Scoped);
        builder.Register<IWebService, WebService>(Lifetime.Scoped);


        RegisterHudWindow(builder);
        RegisterSettingsPopup(builder);

        RegisterStartingPopup(builder);
        RegisterChefPackPopup(builder);
        RegisterChefPackInfoPopup(builder);

        builder.RegisterEntryPoint<AppBoot>();
        builder.Register<UIManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
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
        builder.Register<ChefPackPopupPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }

    private static void RegisterChefPackInfoPopup(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<ChefPackInfoPopupView>();
        builder.Register<ChefPackInfoPopupPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }

    private static void RegisterHudWindow(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<HudView>();
        builder.Register<HudModel>(Lifetime.Scoped);
        builder.Register<HudPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }

    private static void RegisterSettingsPopup(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<SettingsPopupView>();
        builder.Register<SettingsPopupModel>(Lifetime.Scoped);
        builder.Register<SettingsPopupPresenter>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf();
    }
}
using JetBrains.Annotations;
using UITemplate.Presentation.Model.Windows;
using UITemplate.Presentation.Presenters.Common;
using UITemplate.View.Windows;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Presenter.Windows
{
    [UsedImplicitly]
    public class HudPresenter : WindowPresenter<HudView, HudModel>, IInitializable
    {
        public HudPresenter(HudView view, HudModel model) : base(view, model)
        {
        }

        public void Initialize()
        {
            Register(view.onSettingsBtnClick, () => MessageBroker.Default.Publish(new HudSettingsOpenEvent()));
        }
    }

    internal class HudSettingsOpenEvent
    {
    }
}
using JetBrains.Annotations;
using UITemplate.Presentation.Presenters.Common;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Presentation.Windows.Hud
{
    [UsedImplicitly]
    public class HudPresenter : WindowPresenter<HudView, HudModel>, IInitializable
    {
        public void Initialize()
        {
            Register(view.onSettingsBtnClick, () => MessageBroker.Default.Publish(new HudSettingsOpenEvent()));
        }
    }

    internal class HudSettingsOpenEvent
    {
    }
}
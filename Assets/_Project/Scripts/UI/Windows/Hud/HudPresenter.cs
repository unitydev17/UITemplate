using JetBrains.Annotations;
using UITemplate.Events;
using UITemplate.Presentation.MVP.Presenter;
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
            Register(MessageBroker.Default.Receive<UpdatePlayerDataEvent>(), UpdateIncome);
        }

        private void UpdateIncome(UpdatePlayerDataEvent data)
        {
            view.UpdateCoins(data.dto.money);
        }
    }

    internal class HudSettingsOpenEvent
    {
    }
}
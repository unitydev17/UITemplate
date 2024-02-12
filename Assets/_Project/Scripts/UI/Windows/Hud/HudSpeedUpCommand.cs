using JetBrains.Annotations;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Core.Controller.Command;
using UITemplate.Events;
using UniRx;

namespace UITemplate.Presentation.Windows.Hud
{
    [UsedImplicitly]
    public class HudSpeedUpCommand : TimerCommand
    {
        private readonly UpgradeCfg _cfg;
        private HudModel _model;
        private HudView _view;


        public HudSpeedUpCommand(UpgradeCfg cfg)
        {
            _cfg = cfg;
        }

        public void SetViewModel(HudView view, HudModel model)
        {
            _view = view;
            _model = model;
        }

        public override void Execute()
        {
            SetDuration(_cfg.speedUpDuration)
                .SetStartAction(OnStart)
                .SetProgressNotifier(OnUpdate)
                .SetFinishAction(OnFinish);

            base.Execute();
        }

        private void OnUpdate(int timeRemain, float progress)
        {
            _view.UpdateSpeedUpTimer(timeRemain, progress);
        }

        private void OnFinish()
        {
            _model.timerEnabled = false;
            MessageBroker.Default.Publish(new UISpeedUpRequestEvent(false));
            _view.SetSpeedButtonActive(false);
            _view.UpdateSpeedUpTimer((int) _cfg.speedUpDuration, 1);
        }

        private void OnStart()
        {
            _model.timerEnabled = true;
            MessageBroker.Default.Publish(new UISpeedUpRequestEvent(true));
            _view.SetSpeedButtonActive(true);
        }
    }
}
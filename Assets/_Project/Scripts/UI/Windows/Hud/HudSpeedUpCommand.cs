using JetBrains.Annotations;
using UITemplate.Application.ScriptableObjects;
using UITemplate.Core.Controller.Command;
using UITemplate.Events;
using UniRx;

namespace UITemplate.UI.Windows.Hud
{
    [UsedImplicitly]
    public class HudSpeedUpCommand : TimerCommand
    {
        private readonly UpgradeCfg _cfg;
        private HudModel _model;
        private HudView _view;

        private bool _skipStartNotification;

        public HudSpeedUpCommand(UpgradeCfg cfg)
        {
            _cfg = cfg;
        }

        public void SetInitData(HudView aView, HudModel aModel, bool skipStartNotification = false, float startTime = 0)
        {
            _view = aView;
            _model = aModel;
            _skipStartNotification = skipStartNotification;
            SetStartTime(startTime);
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
            _view.SetSpeedButtonActive(false);
            _view.UpdateSpeedUpTimer(_cfg.speedUpDuration, 1);
            
            MessageBroker.Default.Publish(new UISpeedUpRequestEvent(false));
        }

        protected virtual void OnStart()
        {
            _model.timerEnabled = true;
            _view.SetSpeedButtonActive(true);

            if (!_skipStartNotification) MessageBroker.Default.Publish(new UISpeedUpRequestEvent(true, _cfg.speedUpDuration));
        }
    }
}
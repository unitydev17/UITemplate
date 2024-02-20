using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Common.Events;
using UITemplate.UI.Command;
using UniRx;

namespace UITemplate.UI.Windows.Hud
{
    [UsedImplicitly]
    public class HudTimerCommand : TimerCommand
    {
        private readonly UpgradeCfg _cfg;
        private HudModel _model;
        private HudView _view;

        private bool _skipStartNotification;

        public HudTimerCommand(UpgradeCfg cfg)
        {
            _cfg = cfg;
        }

        public void SetInitData(HudView aView, HudModel aModel, bool skipStartNotification = false, float startTime = 0, float duration = 0)
        {
            _view = aView;
            _model = aModel;
            _skipStartNotification = skipStartNotification;
            SetStartTime(startTime);
            SetDuration(duration);
            UpdateView();
        }

        private void UpdateView()
        {
            var (timeRemain, progress) = GetStartProgress();
            _view.UpdateTimer(timeRemain, progress);
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
            _view.UpdateTimer(timeRemain, progress);
        }

        private void OnFinish()
        {
            _model.timerEnabled = false;
            _view.SetSpeedButtonActive(false);
            _view.UpdateTimer(_cfg.speedUpDuration, 1);

            MessageBroker.Default.Publish(new StopTimerEvent());
        }

        protected virtual void OnStart()
        {
            _model.timerEnabled = true;
            _view.SetSpeedButtonActive(true);

            if (!_skipStartNotification) MessageBroker.Default.Publish(new StartTimerEvent(true, _cfg.speedUpDuration));
        }
    }
}
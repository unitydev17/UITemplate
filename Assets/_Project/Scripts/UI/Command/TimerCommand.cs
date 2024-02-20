using System;
using JetBrains.Annotations;
using UITemplate.Common;
using UITemplate.Utils;
using UniRx;
using UnityEngine;

namespace UITemplate.UI.Command
{
    [UsedImplicitly]
    public class TimerCommand : Registrable, ICommand
    {
        private IDisposable _tempDisposable;
        private float _duration;
        private Action<int, float> _progressNotifier;
        private Action _finishAction;
        private Action _startAction;
        private float _startTime;

        protected void SetStartTime(float time)
        {
            _startTime = time;
        }

        protected TimerCommand SetDuration(float time)
        {
            _duration = time;
            return this;
        }

        public TimerCommand SetStartAction(Action action)
        {
            _startAction = action;
            return this;
        }

        public void SetFinishAction(Action action)
        {
            _finishAction = action;
        }

        public TimerCommand SetProgressNotifier(Action<int, float> notifier)
        {
            _progressNotifier = notifier;
            return this;
        }

        public virtual void Execute()
        {
            _startAction?.Invoke();

            var timer = _startTime;

            _tempDisposable = RegisterTemporary(Observable.EveryUpdate(), () =>
            {
                timer += Time.deltaTime;

                if (timer > _duration)
                {
                    StopTimer();
                    return;
                }

                var progress = 1 - timer / _duration;
                var timeRemain = (int) (_duration - timer);

                _progressNotifier?.Invoke(timeRemain, progress);
            });
        }

        protected (int, float) GetStartProgress()
        {
            return GetProgress(_startTime, _duration);
        }


        public static (int, float) GetProgress(float startTime, float duration)
        {
            var currentProgress = 1f - startTime / duration;
            return ((int) (duration - startTime), currentProgress);
        }

        private void StopTimer()
        {
            _finishAction?.Invoke();
            Dispose(_tempDisposable);
        }

        public void ForceStopTimer()
        {
            if (_tempDisposable == null) return;
            Dispose(_tempDisposable);
        }
    }
}
using System;
using JetBrains.Annotations;
using UITemplate.Common.Events;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.Interfaces;
using UITemplate.Utils;
using UniRx;
using VContainer.Unity;

namespace UITemplate.Core.Services
{
    [UsedImplicitly]
    public class TimerService : Registrable, ITimerService, IInitializable
    {
        private readonly PlayerData _playerData;

        public TimerService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<StartTimerEvent>(), StartTimerEventHandler);
            Register(MessageBroker.Default.Receive<StopTimerEvent>(), StopTimerEventHandler);
        }

        private void StartTimerEventHandler(StartTimerEvent data)
        {
            _playerData.timer.active = data.enable;
            if (!_playerData.timer.active) return;

            _playerData.timer.startTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            _playerData.timer.duration = data.duration;
        }

        private void StopTimerEventHandler()
        {
            _playerData.timer.active = false;
        }

        public double CountTimePassed(out double speedTime, out double normalTime)
        {
            var now = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            var passiveTime = now - _playerData.timer.gameExitTime;

            speedTime = 0;
            normalTime = passiveTime;


            // if Timer was paused, no income should be earned

            if (_playerData.timer.active && _playerData.timer.paused)
            {
                speedTime = 0;
                normalTime = 0;
                return passiveTime;
            }

            // returns speedUp/normal time only in case speedUp was started

            if (!_playerData.timer.active) return passiveTime;


            // timer was active and not paused, so it is required to count normal time as well as speed up one

            var timerTargetFinishTime = _playerData.timer.startTime + _playerData.timer.duration;
            if (now >= timerTargetFinishTime) _playerData.timer.active = false;


            var minStopSpeedTime = Math.Min(timerTargetFinishTime, now);
            speedTime = minStopSpeedTime - _playerData.timer.gameExitTime;
            normalTime = Math.Abs(passiveTime - speedTime);

            return passiveTime;
        }

        public void UnPause()
        {
            if (!_playerData.timer.paused) return;
            if (!_playerData.timer.active) return;

            _playerData.timer.paused = false;

            var deltaPause = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds - _playerData.timer.pauseTime;
            _playerData.timer.startTime += deltaPause;
        }

        public void PauseOnReturnToGame()
        {
            Pause();

            var deltaPause = _playerData.timer.paused ? _playerData.timer.pauseTime - _playerData.timer.gameExitTime : 0;
            _playerData.elapsedTime = _playerData.timer.active ? _playerData.timer.gameExitTime + deltaPause - _playerData.timer.startTime : 0;
        }

        public void Pause()
        {
            if (!_playerData.timer.active) return;

            _playerData.timer.pauseTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            _playerData.timer.paused = true;
        }
    }
}
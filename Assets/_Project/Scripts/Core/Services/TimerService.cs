using System;
using UITemplate.Common.Events;
using UITemplate.Core.DomainEntities;
using UITemplate.Core.Interfaces;
using UITemplate.Utils;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace UITemplate.Core.Services
{
    public class TimerService : Registrable, ITimerService, IInitializable
    {
        private readonly PlayerData _playerData;

        public TimerService(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void Initialize()
        {
            Register(MessageBroker.Default.Receive<SpeedUpRequestEvent>(), SpeedUpRequestEventHandler);
        }

        private void SpeedUpRequestEventHandler(SpeedUpRequestEvent data)
        {
            _playerData.timer.speedUp = data.enable;
            if (_playerData.timer.speedUp == false) return;

            _playerData.timer.speedUpStartTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            _playerData.timer.speedUpDuration = data.duration;
        }

        public void PauseSpeedUpTimer()
        {
            if (!_playerData.timer.speedUp) return;

            _playerData.timer.timerPauseTime = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            _playerData.timer.timerPaused = true;

            
            
            Debug.Log( "   timerPauseTime" + _playerData.timer.timerPauseTime + "   deltatime=" + (_playerData.timer.speedUpStartTime - _playerData.timer.timerPauseTime));
            _playerData.timer.speedUpStartTime = _playerData.timer.timerPauseTime;
        }

        public double CountTimePassed(out double speedUpTime, out double normalTime)
        {
            var now = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
            var passiveTime = now - _playerData.timer.gameExitTime;

            speedUpTime = 0;
            normalTime = passiveTime;


            if (_playerData.timer.speedUp)
            {
                var speedUpTargetStopTime = Math.Min(_playerData.timer.speedUpStartTime + _playerData.timer.speedUpDuration, now);
                CheckStopSpeedUp(now, speedUpTargetStopTime);

                speedUpTime = speedUpTargetStopTime - _playerData.timer.gameExitTime;
                normalTime = Math.Abs(passiveTime - speedUpTime);
            }

            return passiveTime;
        }

        private void CheckStopSpeedUp(double now, double rightBorder)
        {
            if (now - rightBorder >= 0) _playerData.timer.speedUp = false;
        }
    }
}
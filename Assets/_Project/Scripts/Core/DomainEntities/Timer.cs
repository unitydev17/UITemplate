using System;
using UITemplate.Core.Interfaces;

namespace UITemplate.Core.DomainEntities
{
    [Serializable]
    public class Timer : ICopyable<Timer>
    {
        public bool speedUp;
        public double speedUpStartTime;
        public float speedUpDuration;
        public double gameExitTime;
        public double timerPauseTime;
        public bool timerPaused;
        
        public void CopyFrom(Timer data)
        {
            speedUp = data.speedUp;
            speedUpStartTime = data.speedUpStartTime;
            speedUpDuration = data.speedUpDuration;
            gameExitTime = data.gameExitTime;
            timerPauseTime = data.timerPauseTime;
            timerPaused = data.timerPaused;
        }
    }
}
using System;
using UITemplate.Core.Interfaces;

namespace UITemplate.Core.DomainEntities
{
    [Serializable]
    public class Timer : ICopyable<Timer>
    {
        public bool active;
        public double startTime;
        public float duration;
        public double gameExitTime;
        public double pauseTime;
        public bool paused;

        public void CopyFrom(Timer data)
        {
            active = data.active;
            startTime = data.startTime;
            duration = data.duration;
            gameExitTime = data.gameExitTime;
            pauseTime = data.pauseTime;
            paused = data.paused;
        }
    }
}
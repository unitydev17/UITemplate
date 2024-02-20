namespace UITemplate.Core.Interfaces
{
    public interface ITimerService
    {
        public void Pause();

        public double CountTimePassed(out double speedTime, out double normalTime);
        void UnPause();
        void PauseOnReturnToGame();
    }
}
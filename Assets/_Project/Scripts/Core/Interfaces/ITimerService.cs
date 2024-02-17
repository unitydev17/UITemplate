namespace UITemplate.Core.Interfaces
{
    public interface ITimerService
    {
        public void PauseSpeedUpTimer();

        public double CountTimePassed(out double speedUpTime, out double normalTime);
    }
}
namespace UITemplate.Common.Events
{
    public class SpeedUpRequestEvent
    {
        public bool enable { get; }
        public float duration { get; }

        public SpeedUpRequestEvent(bool enable, float duration)
        {
            this.enable = enable;
            this.duration = duration;
        }

        public SpeedUpRequestEvent(bool enable)
        {
            this.enable = enable;
        }
    }
}
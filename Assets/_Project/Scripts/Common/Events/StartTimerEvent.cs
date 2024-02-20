namespace UITemplate.Common.Events
{
    public class StartTimerEvent
    {
        public bool enable { get; }
        public float duration { get; }

        public StartTimerEvent(bool enable, float duration)
        {
            this.enable = enable;
            this.duration = duration;
        }
    }
}
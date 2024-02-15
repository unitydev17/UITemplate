namespace UITemplate.Common.Events
{
    public class UISpeedUpRequestEvent
    {
        public bool enable { get; }
        public float duration { get; }

        public UISpeedUpRequestEvent(bool enable, float duration)
        {
            this.enable = enable;
            this.duration = duration;
        }

        public UISpeedUpRequestEvent(bool enable)
        {
            this.enable = enable;
        }
    }
}
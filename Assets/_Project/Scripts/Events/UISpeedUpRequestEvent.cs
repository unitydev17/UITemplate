namespace UITemplate.Events
{
    public class UISpeedUpRequestEvent
    {
        public bool enable { get; }

        public UISpeedUpRequestEvent(bool enable)
        {
            this.enable = enable;
        }
    }
}
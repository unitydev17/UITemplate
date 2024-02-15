namespace UITemplate.Common.Events
{
    public class UpgradeRequestEvent
    {
        public int id { get; }

        public UpgradeRequestEvent(int id)
        {
            this.id = id;
        }
    }
}
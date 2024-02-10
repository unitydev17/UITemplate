namespace UITemplate.Events
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
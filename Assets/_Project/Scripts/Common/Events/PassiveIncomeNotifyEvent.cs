namespace UITemplate.Common.Events
{
    public class PassiveIncomeNotifyEvent
    {
        public int sum { get; }
        public double time { get; }

        public PassiveIncomeNotifyEvent(int sum, double time)
        {
            this.sum = sum;
            this.time = time;
        }
    }
}
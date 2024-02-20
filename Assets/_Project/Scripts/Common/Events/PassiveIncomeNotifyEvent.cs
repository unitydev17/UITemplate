using UITemplate.Common.Dto;

namespace UITemplate.Common.Events
{
    public class PassiveIncomeNotifyEvent
    {
        public PassiveIncomeNotifyEvent(PlayerDto dto)
        {
            this.dto = dto;
        }

        public PlayerDto dto { get; }
    }
}
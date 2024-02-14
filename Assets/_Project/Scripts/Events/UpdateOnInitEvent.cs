using UITemplate.Common.Dto;

namespace UITemplate.Events
{
    public class UpdateOnInitEvent
    {
        public UpdateOnInitEvent(PlayerDto dto)
        {
            this.dto = dto;
        }

        public PlayerDto dto { get; }
    }
}
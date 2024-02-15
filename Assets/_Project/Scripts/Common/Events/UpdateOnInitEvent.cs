using UITemplate.Common.Dto;

namespace UITemplate.Common.Events
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
using UITemplate.Common.Dto;

namespace UITemplate.Common.Events
{
    public class UpdatePlayerDataEvent
    {
        public UpdatePlayerDataEvent(PlayerDto dto)
        {
            this.dto = dto;
        }

        public PlayerDto dto { get; }
    }
}
using UITemplate.Common.Dto;

namespace UITemplate.Events
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
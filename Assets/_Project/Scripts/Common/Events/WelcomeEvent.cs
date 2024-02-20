using UITemplate.Common.Dto;

namespace UITemplate.Common.Events
{
    public class WelcomeEvent
    {
        public WelcomeEvent(PlayerDto dto)
        {
            this.dto = dto;
        }

        public PlayerDto dto { get; }
    }
}
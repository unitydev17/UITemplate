using UITemplate.Common.Dto;

namespace UITemplate.Events
{
    public class UpgradeResponseEvent
    {
        public UpgradeResponseEvent(BuildingDto dto)
        {
            this.dto = dto;
        }

        public BuildingDto dto { get; }
    }
}
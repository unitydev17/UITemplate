using UITemplate.Common.Dto;

namespace UITemplate.Common.Events
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
using UITemplate.Common.Dto;

namespace UITemplate.Common.Events
{
    public class IncomeEvent
    {
        public IncomeEvent(BuildingDto dto)
        {
            this.dto = dto;
        }

        public BuildingDto dto { get; }
    }
}
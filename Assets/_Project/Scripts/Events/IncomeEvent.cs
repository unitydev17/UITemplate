using UITemplate.Common.Dto;

namespace UITemplate.Events
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
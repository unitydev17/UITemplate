using UITemplate.Common.Dto;

namespace UITemplate.Core.Controller
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
using UITemplate.Common.Dto;
using UITemplate.Core.Entities;

namespace UITemplate.Core
{
    public class BuildingDtoMapper
    {
        public static BuildingDto GetDto(Building building)
        {
            return new BuildingDto
            {
                id = building.id,
                currentIncome = building.currentIncome,
                nextUpgradeCost = building.nextUpgradeCost,
                level = building.level,
                upgradeCompletion = building.upgradeCompletion,
                incomeCompletion = building.incomeCompletion
            };
        }
    }
}
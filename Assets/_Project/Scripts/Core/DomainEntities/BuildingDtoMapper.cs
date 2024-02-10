using UITemplate.Common.Dto;
using UITemplate.Core.Entities;

namespace UITemplate.Core
{
    public static class BuildingDtoMapper
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

        public static PlayerDto ToDto(this PlayerData playerData)
        {
            return new PlayerDto
            {
                money = playerData.money
            };
        }

        public static BuildingDto ToDto(this Building building)
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
using System.Collections.Generic;
using System.Linq;
using UITemplate.Common.Dto;

namespace UITemplate.Core.DomainEntities.Mappers
{
    public static class BuildingDtoMapper
    {
        public static BuildingDto GetDto(Building building)
        {
            return building.ToDto();
        }

        public static PlayerDto ToDto(this PlayerData playerData)
        {
            return new PlayerDto
            {
                money = playerData.money,
                speedUp = playerData.speedUp,
                speedUpDuration = playerData.speedUpDuration,
                speedUpStartTime = playerData.speedUpStartTime
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
                upgradeProgress = building.upgradeProgress,
                incomeProgress = building.incomeProgress,
                nextDeltaIncome = building.nextDeltaIncome,
                incomeReceived =  building.incomeReceived
            };
        }

        public static List<Building> ToEntityList(IEnumerable<BuildingDto> buildingDtoList)
        {
            return buildingDtoList.Select(ToEntity).ToList();
        }

        private static Building ToEntity(BuildingDto dto)
        {
            return new Building
            {
                id = dto.id,
                incomePerSecond = dto.incomePerSecond
            };
        }
    }
}
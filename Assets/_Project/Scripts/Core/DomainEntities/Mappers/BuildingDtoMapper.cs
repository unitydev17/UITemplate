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
                timer = playerData.timer.ToDto()
            };
        }

        private static TimerDto ToDto(this Timer timer)
        {
            return new TimerDto
            {
                speedUp = timer.speedUp,
                speedUpDuration = timer.speedUpDuration,
                speedUpStartTime = timer.speedUpStartTime,
                gameExitTime = timer.gameExitTime,
                timerPauseTime = timer.timerPauseTime,
                timerPaused = timer.timerPaused
            };
        }

        public static BuildingDto ToDto(this Building building)
        {
            return new BuildingDto
            {
                id = building.id,
                currentIncome = building.currentIncome,
                nextUpgradeCost = building.nextUpgradeCost,
                upgradeLevel = building.upgradeLevel,
                upgradeProgress = building.upgradeProgress,
                incomeProgress = building.incomeProgress,
                nextDeltaIncome = building.nextDeltaIncome,
                incomeReceived = building.incomeReceived
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
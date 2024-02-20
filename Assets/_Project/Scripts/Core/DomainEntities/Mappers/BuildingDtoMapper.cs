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
                timer = playerData.timer.ToDto(),
                passiveIncome = playerData.passiveIncome,
                passiveTime = playerData.passiveTime,
                elapsedTime = playerData.elapsedTime,
                levelCompleted = playerData.levelCompleted
            };
        }

        private static TimerDto ToDto(this Timer timer)
        {
            return new TimerDto
            {
                active = timer.active,
                duration = timer.duration,
                startTime = timer.startTime,
                gameExitTime = timer.gameExitTime,
                timerPauseTime = timer.pauseTime,
                timerPaused = timer.paused
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
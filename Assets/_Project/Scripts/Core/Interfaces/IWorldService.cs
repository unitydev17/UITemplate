using System.Collections.Generic;
using UITemplate.Common.Dto;
using UITemplate.Core.Entities;

namespace UITemplate.Core.Interfaces
{
    public interface IWorldService
    {
        public List<Building> FetchBuildingsFromScene();
        void UpdateBuildingViews(IEnumerable<BuildingDto> dtoList);
    }
}
using System.Collections.Generic;
using UITemplate.Common.Dto;
using UITemplate.Core.DomainEntities;

namespace UITemplate.Core.Interfaces
{
    public interface ISceneService
    {
        public List<Building> FetchBuildingsFromScene();
        void UpdateBuildingViews(IEnumerable<BuildingDto> dtoList);
    }
}
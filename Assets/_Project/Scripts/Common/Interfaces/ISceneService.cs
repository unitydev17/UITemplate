using System.Collections.Generic;
using UITemplate.Common.Dto;

namespace UITemplate.Common.Interfaces
{
    public interface ISceneService
    {
        public IEnumerable<BuildingDto> FetchBuildingsFromScene();
        void UpdateBuildingViews(IEnumerable<BuildingDto> dtoList);
    }
}
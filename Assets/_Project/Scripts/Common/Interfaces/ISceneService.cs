using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UITemplate.Common.Dto;

namespace UITemplate.Common.Interfaces
{
    public interface ISceneService
    {
        public IEnumerable<BuildingDto> FetchBuildingsFromScene();
        void UpdateBuildingViews(IEnumerable<BuildingDto> dtoList, bool immediate = false);
        UniTask LoadLevel(int index);
    }
}
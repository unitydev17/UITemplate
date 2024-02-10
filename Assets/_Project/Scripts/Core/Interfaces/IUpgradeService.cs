using UITemplate.Core.Entities;

namespace UITemplate.Core.Interfaces
{
    public interface IUpgradeService
    {
        public void UpdateBuildingValues(ref Building building);
        public bool TryUpgrade(ref Building building);
        
    }
}
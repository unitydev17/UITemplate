using UITemplate.Core.DomainEntities;

namespace UITemplate.Core.Interfaces
{
    public interface IUpgradeService
    {
        public void UpdateBuildingsInfo();
        public bool TryUpgrade(ref Building building);
        
    }
}
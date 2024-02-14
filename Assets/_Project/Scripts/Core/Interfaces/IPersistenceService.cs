using System.Collections.Generic;
using UITemplate.Core.DomainEntities;

namespace UITemplate.Core.Interfaces
{
    public interface IPersistenceService
    {
        void SaveGameState(IEnumerable<Building> buildings);
    }
}
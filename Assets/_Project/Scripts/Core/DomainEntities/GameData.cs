using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UITemplate.Core.Interfaces;

namespace UITemplate.Core.DomainEntities
{
    [UsedImplicitly]
    public class GameData : ICopyable<GameData>
    {
        public List<Building> buildings = new List<Building>();


        public void CopyFrom(GameData data)
        {
            foreach (var copyable in data.buildings)
            {
                foreach (var b in buildings.Where(b => b.id == copyable.id))
                {
                    b.CopyFrom(copyable);
                }
            }
        }
    }
}
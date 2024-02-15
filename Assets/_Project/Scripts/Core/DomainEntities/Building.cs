using System;
using UITemplate.Core.Interfaces;

namespace UITemplate.Core.DomainEntities
{
    [Serializable]
    public class Building : ICopyable<Building>
    {
        public int id;
        public int level;
        public int currentIncome;
        public int nextDeltaIncome;
        public int nextUpgradeCost;
        public float upgradeProgress;
        public float incomeProgress;
        public float incomePerSecond;
        public float incomeMultiplier;

        public void CopyFrom(Building data)
        {
            level = data.level;
            incomeProgress = data.incomeProgress;
        }
    }
}
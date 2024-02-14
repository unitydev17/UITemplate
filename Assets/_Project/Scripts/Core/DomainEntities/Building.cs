using System;

namespace UITemplate.Core.DomainEntities
{
    [Serializable]
    public class Building
    {
        public int id;
        public int level;
        public int currentIncome;
        public int nextDeltaIncome;
        public int nextUpgradeCost;
        public float upgradeCompletion;
        public float incomeCompletion;
        public float incomeSpeed;
        public float incomeMultiplier;
    }
}
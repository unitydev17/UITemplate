using System;
using UITemplate.Core.Interfaces;

namespace UITemplate.Core.DomainEntities
{
    [Serializable]
    public class Building : ICopyable<Building>
    {
        public int id;
        public int upgradeLevel;
        public float incomeProgress;

        [NonSerialized] public int currentIncome;
        [NonSerialized] public int nextDeltaIncome;
        [NonSerialized] public int nextUpgradeCost;
        [NonSerialized] public float upgradeProgress;
        [NonSerialized] public float incomePerSecond;
        [NonSerialized] public float incomeMultiplier;
        [NonSerialized] public bool incomeReceived;

        public void CopyFrom(Building data)
        {
            upgradeLevel = data.upgradeLevel;
            incomeProgress = data.incomeProgress;
        }
    }
}
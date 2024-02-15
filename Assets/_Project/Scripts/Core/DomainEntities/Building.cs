using System;
using UITemplate.Core.Interfaces;
using UnityEngine.Serialization;

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
        public float upgradeCompletion;
        public float incomeCompletion;
        [FormerlySerializedAs("incomeSpeed")] public float incomePerSecond;
        public float incomeMultiplier;

        public void CopyFrom(Building data)
        {
            level = data.level;
        }
    }
}
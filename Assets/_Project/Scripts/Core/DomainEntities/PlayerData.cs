using System;
using JetBrains.Annotations;

namespace UITemplate.Core.DomainEntities
{
    [UsedImplicitly]
    public class PlayerData
    {
        public int levelIndex;
        public int money;
        public Timer timer;

        [NonSerialized] public int passiveIncome;
        [NonSerialized] public double passiveTime;
        [NonSerialized] public bool levelCompleted;

        public PlayerData()
        {
            timer = new Timer();
        }

        public void CopyFrom(PlayerData data)
        {
            levelIndex = data.levelIndex;
            money = data.money;
            levelCompleted = data.levelCompleted;

            timer.CopyFrom(data.timer);
        }
    }
}
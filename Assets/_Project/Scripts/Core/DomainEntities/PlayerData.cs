using System;
using JetBrains.Annotations;

namespace UITemplate.Core.DomainEntities
{
    [UsedImplicitly]
    public class PlayerData
    {
        public int levelIndex;
        public float money;
        public Timer timer;
        public bool levelCompleted;

        [NonSerialized] public int passiveIncome;
        [NonSerialized] public double passiveTime;
        [NonSerialized] public bool countingEnabled;
        [NonSerialized] public double elapsedTime;

        public PlayerData()
        {
            timer = new Timer();
        }


        public void CopyFrom(PlayerData data)
        {
            levelIndex = data.levelIndex;
            money = data.money;
            countingEnabled = data.countingEnabled;
            levelCompleted = data.levelCompleted;

            timer.CopyFrom(data.timer);
        }
    }
}
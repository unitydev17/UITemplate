using JetBrains.Annotations;

namespace UITemplate.Core.DomainEntities
{
    [UsedImplicitly]
    public class PlayerData
    {
        public int money;
        public bool speedUp;
        public double speedUpStartTime;
        public float speedUpDuration;
        public double gameExitTime;
        public int passiveIncome;
        public double passiveTime;

        public void CopyFrom(PlayerData data)
        {
            money = data.money;
            speedUp = data.speedUp;
            speedUpStartTime = data.speedUpStartTime;
            speedUpDuration = data.speedUpDuration;
            gameExitTime = data.gameExitTime;
        }
    }
}
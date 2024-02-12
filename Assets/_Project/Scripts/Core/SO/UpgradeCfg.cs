using System;
using UnityEngine;

namespace UITemplate.Application.ScriptableObjects
{
    [CreateAssetMenu(fileName = "UpgradeCfg")]
    public class UpgradeCfg : ScriptableObject
    {
        public string costs;
        public string incomes;
        public string incomeMultiplier;
        public float globalSpeedMultiplier;
        public float speedUpDuration;

        public int upgradesCount => GetSize(costs);
        public int GetCost(in int upgradeLevel) => GetValue<int>(costs, upgradeLevel, upgradesCount - 1);
        public int GetIncome(in int upgradeLevel) => GetValue<int>(incomes, upgradeLevel, upgradesCount);

        public float GetIncomeMultiplier(in int upgradeLevel) => GetValue<float>(incomeMultiplier, upgradeLevel, upgradesCount);

        private T GetValue<T>(in string str, in int index, int maxIndex)
        {
            var strValue = str.Split(",")[Math.Min(index, maxIndex)];

            if (typeof(T) == typeof(int)) return (T) (object) Convert.ToInt32(strValue);

            if (typeof(T) == typeof(float)) return (T) (object) Convert.ToSingle(strValue);

            throw new Exception($"Can not convert value {str}");
        }


        private static int GetSize(in string str)
        {
            return str.Split(",").Length;
        }
    }
}
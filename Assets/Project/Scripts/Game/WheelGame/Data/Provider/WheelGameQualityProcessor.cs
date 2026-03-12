using Project.Scripts.Game.WheelGame.Data.Item;
using UnityEngine;

namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    public class WheelGameQualityProcessor : IQualityProgressCalculator
    {
        public ItemQuality CalculateQuality(int currentIndex)
        {
            int qualityLevel = currentIndex / 5;

            int maxValue = System.Enum.GetValues(typeof(ItemQuality))
                .Length - 1;

            qualityLevel = Mathf.Clamp(qualityLevel, 0, maxValue);

            return (ItemQuality)qualityLevel;
        }
    }
}
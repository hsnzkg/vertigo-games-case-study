using Project.Scripts.Game.WheelGame.Data.Item;

namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    public interface IQualityProgressCalculator
    {
        public ItemQuality CalculateQuality(int currentIndex);
    }
}

using Project.Scripts.Game.WheelGame.Data.Item;

namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    public struct WheelItemResult
    {
        public IWheelItem Item { get; }
        public int Amount { get; }

        public WheelItemResult(IWheelItem item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
}
using Project.Scripts.UI.Wheel.Data.Item;

namespace Project.Scripts.UI.Wheel.Data.Provider
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
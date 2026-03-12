using EventBus.Runtime;
using Project.Scripts.Game.WheelGame.Data.Item;

namespace Project.Scripts.EventBus.Events.Bag
{
    public struct EAddItem : IEvent
    {
        public readonly IWheelItem Item;
        public int Amount { get; }

        public EAddItem(IWheelItem item, int amount)
        {
            Item = item;
            Amount = amount;
        }
    }
}

using Project.Scripts.Game.WheelGame.Data.Item;

namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    public interface IWheelItemCollectionProvider
    {
        public WheelItemResult[] Provide(WheelZoneType zoneType, ItemQuality targetQuality = ItemQuality.Common, int seed = -1);
    }
}
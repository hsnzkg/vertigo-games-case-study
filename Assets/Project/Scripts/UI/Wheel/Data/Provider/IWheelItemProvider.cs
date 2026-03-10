namespace Project.Scripts.UI.Wheel.Data.Provider
{
    public interface IWheelItemCollectionProvider
    {
        public WheelItemResult[] Provide(WheelZoneType zoneType, int seed = -1);
    }
}
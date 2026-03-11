namespace Project.Scripts.Game.WheelGame.Data.Provider
{
    public interface IWheelItemCollectionProvider
    {
        public WheelItemResult[] Provide(WheelZoneType zoneType, int seed = -1);
    }
}
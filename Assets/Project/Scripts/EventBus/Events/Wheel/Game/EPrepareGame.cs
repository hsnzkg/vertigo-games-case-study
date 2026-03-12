using EventBus.Runtime;
using Project.Scripts.Game.WheelGame.Data.Provider;

namespace Project.Scripts.EventBus.Events.Wheel.Game
{
    public struct EPrepareGame : IEvent
    {
        public readonly WheelItemResult[] Result;
        public readonly WheelZoneType ZoneType;
        public readonly int ZoneIndex;

        public EPrepareGame(WheelItemResult[] wheelBag, WheelZoneType zoneType,int zoneIndex)
        {
            Result = wheelBag;
            ZoneType = zoneType;
            ZoneIndex = zoneIndex;
        }
    }
}
using EventBus.Runtime;
using Project.Scripts.Game.WheelGame.Data.Provider;

namespace Project.Scripts.EventBus.Events.Wheel.Game
{
    public struct EPrepareGame : IEvent
    {
        public readonly WheelItemResult[] Result;
        public readonly WheelZoneType ZoneType;

        public EPrepareGame(WheelItemResult[] wheelBag, WheelZoneType zoneType)
        {
            Result = wheelBag;
            ZoneType = zoneType;
        }
    }
}
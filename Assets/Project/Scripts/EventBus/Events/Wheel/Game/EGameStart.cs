using EventBus.Runtime;

namespace Project.Scripts.EventBus.Events.Wheel.Game
{
    public struct EGameStart : IEvent
    {
        public readonly int Index;
        public EGameStart(int selectedIndex)
        {
            Index = selectedIndex;
        }
    }
}
using EventBus.Runtime;

namespace Project.Scripts.EventBus.Events.MonoBehaviour
{
    public struct EUpdate : IEvent
    {
        public readonly float Delta;

        public EUpdate(float delta)
        {
            Delta = delta;
        }
    }
}
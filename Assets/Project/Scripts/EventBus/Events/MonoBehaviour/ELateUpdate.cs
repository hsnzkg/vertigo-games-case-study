using EventBus.Runtime;

namespace Project.Scripts.EventBus.Events.MonoBehaviour
{
    public struct ELateUpdate : IEvent
    {
        public readonly float Delta;

        public ELateUpdate(float delta)
        {
            Delta = delta;
        }
    }
}
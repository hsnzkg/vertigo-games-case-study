using EventBus.Runtime;

namespace Project.Scripts.EventBus.Events.MonoBehaviour
{
    public struct EFixedUpdate : IEvent
    {
        public readonly float Delta;

        public EFixedUpdate(float delta)
        {
            Delta = delta;
        }
    }
}
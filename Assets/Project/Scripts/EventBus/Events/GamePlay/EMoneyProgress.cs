using EventBus.Runtime;

namespace Project.Scripts.EventBus.Events.GamePlay
{
    public struct EMoneyProgress : IEvent
    {
        public int CurrentMoneyValue;

        public EMoneyProgress(int currentMoneyValue)
        {
            CurrentMoneyValue = currentMoneyValue;
        }
    }
}

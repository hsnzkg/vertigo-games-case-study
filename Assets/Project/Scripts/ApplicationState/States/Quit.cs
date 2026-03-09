using EventBus.Runtime;
using FiniteStateMachine.Runtime;
using Storage.Runtime;

namespace Project.Scripts.ApplicationState.States
{
    public class Quit : StateBase
    {
        protected override void OnEnter()
        {
            EventBusCenter.DisposeAllBuses();
            StorageCenter.DisposeAllStorages();
        }
    }
}
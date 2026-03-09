using FiniteStateMachine.Runtime;

namespace Project.Scripts.ApplicationState.States
{
    public class Preload : StateBase
    {
        protected override void OnEnter()
        {
            Initialize();
            ApplicationStateManager.RequestStateChange<Gameplay>();
        }

        private void Initialize()
        {
        }
    }
}
namespace Project.Scripts.ApplicationState
{
    public interface IApplicationStateManager
    {
        void Initialize();
        void RequestStateChange<T>();
    }
}
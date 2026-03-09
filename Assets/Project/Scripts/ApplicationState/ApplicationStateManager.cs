namespace Project.Scripts.ApplicationState
{
    public static class ApplicationStateManager
    {
        private static IApplicationStateManager s_instance;
        public static IApplicationStateManager Instance => s_instance;

        public static void Set(IApplicationStateManager manager)
        {
            s_instance = manager;
        }

        public static void Initialize()
        {
            s_instance.Initialize();
        }

        public static void RequestStateChange<T>()
        {
            s_instance.RequestStateChange<T>();
        }
    }
}
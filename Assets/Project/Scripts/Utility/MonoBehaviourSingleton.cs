using UnityEngine;

namespace Project.Scripts.Utility
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T s_instance;
        private static bool s_isInitialized;
        private static string LogTag => $"[{typeof(T).Name}]";
        public static bool HasInstance => s_instance != null;

        public static T Instance
        {
            get
            {
                if (s_instance != null) return s_instance;
                s_instance = FindFirstObjectByType<T>();
                if (s_instance != null) return s_instance;
                GameObject go = new(typeof(T).Name);
                s_instance = go.AddComponent<T>();
                return s_instance;
            }
        }

        public static void Create()
        {
            if (s_isInitialized)
            {
                LogWarning("Initialize called but already initialized.");
                return;
            }

            _ = Instance;
            s_isInitialized = true;
        }

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = (T)this;
                OnAwake();
            }
            else if (s_instance != this)
            {
                LogWarning("Duplicate instance detected and destroyed.");
                OnDuplicateDestroyed();
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            OnEnabled();
        }

        private void OnDisable()
        {
            OnDisabled();
        }

        private void OnDestroy()
        {
            if (s_instance != this) return;
            s_instance = null;
            s_isInitialized = false;
            OnDestroyed();
        }

        protected void MakePersistent()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnEnabled()
        {
        }

        protected virtual void OnDisabled()
        {
        }

        protected virtual void OnDestroyed()
        {
        }

        protected virtual void OnDuplicateDestroyed()
        {
        }

        protected static void Log(string message)
        {
            Debug.Log($"{LogTag} {message}");
        }

        protected static void LogWarning(string message)
        {
            Debug.LogWarning($"{LogTag} {message}");
        }

        protected static void LogError(string message)
        {
            Debug.LogError($"{LogTag} {message}");
        }
    }
}
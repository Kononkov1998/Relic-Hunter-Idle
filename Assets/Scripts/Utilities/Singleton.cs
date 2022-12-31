using UnityEngine;

namespace Utilities
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                T[] instances = FindObjectsOfType<T>(true);

                if (instances.Length == 0)
                {
                    Debug.LogError($"No instance of type {typeof(T)} found!");
                    return null;
                }

                if (instances.Length > 1)
                {
                    Debug.LogError($"Found more than one instance of type {typeof(T)}");
                    return null;
                }

                _instance = instances[0];
                _instance.Initialize();
                return _instance;
            }
        }

        public static bool IsInstanceExist => _instance != null;

        private void Awake()
        {
            if (_instance != this && _instance != null)
            {
                Destroy(this);
            }
            else if (_instance == null)
            {
                var instances = (T[]) FindObjectsOfType(typeof(T));

                if (instances.Length == 0)
                    Debug.LogError($"No instance of type {typeof(T)} found!");
                else if (instances.Length > 1)
                    Debug.LogError($"Found more than one instance of type {typeof(T)}");
                else
                    _instance = instances[0];

                Initialize();
            }
        }

        protected abstract void Initialize();
    }
}
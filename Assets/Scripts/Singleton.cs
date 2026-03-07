// Adapted & simplified from the Save Clicker Singleton class


using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindFirstObjectByType(typeof(T));

                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T) + " (Singleton)";

                    if (Application.isPlaying)
                    {
                        DontDestroyOnLoad(singletonObject);
                    }
                }
            }

            return _instance;
        }
    }
}
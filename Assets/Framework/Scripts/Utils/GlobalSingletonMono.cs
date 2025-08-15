
using UnityEngine;

public class GlobalSingletonMono<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _inst = null;
    public static T inst
    {
        get
        {
            if (_inst == null)
            {
                var go = new GameObject(typeof(T).Name + " (Singleton)");
                go.hideFlags = HideFlags.DontSave;
                DontDestroyOnLoad(go);

                go.GetOrAddComponent<T>();
            }
            return _inst;
        }
    }

    private void Awake()
    {
        if (_inst == null)
        {
            _inst = GetComponent<T>();
            gameObject.hideFlags = HideFlags.DontSave;
            gameObject.name = typeof(T).Name + " (Singleton)";
            DontDestroyOnLoad(gameObject);
            OnCreated();
        }
        else
            DestroyImmediate(gameObject);
    }

    protected virtual void OnCreated()
    { }
}

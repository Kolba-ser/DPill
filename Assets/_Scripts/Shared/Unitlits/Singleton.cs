using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();
                if (!_instance)
                {
                    Debug.LogError($"Singleton of type {typeof(T)} not contains in scene");
                    return null;
                }

                _instance.AwakeSingletone();
            }
            
            return _instance;
        }
    }
    protected virtual bool ForceOverride { get; } = false;

    public static bool InstanceIsNotNull => _instance;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = GetComponent<T>();
            AwakeSingletone();
        }
        else if(ForceOverride)
        {
            _instance = GetComponent<T>();
            AwakeSingletone();
        }
        else if (_instance != this)
            Debug.LogError($"Dublicated singleton instance {typeof(T).Name}", this);
    }  

    protected virtual void AwakeSingletone() { }
}
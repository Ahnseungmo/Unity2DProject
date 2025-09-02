using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static T _instance = default(T);
    public static T Get
    {
        get
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }

    }
    public static void Delete()
    {
        _instance = null;
    }

};
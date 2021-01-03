using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            GameObject obj = new GameObject
            {
                name = typeof(T).Name,
                hideFlags = HideFlags.HideAndDontSave
            };

            instance = obj.AddComponent<T>();

            return instance;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}

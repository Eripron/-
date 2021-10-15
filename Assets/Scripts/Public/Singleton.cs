using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;

    protected void Awake()
    {
        instance = GetComponent<T>();
    }
}

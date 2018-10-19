using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

public interface ISingleton
{
    string TName { get; }
}
public abstract class Singleton<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    Debug.LogError("There is no " + typeof(T).Name + " in Scene");
                }
            }
            return _instance;
        }
    }
    public string TName
    {
        get { return typeof(T).Name; }
    }
}
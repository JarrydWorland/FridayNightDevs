using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
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
            }
            return _instance;
        }
    }
    public string TName
    {
        get { return typeof(T).Name; }
    }
}
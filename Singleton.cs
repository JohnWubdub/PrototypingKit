﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Object
{
    //singleton example
    
    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Instance = FindObjectOfType<T>();
            }
            return instance;
        }
        private set { instance = value; }
    }
}
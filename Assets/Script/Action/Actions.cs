using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public static class Actions
{
    public static UnityAction<Bot> killbot;
    public static CounterTime counterTime = new CounterTime();
    public static void Start(IAction action,float time)
    {
        counterTime.Start(action,time);
    }
}

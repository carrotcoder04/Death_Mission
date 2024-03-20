using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAction
{
    void Enter();
}
public class Action : IAction
{
    public UnityAction action;
    public void Enter()
    {
        action.Invoke();
    }
}

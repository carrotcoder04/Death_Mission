using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim("Idle");
    }

    public void OnExecute(Bot t)
    {
        
    }

    public void OnExit(Bot t)
    {
        
    }
}

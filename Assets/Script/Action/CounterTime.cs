using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Application;

public class CounterTime
{
    public void Start(IAction action, float time)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(time);
        sequence.OnComplete(() => Active(action));
        sequence.Play();
    }
    private void Active(IAction action)
    {
        action.Enter();
    }
}

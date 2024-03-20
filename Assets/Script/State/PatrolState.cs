using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int point;
    Tween tween;
    public void OnEnter(Bot t)
    {
        point = 0;
        t.ChangeAnim("Run");
        MoveToTarget(t);
    }
    void MoveToTarget(Bot t)
    {
        float distance = Vector2.Distance(t.transform.position, t.path.vectorPath[point]);
        float botdirect = t.path.vectorPath[point].x - t.transform.position.x;
        if(botdirect < 0 )
        {
            t.spriteRenderer.transform.localScale = new Vector3(-1, 1, 0);
        }
        else if(botdirect > 0 )
        {
            t.spriteRenderer.transform.localScale = new Vector3(1, 1, 0);
        }
        tween = t.transform.DOMove(t.path.vectorPath[point], distance / 0.35f).OnComplete(() =>
        {
            if (point < t.path.vectorPath.Count - 1)
            {
                point++;
                MoveToTarget(t);
            }
        });
    }
    public void OnExecute(Bot t)
    {
        
    }
    public void OnExit(Bot t)
    {
        tween.Kill();
    }
}

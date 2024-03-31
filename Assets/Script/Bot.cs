using DG.Tweening;
using MarchingBytes;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.Application;

public class Bot : Character
{
    IState<Bot> currentState;
    public Seeker seeker;
    public Transform target;
    public SpriteRenderer spriteRenderer;
    public Path path;
    public float distance = 2.5f;
    public bool isinRange = false;
    private void OnDisable()
    {
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    private void OnEnable()
    {
        if(target != null) ResetBot();
    }
    void Start()
    {
        OnInit(100);
        target = Map1Controller.Instance.playerTF;
        InvokeRepeating("CalculatorPath", 2f, 0.5f);
    }
    void CalculatorPath()
    {
        if(!isDead)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance < this.distance)
            {
                if (distance < 0.2f)
                {
                    Vector3 direct = (target.transform.position - transform.position).normalized;
                    Player player = target.GetComponent<Player>();
                    player.TakeDamage(3);
                    player.PushBack(direct);
                }
                isinRange = true;
            }
            else isinRange = false;
            if (isinRange && !isHurting && seeker.IsDone())
            {
                seeker.StartPath(transform.position, target.position, OnPathCallBack);
            }
            if (!isinRange)
            {
                ChangeState(new IdleState());
            }
        }
    }
    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    void OnPathCallBack(Path path)
    {
        if (path.error)
        {
            ChangeState(new IdleState());
            return;
        }
        this.path = path;
        ChangeState(new PatrolState());
    }
    void ResetBot()
    {
        ResetCharacter();
        isinRange = false;
        ChangeState(new IdleState());
    }
    protected override void OnDeath()
    {
        Actions.killbot.Invoke(this);
        base.OnDeath();
        ChangeState(new DeathState());
    }
    protected override void OnHurting(float damage)
    {
        base.OnHurting(damage);
        ChangeState(new HurtState());
    }
}

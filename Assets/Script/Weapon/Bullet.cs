using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected float damage;
    protected void OnInit(float damge)
    {
        this.damage = damge;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Bot bot = collision.GetComponent<Bot>();
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            bot.TakeDamage(damage);
            bot.PushBack(direction);
        }
        OnHit();
    }
    protected virtual void OnHit()
    {

    }
}

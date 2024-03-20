using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private string currentAnim;
    protected float maxHp;
    protected float currentHp;
    protected bool isHurting = false;
    private float pushForce = 0.05f;
    protected bool isDead = false;
    protected Collider2D collider2d;
    protected IAction action;
    public void OnInit(float maxHp)
    {
        collider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        this.maxHp = maxHp;
        currentHp = maxHp;
        ChangeAnim("Idle");
    }
    public void ResetCharacter()
    {
        isDead = false;
        isHurting = false;
        collider2d.enabled = true;
        currentHp = maxHp;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null) animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
           OnHurting(damage);
        }
    }
    public void PushBack(Vector3 direction)
    {
        rb.AddForce(direction * pushForce,ForceMode2D.Impulse);
        Invoke(nameof(StopForce),0.2f);
    }
    void StopForce()
    {
        rb.velocity = Vector3.zero;
    }
    protected virtual void OnDeath()
    {
        isDead = true;
        collider2d.enabled = false;
        ChangeAnim("Dead");
        //gameObject.SetActive(false) after 3s
        action = new Action()
        {
            action = () =>
            {
                gameObject.SetActive(false);
            }
        };
        Actions.Start(action, 3);
    }
    protected virtual void OnHurting(float damage)
    {
        isHurting = true;
        ChangeAnim("Hurt");
        //Change isHurting = false after 0.1s
        action = new Action()
        {
            action = () =>
            {
                isHurting = false;
            }
        };
        Actions.Start(action, 0.1f);
        currentHp -= damage;
        if (currentHp < 0.001f)
        {
            OnDeath();
        }
    }
}

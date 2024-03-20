using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BulletShotgun : MonoBehaviour
{
    public bool isHit = false;
    [SerializeField] private Animator animator;
    [SerializeField] GameObject bulletHit;
    private string currentAnim;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        ChangeAnim("Init");
    }
    private void OnDisable()
    {
        isHit = false;
        sprite.enabled = true;
        bulletHit.SetActive(false);
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    public IEnumerator Disappear()
    {
        ChangeAnim("Disappear");
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
        yield break;
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null) animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Weapon")
        {
            if (!isHit)
            {
                isHit = true;
                StartCoroutine(CollisionHandling());
            }
        }
    }
    IEnumerator CollisionHandling()
    {
        sprite.enabled = false;
        bulletHit.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
        yield break;
    }
}

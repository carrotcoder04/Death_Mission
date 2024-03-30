using MarchingBytes;
using System.Collections;
using UnityEngine;

public class BulletRevolverHit : Bullet
{
    [SerializeField] GameObject bulletHit;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public bool isHit = false;
    private void Start()
    {
        OnInit(10);
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        isHit = false;
        bulletHit.SetActive(false);
        sprite.enabled = true;
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    protected override void OnHit()
    {
        if (!isHit)
        {
            isHit = true;
            StartCoroutine(CollisionHandling());
        }
    }
    IEnumerator CollisionHandling()
    {
        bulletHit.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        sprite.enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
        yield break;
    }
}

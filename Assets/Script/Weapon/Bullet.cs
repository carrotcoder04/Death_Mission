using MarchingBytes;
using System.Collections;
using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bulletHit;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public bool isHit = false;
    private void Start()
    {
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Weapon")
        {
            if(!isHit)
            {
                isHit = true;
                StartCoroutine(CollisionHanding());
            }
        }
    }
    IEnumerator CollisionHanding()
    {
        bulletHit.SetActive(true);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.05f);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}

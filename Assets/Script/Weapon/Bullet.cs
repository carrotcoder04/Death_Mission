using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bulletHit;
    Rigidbody2D rb;
    BoxCollider2D box;
    SpriteRenderer sprite;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        bulletHit.SetActive(false);
        box.enabled = true;
        sprite.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Weapon")
        {
            StartCoroutine(CollisonHanding());
        }
    }
    IEnumerator CollisonHanding()
    {
        bulletHit.SetActive(true);
        box.enabled = false;
        sprite.enabled = false;
        yield return new WaitForSeconds(0.05f);
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
    }
}

using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
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
        sprite.enabled = true;
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Weapon")
        {
            if (!isHit)
            {
                isHit = true;
                StartCoroutine(Boom());
            }
        }
    }
    public IEnumerator Boom()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.4f);
        sprite.enabled = false;
        explosion.SetActive(true);
        CameraFollower.Instance.CameraShake(0.15f);
        yield return new WaitForSeconds(0.4f);
        explosion.SetActive(false);
        gameObject.SetActive(false);
        yield break;
    }
}

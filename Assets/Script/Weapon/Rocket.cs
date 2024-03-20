using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rocket : MonoBehaviour
{
    [SerializeField] List<GameObject> hit = new List<GameObject>();
    [SerializeField] GameObject explosion;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    public bool isHit = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnDisable()
    {
        isHit = false;
        sprite.enabled = true;
        for (int i = 0; i < hit.Count; i++)
        {
            hit[i].SetActive(false);
        }
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }
    private void OnEnable()
    {
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < hit.Count; i++)
        {
            hit[i].SetActive(true);
        }
        yield break;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Weapon")
        {
            if(!isHit)
            {
                isHit = true;
                StartCoroutine(Boom());
            }
        }
    }
    public IEnumerator Boom()
    {
        rb.velocity = Vector2.zero;
        sprite.enabled = false;
        explosion.SetActive(true);
        CameraFollower.Instance.CameraShake(0.15f);
        yield return new WaitForSeconds(0.4f);
        explosion.SetActive(false);
        gameObject.SetActive(false);
        yield break;
    }
}

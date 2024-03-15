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
    public bool isHit = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        isHit = false;
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
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < hit.Count; i++)
        {
            hit[i].SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "Weapon")
        {
            if(!isHit)
            {
                isHit = true;
                Debug.Log("Hit " + collision.gameObject.name);
                StartCoroutine(Boom());
            }
        }
    }
    public IEnumerator Boom()
    {
        rb.velocity = Vector2.zero;
        explosion.SetActive(true);
        CameraFollower.Instance.CameraShake(0f, 0.04f);
        yield return new WaitForSeconds(0.4f);
        explosion.SetActive(false);
        gameObject.SetActive(false);
    }
}

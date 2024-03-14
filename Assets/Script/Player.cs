using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Player : Character
{
    [SerializeField] GameObject player;
    [SerializeField] Transform firepos;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletparent;
    private float maxfireTime = 0.2f;
    private float firetime = 0;
    private Transform playerTF;
    private float speed = 1f;
    private bool isRunning = false;
    void Start()
    {
        playerTF = player.transform;
        OnInit();
    }
    void Update()
    {
        Move();
        firetime += Time.deltaTime;
        if(Input.GetMouseButton(0) && firetime > maxfireTime)
        {
            StartCoroutine(Fire());
            firetime = 0;
        }
    }

    IEnumerator Fire()
    {
        bulletparent.SetActive(true);
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        Vector3 direct = (target - firepos.transform.position).normalized;
        GameObject bullettmp = EasyObjectPool.instance.GetObjectFromPool("Bullet",firepos.transform.position,firepos.rotation);
        Rigidbody2D rb  = bullettmp.GetComponent<Rigidbody2D>();
        rb.AddForce(direct*5f,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        bulletparent.SetActive(false);
        bullettmp.SetActive(false);
        EasyObjectPool.instance.ReturnObjectToPool(bullettmp);
        StopCoroutine(Fire());
    }

    void Move()
    {
        Vector3 direct;
        if(Input.GetKey(KeyCode.W))
        {
            direct = new Vector2(0, 1) * speed * Time.deltaTime;
            playerTF.position += direct;
            isRunning = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direct = new Vector2(0, -1) * speed * Time.deltaTime;
            playerTF.position += direct;
            isRunning = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direct = new Vector2(1, 0) * speed * Time.deltaTime;
            playerTF.position += direct;
            isRunning = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direct = new Vector2(-1, 0) * speed * Time.deltaTime;
            playerTF.position += direct;
            isRunning = true;
        }
        if (isRunning)
        {
            ChangeAnim("Run");
        }
        else
        {
            ChangeAnim("Idle");
        }
        isRunning = false;
    }
}
